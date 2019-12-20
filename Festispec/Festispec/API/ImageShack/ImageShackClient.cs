using Festispec.Utility.Builders;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.API.ImageShack
{
    public class ImageShackClient : ApiClient
    {
        private readonly string _apiKey;

        public ImageShackClient() : base("https://api.imageshack.com/v2/")
        {
            _apiKey = (new SettingsBuilder()).Build().ImageShackApiKey;
        }

        /// <summary>
        /// Haalt de image op met het gegeven id.
        /// </summary>
        /// <param name="id">De ID van het image.</param>
        /// <returns>De image.</returns>
        public ImageModel GetImage(string id)
        {
            Dictionary<string, string> urlSegments = new Dictionary<string, string>
            {
                { "id", id }
            };

            return RequestAsJsonObjectFromValue<ImageModel>("images/{id}", Method.GET, "result", urlSegments);
        }

        /// <summary>
        /// Upload een afbeelding
        /// </summary>
        /// <param name="file">De afbeelding</param>
        /// <returns>Het resultaat van de geuploaden afbeelding.</returns>
        public UploadModel UploadImage(FileData file)
        {
            return UploadImage(new FileData[] { file });
        }

        /// <summary>
        /// Upload afbeelding(en).
        /// </summary>
        /// <param name="files">De afbeelding(en).</param>
        /// <returns>Het resultaat van de geuploaden afbeelding(en).</returns>
        public UploadModel UploadImage(FileData[] files)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "api_key", _apiKey }
            };

            return RequestAsJsonObjectFromValue<UploadModel>("images", Method.POST, "result", null, parameters, null, files);
        }

        /// <summary>
        /// Upload een afbeelding
        /// </summary>
        /// <param name="fileLocation">De afbeelding locatie</param>
        /// <returns>Het resultaat van de geuploaden afbeelding.</returns>
        public UploadModel UploadImage(ImageContainer fileLocation)
        {
            return UploadImage(new ImageContainer[] { fileLocation });
        }

        /// <summary>
        /// Upload afbeelding(en).
        /// </summary>
        /// <param name="fileLocations">De locatie(s) van de afbeelding(en)</param>
        /// <returns>Het resultaat van de geuploaden afbeelding(en).</returns>
        public UploadModel UploadImage(ImageContainer[] fileLocations)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "api_key", _apiKey }
            };

            FileData[] fileData = new FileData[fileLocations.Length];

            for (int i = 0; i < fileLocations.Length; i++)
            {
                ImageContainer file = fileLocations[i];

                string filePath = file.ImageLocation.Replace("%20", " ");
                fileData[i] = new FileData(File.ReadAllBytes(filePath), file.ContentType);
            }

            return RequestAsJsonObjectFromValue<UploadModel>("images", Method.POST, "result", null, parameters, null, fileData);
        }

        public void Examples()
        {
            // Image Uploaden met byte array.
            /*
            UploadModel model = UploadImage(new FileData[]
            {
                new FileData(File.ReadAllBytes(@"D:\13.png"), "image/png")
            });
            */

            // Image Uploaden met file location
            /*
            UploadModel model = UploadImage(new ImageContainer[]
            {
                new ImageContainer(@"D:\13.png", "image/png")
            });
            Console.WriteLine(model.Images[0].DirectLink);\
            */

            // Image ophalen
            /*
            ImageModel model = GetImage("pmPXEkXep");
            */
        }
    }
}
