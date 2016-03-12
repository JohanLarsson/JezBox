using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace JezBox.AssetSyncServiceClient {
    /// <summary>
    /// AssetSync service client with methods to access given web API services.
    /// 
    /// Enumeration values in any AssetSyncServiceClientException's thrown are of type AssetSyncServiceClientErrorType.
    /// </summary>
    public class AssetSyncServiceClient : IAssetSyncServiceClient {
        #region Private members
        private object _lock = new object();
        private bool _initialized = false;
        private HttpClient _instanceClient;
        private HttpClient _client {
            get {
                lock (_lock) {
                    if (!_initialized) { throw new AssetSyncServiceClientException("HTTP client has not been initialized."); }
                    return _instanceClient;
                }
            }
        }
        #endregion

        #region Core methods
        /// <summary>
        /// Creates and configures (or recreates and reconfigures) HTTP client.
        /// </summary>
        public void Initialize(string baseUrl, bool onlyRequestJson = true) {
            lock (_lock) {
                Close();

                _instanceClient = new HttpClient();
                _instanceClient.BaseAddress = new Uri(baseUrl);

                if (onlyRequestJson) {
                    _instanceClient.DefaultRequestHeaders.Accept.Clear();
                    _instanceClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }

                _initialized = true;
            }
        }

        /// <summary>
        /// Closes / disposes of the HTTP client.
        /// </summary>
        public void Close() {
            lock (_lock) {
                Dispose();
            }
        }
        
        protected virtual void Dispose(bool disposing) {
            lock (_lock) {
                // Clean up native resources...

                if (disposing) {
                    // Also clean up managed resources...
                    if (_initialized && _instanceClient != null) {
                        _instanceClient.Dispose();
                    }
                }
            }
        }

        public void Dispose() {
            Dispose(true);
        }
        #endregion

        /// <summary>
        /// Tries to ping the web API service.
        /// </summary>
        /// <returns>True if successful; otherwise false.</returns>
        public async Task<bool> PingServiceAsync() {
            // TODO: temp. reimplements old CheckConnection and CheckConnectionAsync (see Reference.cs)

            try {
                HttpResponseMessage response = await _client.GetAsync("api/ping");
                if (response.StatusCode != HttpStatusCode.NoContent) {
                    return false;
                }
                return true;
            }
            catch (AssetSyncServiceClientException) {
                throw;
            }
            catch (Exception) {
                return false;
            }
        }
    }

    public enum AssetSyncServiceClientErrorType : int {
        Other                    = 0,
        InvalidUsernamePassword  = 10,
    }
}
