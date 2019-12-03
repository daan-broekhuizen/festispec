using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.API.ImageShack
{
    public class ImageContainer
    {
        public string ImageLocation { get; set; }

        public string ContentType { get; set; }

        public ImageContainer(string imageLocation, string contentType = "image/png")
        {
            this.ImageLocation = imageLocation;
            this.ContentType = contentType;
        }
    }
}
