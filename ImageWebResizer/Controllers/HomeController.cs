using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ImageWebResizer.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using ImageMagick;

namespace ImageWebResizer.Controllers
{
    public class HomeController : Controller
    {
        private readonly string filePath = Path.GetTempPath();

        public IActionResult Index()
        {
            using (var db = new StoreContext())
            {
                ViewData["historic"] = db.Pictures.OrderByDescending(x=>x.DateUpload).Take(50).ToList();
            };
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Index(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);
            var stored = new List<Picture>();
            foreach (var formFile in files)
            {
                if (formFile.Length > 0 && (new string[] { ".JPG",".JPEG",".BMP", ".PNG" }).Contains(Path.GetExtension(formFile.FileName).ToUpper()))
                {
                    var filename = "img" + getRandomFilename(formFile.FileName);
                    var pathname = filePath + filename;
                    using (var stream = new FileStream(pathname, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    //----dimensions
                    int _height =0;
                    int _width =0;

                    using (MagickImage image = new MagickImage(pathname))
                    {
                        _height = image.Height;
                        _width = image.Width;
                        
                    }
                    //----
                    
                    using (var db = new StoreContext())
                    {
                        var p = new Picture
                        {
                            Id = filename.Replace(".", ""),
                            DateUpload = DateTime.Now,
                            OriginalName = formFile.FileName,
                            FileName = filename,
                            Name = formFile.Name,
                            Length = formFile.Length,
                            Width = _width,
                            Height = _height,
                            PathOriginal = pathname
                        };
                        db.Pictures.Add(p);
                        db.SaveChanges();
                        stored.Add(p);
                    }
                }
            }

            return Ok(new { result = true, stored });
        }

        public IActionResult ResizeTo300(string id)
        {

            using (var db = new StoreContext())
            {
                Picture picture = db.Pictures.FirstOrDefault(x => x.Id.Equals(id));

                if (picture == null)
                    return Ok(new { result = false });

                // Read from file
                using (MagickImage image = new MagickImage(picture.PathOriginal))
                {
                    MagickGeometry size = new MagickGeometry(0, 300)
                    {
                        IgnoreAspectRatio = false
                    };

                    image.Resize(size);
                    picture.Path300 = filePath + "300" + id;
                    // Save the result
                    image.Write(picture.Path300);
                }


                picture.Length300 = new FileInfo(picture.Path300).Length;
                db.SaveChanges();
                return Ok(new { result = true, picture });
            }

        }

        public IActionResult GetImage(string id, int size=0)
        {
            using (var db = new StoreContext())
            {
                PhysicalFileResult file;
                Picture p = db.Pictures.FirstOrDefault(x => x.Id.Equals(id));

                if (p == null)
                    return Ok("Invalid entry.");

                string path;
                switch (size)
                {
                    case 0:
                        path = p.PathOriginal;
                        break;
                    case 300:
                        path = p.Path300;
                        break;
                    default:
                        return Ok("Invalid entry.");
                }

                if (System.IO.File.Exists(path)) { 
                file = PhysicalFile(path, System.Net.Mime.MediaTypeNames.Image.Jpeg);

                    return file;
                }
                else
                {
                    return Ok("File does not exist in the file system.");
                }


            }
        }



        public IActionResult About()
        {
            return View();
        }
        

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }





        //--------------------------
        private string getRandomFilename(string name)
        {
            string path = Path.GetRandomFileName();
            path = path.Replace(".", "");
            path += Path.GetExtension(name);
            return path;
        }


    }
}
