using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace JezBox
{
    public sealed class AssetSyncServiceClient : IAssetSyncServiceClient
    {
        private readonly HttpClient _client;
        private bool _disposed;

        public AssetSyncServiceClient(AssetSyncServiceClientSettings settings)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(settings.BaseUrl);

            if (settings.JsonOnly)
            {
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        public Task<string> GetStringAsync(string requestUri)
        {
            VerifyNotDisposed();
            return _client.GetStringAsync(requestUri);
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;
            _client.Dispose();
        }

        private void VerifyNotDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }
        }
    }
}