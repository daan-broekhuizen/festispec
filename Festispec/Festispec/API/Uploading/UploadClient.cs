using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festispec.API.ImageBB;
using Festispec.API.ImageShack;
using Festispec.Utility.Builders;

namespace Festispec.API.Uploading
{
    public class UploadClient : IUploadClient
    {
        private readonly IUploadClient _currentClient;

        public UploadClient()
        {
            switch(new SettingsBuilder().Build().UploadService.ToLower())
            {
                case UploadServices.IMAGESHACK:
                    _currentClient = new ImageShackClient();

                    break;
                case UploadServices.IMAGEBB:
                    _currentClient = new ImageBBClient();

                    break;
            }
        }

        public ImageModel GetImage(string id)
        {
            return _currentClient.GetImage(id);
        }

        public UploadModel UploadImage(FileData file)
        {
            return _currentClient.UploadImage(new FileData[] { file });
        }

        public UploadModel UploadImage(FileData[] files)
        {
            return _currentClient.UploadImage(files);
        }

        public UploadModel UploadImage(ImageContainer fileLocation)
        {
            return _currentClient.UploadImage(new ImageContainer[] { fileLocation });
        }

        public UploadModel UploadImage(ImageContainer[] fileLocations)
        {
            return _currentClient.UploadImage(fileLocations);
        }
    }
}
