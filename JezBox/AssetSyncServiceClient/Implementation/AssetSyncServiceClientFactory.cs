using System;
using System.Data.SqlClient;
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
            private readonly AssetSyncServiceClientSettings _settings;
            private readonly SerialDisposable<HttpClient> _client;
            private bool _disposed;

            public AssetSyncServiceClient(AssetSyncServiceClientSettings settings)
            {
                this._settings = settings;
                _client = new SerialDisposable<HttpClient>(CreateClient(settings));
            }

            public void UpdateUrl(string url)
            {

                _settings.BaseUrl = url;
                _client.SetValue(CreateClient(_settings));
            }

            /// <summary>
            /// Tries to ping the web API service.
            /// </summary>
            /// <returns>True if successful; otherwise false.</returns>
            public async Task<bool> IsAlive()
            {
                VerifyNotDisposed();
                try
                {
                    var client = _client.Getvalue();
                    if (client == null)
                    {
                        return false;
                    }
                    var response = await client.GetAsync("api/ping").ConfigureAwait(false);

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

            private HttpClient CreateClient(AssetSyncServiceClientSettings settings)
            {
                var client = new HttpClient { BaseAddress = new Uri(settings.BaseUrl) };

                if (settings.JsonOnly)
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }

                return client;
            }
        }
    }
}
