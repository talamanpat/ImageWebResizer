using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageWebResizer.Models
{
    public class Picture
    {

        public string Id { get; set; }
        public DateTime DateUpload { get; set; }
        public string PathOriginal { get; set; }
        public string Path300 { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public long? Length { get; }
        public string Name { get; }
        public string OriginalName { get; }
        public string FileName { get; }
        

    }
}
