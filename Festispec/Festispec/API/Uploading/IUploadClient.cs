using Festispec.API.ImageShack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.API.Uploading
{
    public interface IUploadClient
    {
        /// <summary>
        /// Haalt de image op met het gegeven id.
        /// </summary>
        /// <param name="id">De ID van het image.</param>
        /// <returns>De image.</returns>
        ImageModel GetImage(string id);

        /// <summary>
        /// Upload afbeelding(en).
        /// </summary>
        /// <param name="files">De afbeelding(en).</param>
        /// <returns>Het resultaat van de geuploaden afbeelding(en).</returns>
        UploadModel UploadImage(FileData[] files);

        /// <summary>
        /// Upload afbeelding(en).
        /// </summary>
        /// <param name="fileLocations">De locatie(s) van de afbeelding(en)</param>
        /// <returns>Het resultaat van de geuploaden afbeelding(en).</returns>
        UploadModel UploadImage(ImageContainer[] fileLocations);
    }
}
