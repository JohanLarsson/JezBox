using System;
using System.Threading.Tasks;

namespace JezBox
{
    public interface IAssetSyncServiceClient : IDisposable
    {
        // Picked a sample method randomly, you can expose an api that makes sense in your app/lib
        Task<string> GetStringAsync(string requestUri);
    }

    public class AssetSyncService
    {
        private readonly IAssetSyncServiceClient client;

        public AssetSyncService(IAssetSyncServiceClient client)
        {
            this.client = client;
        }
    }
}