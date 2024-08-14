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
        private LotteryPageViewModel _viewModel;

        [SetUp]
        public void SetUp()
        {
            _mockLotteryDataService = new Mock<ILotteryDataService>();
            _viewModel = new LotteryPageViewModel(_mockLotteryDataService.Object);
        }

        [Test]
        public async Task LoadLotteryDrawsAsync_SuccessfullyLoadsData()
        {
            // Arrange
            DateTime drawDate = new DateTime(2023, 5, 15);
            var expectedDraws = new List<LotteryDrawModel>
            {
                new LotteryDrawModel { Id = "draw-1", DrawDate = drawDate, Number1 = 2 },
                new LotteryDrawModel { Id = "draw-2", DrawDate = drawDate, Number1 = 5 }
            };

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
        }

        [Test]
        public async Task LoadLotteryDrawsAsync_HandlesException()
        {
            // Arrange
            var exceptionMessage = "An error occurred";

            _mockLotteryDataService
                .Setup(service => service.GetLotteryDrawsAsync())
                .ThrowsAsync(new Exception(exceptionMessage));

            // Act
            await _viewModel.LoadLotteryDrawsAsync();

            // Assert
            Assert.False(_viewModel.IsLoading);
            Assert.AreEqual($"Failed to load data: {exceptionMessage}", _viewModel.ErrorMessage);
            Assert.IsEmpty(_viewModel.LotteryDraws);
        }
    }
}
