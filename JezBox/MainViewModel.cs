namespace JezBox
{
    public class MainViewModel
    {
        private readonly AssetSyncService service;

        public MainViewModel(AssetSyncService service)
        {
            this.service = service;
        }
    }
}
