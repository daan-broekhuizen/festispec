using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.API
{
    public class FileData
    {
        public string Name { get; set; }

        public byte[] Data { get; set; }

        public string FileName { get; set; }

        public string ContentType { get; set; }

        public FileData(byte[] data, string contentType, string name = "file", string fileName = null)
        {
            this.Data = data;
            this.ContentType = contentType;
            this.Name = name;
            this.FileName = fileName;

            if (this.FileName == null)
                GenerateFileName(10);
        }

        public void GenerateFileName(int length)
        {
            Random random = new Random();

            this.FileName = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
