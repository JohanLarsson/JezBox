using System;
using System.Threading.Tasks;

namespace JezBox {
    /// <summary>
    /// AssetSync service client with methods to access given web API services.
    /// 
    /// Exceptions thrown occurring within the service client should be of type (or extend) AssetSyncServiceClientException.
    /// </summary>
    public interface IAssetSyncServiceClient : IDisposable
    {
        void UpdateUrl(string url);

        /// <summary>
        /// Tries to ping the web API service.
        /// </summary>
        /// <returns>True if successful; otherwise false.</returns>
        Task<bool> IsAlive();
    }
}
