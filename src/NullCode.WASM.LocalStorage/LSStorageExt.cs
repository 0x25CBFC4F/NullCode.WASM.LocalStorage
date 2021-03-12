using Microsoft.Extensions.DependencyInjection;

namespace NullCode.WASM.LocalStorage
{
    public static class LSStorageExt
    {
        /// <summary>
        /// This method registers Local storage service in Microsoft DI container.
        /// </summary>
        public static void AddLocalStorageService(this IServiceCollection serviceCollection)
        {
            AddLocalStorageService<LocalStorageService>(serviceCollection);
        }

        /// <summary>
        /// This methods registers custom implementation of local storage service in Microsoft DI container.
        /// </summary>
        /// <typeparam name="TService">Implementation type</typeparam>
        public static void AddLocalStorageService<TService>(this IServiceCollection serviceCollection) where TService : class, ILocalStorageService
        {
            serviceCollection.AddSingleton<ILocalStorageService, TService>();
        }
    }
}