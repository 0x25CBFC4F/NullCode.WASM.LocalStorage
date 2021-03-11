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
            serviceCollection.AddSingleton<ILocalStorageService, LocalStorageService>();
        }
    }
}