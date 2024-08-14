using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using LotteryApp.Core.Models;
using LotteryApp.Core.Services;
using LotteryApp.Core.ViewModels;

namespace LotteryApp.Tests.ViewModels
{
    [TestFixture]
    public class LotteryPageViewModelTests
    {
        private Mock<ILotteryDataService> _mockLotteryDataService;
        private Mock<IPreferencesService> _mockPreferencesService;
        private Mock<IConnectivityService> _mockConnectivityService;
        private LotteryPageViewModel _viewModel;

        [SetUp]
        public void SetUp()
        {
            _mockLotteryDataService = new Mock<ILotteryDataService>();
            _mockPreferencesService = new Mock<IPreferencesService>();
            _mockConnectivityService = new Mock<IConnectivityService>();

            _viewModel = new LotteryPageViewModel(
                _mockLotteryDataService.Object,
                _mockPreferencesService.Object,
                _mockConnectivityService.Object
            );
        }

        [Test]
        public async Task LoadLotteryDrawsAsync_SuccessfullyLoadsData_WithInternetConnection()
        {
            // Arrange
            DateTime drawDate = new DateTime(2023, 5, 15);
            var expectedDraws = new List<LotteryDrawModel>
            {
                new LotteryDrawModel { Id = "draw-1", DrawDate = drawDate.ToString(), Number1 = 2 },
                new LotteryDrawModel { Id = "draw-2", DrawDate = drawDate.ToString(), Number1 = 5 }
            };

            _mockConnectivityService
                .Setup(service => service.IsConnected())
                .Returns(true);

            _mockLotteryDataService
                .Setup(service => service.GetLotteryDrawsAsync())
                .ReturnsAsync(expectedDraws);

            // Act
            await _viewModel.LoadLotteryDrawsAsync();

            // Assert
            Assert.False(_viewModel.IsLoading);
            Assert.IsEmpty(_viewModel.ErrorMessage);
            Assert.AreEqual(2, _viewModel.LotteryDraws.Count);
            Assert.AreEqual(expectedDraws, _viewModel.LotteryDraws);
            _mockPreferencesService.Verify(service => service.SaveList(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task LoadLotteryDrawsAsync_UsesSavedPreferences_WhenNoInternetConnection()
        {
            // Arrange
            _mockConnectivityService
                .Setup(service => service.IsConnected())
                .Returns(false);

            var savedDrawsJson = "[{\"Id\":\"draw-1\",\"DrawDate\":\"2023-05-15T00:00:00\",\"Number1\":2}]";
            _mockPreferencesService
                .Setup(service => service.GetList(It.IsAny<string>()))
                .Returns(savedDrawsJson);

            // Act
            await _viewModel.LoadLotteryDrawsAsync();

            // Assert
            Assert.False(_viewModel.IsLoading);
            Assert.IsEmpty(_viewModel.ErrorMessage);
            Assert.AreEqual(1, _viewModel.LotteryDraws.Count);
            Assert.AreEqual("draw-1", _viewModel.LotteryDraws[0].Id);
            _mockLotteryDataService.Verify(service => service.GetLotteryDrawsAsync(), Times.Never);
        }

        [Test]
        public async Task LoadLotteryDrawsAsync_HandlesException()
        {
            // Arrange
            var exceptionMessage = "An error occurred";

            _mockConnectivityService
                .Setup(service => service.IsConnected())
                .Returns(true);

            _mockLotteryDataService
                .Setup(service => service.GetLotteryDrawsAsync())
                .ThrowsAsync(new Exception(exceptionMessage));

            // Act
            await _viewModel.LoadLotteryDrawsAsync();

            // Assert
            Assert.False(_viewModel.IsLoading);
            Assert.AreEqual($"Failed to load data: {exceptionMessage}", _viewModel.ErrorMessage);
            Assert.IsEmpty(_viewModel.LotteryDraws);
            _mockPreferencesService.Verify(service => service.SaveList(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task LoadLotteryDrawsAsync_ShowsError_WhenNoInternetAndNoSavedData()
        {
            // Arrange
            _mockConnectivityService
                .Setup(service => service.IsConnected())
                .Returns(false);

            _mockPreferencesService
                .Setup(service => service.GetList(It.IsAny<string>()))
                .Returns(string.Empty);

            // Act
            await _viewModel.LoadLotteryDrawsAsync();

            // Assert
            Assert.False(_viewModel.IsLoading);
            Assert.AreEqual("No internet connection and no saved data available.", _viewModel.ErrorMessage);
            Assert.IsEmpty(_viewModel.LotteryDraws);
        }
    }
}
