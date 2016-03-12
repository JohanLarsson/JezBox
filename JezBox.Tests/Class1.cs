namespace JezBox.Tests
{
    using Ninject;
    using NUnit.Framework;

    public class Class1
    {
        [Test]
        public void ResolveSimple()
        {
            var kernel = new StandardKernel();
            var client1 = kernel.Get<AssetSyncServiceClient>();
            var client2 = kernel.Get<AssetSyncServiceClient>();

            Assert.AreNotSame(client1, client2);
        }
    }
}
