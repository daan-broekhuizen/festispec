using Microsoft.VisualStudio.TestTools.UnitTesting;
using Festispec.API.ImageShack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Festispec.API.Uploading;

namespace Festispec.API.ImageShack.Tests
{
    [TestClass()]
    public class ImageShackClientTests
    {
        [TestMethod()]
        public void UploadImageTest()
        {
            string imgLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "/Images/test.png";

            ImageShackClient client = new ImageShackClient();

            UploadModel upload = client.UploadImage(new ImageContainer[] { new ImageContainer(imgLocation, "image/png") });

            Assert.IsNotNull(upload.Images, "Er zijn geen afbeeldingen geupload");
            Assert.IsTrue(upload.Images.Length > 0, "Er zijn gee nimages geupload");
        }

        [TestMethod()]
        public void GetImageTest()
        {
            string id = "pmNU2wZhp";

            ImageShackClient client = new ImageShackClient();
            ImageModel image = client.GetImage(id);

            Assert.IsNotNull(image, "Er was geen image gevonden");
            Assert.IsNotNull(image.DirectLink, "Er is geen link beschikbaar");
        }
    }
}