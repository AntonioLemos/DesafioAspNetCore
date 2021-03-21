using BlazorApp.Models;
using Microsoft.JSInterop;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorApp.Data
{
    public class LocalStorageService : ILocalStorageService
    {
        private IJSRuntime _jsRuntime;
        public LocalStorageService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<User> GetItem(string key)
        {
            var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);

            if (json == null)
                return new User();

            return JsonSerializer.Deserialize<User>(json);
        }

        public async Task SetItem(string key, User value)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, JsonSerializer.Serialize(value));
        }

        public async Task RemoveItem(string key)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
        }
    }
}
