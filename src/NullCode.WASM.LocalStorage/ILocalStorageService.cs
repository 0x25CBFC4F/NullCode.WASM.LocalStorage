using System.Threading.Tasks;

namespace NullCode.WASM.LocalStorage
{
    /// <summary>
    /// <see cref="ILocalStorageService"/> provides easy interop with browsers' local storage. <br/>
    /// JSON object storage is supported as well as raw text.
    /// </summary>
    public interface ILocalStorageService
    {
        /// <summary>
        /// Get object from local storage.
        /// </summary>
        /// <param name="name">Key name; Default is full name of <typeparamref name="T"/></param>
        /// <typeparam name="T">Object type</typeparam>
        /// <returns>Object instance or null</returns>
        Task<T> Get<T>(string name = null) where T : class;
        
        /// <summary>
        /// Puts object into local storage.
        /// </summary>
        /// <param name="name">Key name; Default is full name of <paramref name="obj"/></param>
        /// <param name="obj">Object instance</param>
        /// <returns>'true' on success, 'false' otherwise</returns>
        Task<bool> Set(string name, object obj);
        
        /// <summary>
        /// Puts object into local storage with default name.
        /// </summary>
        /// See also: <seealso cref="Set(string,object)"/>
        Task<bool> Set(object obj);
        
        /// <summary>
        /// Returns raw string from local storage by key.
        /// </summary>
        /// <param name="key">Key name</param>
        /// <returns>Raw text or null</returns>
        Task<string> GetRaw(string key);
        
        /// <summary>
        /// Puts raw string into local storage.
        /// </summary>
        /// <param name="key">Key name</param>
        /// <param name="raw">Raw string</param>
        /// <returns></returns>
        Task SetRaw(string key, string raw);

        /// <summary>
        /// Removes specific item from local storage by key name
        /// </summary>
        /// <param name="key">Key name</param>
        Task Remove(string key);

        /// <summary>
        /// Removes all items from local storage.
        /// </summary>
        Task RemoveAll();
    }
}