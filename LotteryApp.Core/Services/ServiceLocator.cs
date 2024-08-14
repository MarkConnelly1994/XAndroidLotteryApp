using System;
using System.Collections.Generic;

namespace LotteryApp.Core.Services
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> services = new Dictionary<Type, object>();
        private static readonly object lockObj = new object();

        /// <summary>
        /// Registers a service of type T.
        /// </summary>
        /// <typeparam name="T">The type of the service.</typeparam>
        /// <param name="service">The instance of the service to register.</param>
        public static void Register<T>(T service) where T : class
        {
            lock (lockObj)
            {
                var type = typeof(T);
                if (services.ContainsKey(type))
                {
                    services[type] = service; 
                }
                else
                {
                    services.Add(type, service);
                }
            }
        }

        /// <summary>
        /// Resolves a service of type T.
        /// </summary>
        /// <typeparam name="T">The type of the service.</typeparam>
        /// <returns>The instance of the service.</returns>
        public static T Resolve<T>() where T : class
        {
            lock (lockObj)
            {
                var type = typeof(T);
                if (services.TryGetValue(type, out var service))
                {
                    return service as T;
                }
                else
                {
                    throw new InvalidOperationException($"Service of type {type.Name} not registered.");
                }
            }
        }

        /// <summary>
        /// Checks if a service of type T is registered.
        /// </summary>
        /// <typeparam name="T">The type of the service.</typeparam>
        /// <returns>True if the service is registered, otherwise false.</returns>
        public static bool IsRegistered<T>() where T : class
        {
            lock (lockObj)
            {
                return services.ContainsKey(typeof(T));
            }
        }
    }
}
