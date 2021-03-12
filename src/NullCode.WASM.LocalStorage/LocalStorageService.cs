using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace NullCode.WASM.LocalStorage
{
    /// <inheritdoc />
    public class LocalStorageService : ILocalStorageService
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly ILogger<LocalStorageService> _logger;

        public LocalStorageService(IJSRuntime jsRuntime, ILogger<LocalStorageService> logger)
        {
            _jsRuntime = jsRuntime;
            _logger = logger;
        }
        
        public virtual async Task<T> Get<T>(string name = null) where T : class
        {
            name = CheckName(name, typeof(T));
            
            var rawJson = await GetRaw(name);

            if (rawJson == null)
            {
                return null;
            }

            try
            {
                return JsonConvert.DeserializeObject<T>(rawJson);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured while deserializing JSON");
                return null;
            }
        }

        public virtual async Task<bool> Set(string name, object obj)
        {
            name = CheckName(name, obj.GetType());

            try
            {
                var rawJson = JsonConvert.SerializeObject(obj, Formatting.None);
                await SetRaw(name, rawJson);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured while serializing JSON");
                return false;
            }
        }
        
        public virtual async Task<bool> Set(object obj)
        {
            return await Set(null, obj);
        }

        public virtual async Task<string> GetRaw(string key)
        {
            var escapedKey = Regex.Escape(key);
            return await _jsRuntime.InvokeAsync<string>("eval", $"window.localStorage.getItem('{escapedKey}')");
        }

        public virtual async Task SetRaw(string key, string raw)
        {
            var escapedKey = Regex.Escape(key);
            var escapedJson = Regex.Escape(raw);
            await _jsRuntime.InvokeVoidAsync("eval", $"window.localStorage.setItem(`{escapedKey}`, `{escapedJson}`)");
        }

        /// <inheritdoc />
        public virtual async Task Remove(string key)
        {
            var escapedKey = Regex.Escape(key); 
            await _jsRuntime.InvokeAsync<string>("eval", $"window.localStorage.removeItem('{escapedKey}')");
        }
        
        public virtual async Task Remove<T>() where T : class
        {
            var name = CheckName(null, typeof(T));
            await _jsRuntime.InvokeAsync<string>("eval", $"window.localStorage.removeItem('{name}')");
        }

        /// <inheritdoc />
        public virtual async Task RemoveAll()
        {
            await _jsRuntime.InvokeAsync<string>("eval", "window.localStorage.clear()");
        }

        /// <summary>
        /// Generates key name from class if specified name is empty.
        /// </summary>
        /// <param name="name">Current name</param>
        /// <param name="type">Type</param>
        /// <returns>New/old name</returns>
        private static string CheckName(string name, Type type)
        {
            return !string.IsNullOrWhiteSpace(name) ? name : type.FullName;
        }
    }
}