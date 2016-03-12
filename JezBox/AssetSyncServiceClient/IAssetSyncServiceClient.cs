using System;
using System.Threading.Tasks;

namespace JezBox.AssetSyncServiceClient {
    /// <summary>
    /// AssetSync service client with methods to access given web API services.
    /// 
    /// Exceptions thrown occurring within the service client should be of type (or extend) AssetSyncServiceClientException.
    /// </summary>
    public interface IAssetSyncServiceClient : IDisposable {
        /// <summary>
        /// Creates and configures (or recreates and reconfigures) HTTP client.
        /// </summary>
        void Initialize(string baseUrl, bool onlyRequestJson = true);

        /// <summary>
        /// Closes / disposes of the HTTP client.
        /// </summary>
        void Close();

        /// <summary>
        /// Tries to ping the web API service.
        /// </summary>
        /// <returns>True if successful; otherwise false.</returns>
        Task<bool> PingServiceAsync();
    }
}
