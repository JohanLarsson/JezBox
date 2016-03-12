using System;
using System.IO;
using System.Xml.Serialization;

namespace JezBox.Tests
{
    using Ninject;
    using NUnit.Framework;

    public class Class1
    {
        [Test]
        public void ResolveSimple()
        {
            var settings = new AssetSyncServiceClientSettings();
            var xmlSerializer = new XmlSerializer(settings.GetType());
            using (var writer = new StringWriter())
            {
                xmlSerializer.Serialize(writer, settings);
                Console.WriteLine(writer.ToString());
            }
            var kernel = new StandardKernel();
            var client1 = kernel.Get<AssetSyncServiceClient>();
            var client2 = kernel.Get<AssetSyncServiceClient>();

            Assert.AreNotSame(client1, client2);
        }
    }
}
