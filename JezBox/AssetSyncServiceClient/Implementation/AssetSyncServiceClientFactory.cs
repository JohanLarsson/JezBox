using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace JezBox
{
    /// <summary>
    /// A 'singleton factory' that creates and/or provides a single instance of IAssetSyncServiceClient.
    /// </summary>
    public static class AssetSyncServiceClientFactory
    {
        private static readonly Lazy<IAssetSyncServiceClient> _instance = new Lazy<IAssetSyncServiceClient>(() => new AssetSyncServiceClient(Properties.Settings.Default.AssetSyncServiceClientSettings));

        public static IAssetSyncServiceClient Client => _instance.Value;

        /// <summary>
        /// AssetSync service client with methods to access given web API services.
        /// 
        /// Enumeration values in any AssetSyncServiceClientException's thrown are of type AssetSyncServiceClientErrorType.
        /// </summary>
        private sealed class AssetSyncServiceClient : IAssetSyncServiceClient
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

            /// <summary>
            /// Tries to ping the web API service.
            /// </summary>
            /// <returns>True if successful; otherwise false.</returns>
            public async Task<bool> PingServiceAsync()
            {
                VerifyNotDisposed();
                try
                {
                    HttpResponseMessage response = await _client.GetAsync("api/ping").ConfigureAwait(false);
                    if (response.StatusCode != HttpStatusCode.NoContent)
                    {
                        return false;
                    }
                    return true;
                }
                catch (AssetSyncServiceClientException)
                {
                    throw;
                }
                catch (Exception)
                {
                    return false;
                }
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
}
