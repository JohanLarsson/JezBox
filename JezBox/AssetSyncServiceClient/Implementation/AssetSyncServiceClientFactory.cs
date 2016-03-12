namespace JezBox.AssetSyncServiceClient {
    /// <summary>
    /// A 'singleton factory' that creates and/or provides a single instance of IAssetSyncServiceClient.
    /// </summary>
    public static class AssetSyncServiceClientFactory {
        private static object _lock = new object();
        private static IAssetSyncServiceClient _instance = null;

        /// <summary>
        /// Gets the AssetSync service client singleton; creates lazily if necessary.
        /// </summary>
        /// <returns>The AssetSync service client singleton.</returns>
        public static IAssetSyncServiceClient GetAssetSyncServiceClient() {
            lock (_lock) {
                if (_instance == null) {
                    _instance = new AssetSyncServiceClient();
                }

                return _instance;
            }
        }
    }
}
