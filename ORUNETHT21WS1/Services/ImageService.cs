using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Services {
    public class ImageService {

        public static string GetImagePathRelative(string imagepath) {
            return $"/UploadedImages/{imagepath}";
        }

        public string SaveImageToDisk(HttpPostedFileBase httpPostedFile) {
            var imagefolder = HttpContext.Current.Server.MapPath("~/UploadedImages");
            //vi lägger på 4st random bokstäver före så att varje fil blir unik, annars kan de krocka med samma filnamn
            var filename = Guid.NewGuid().ToString().Substring(0, 4) + httpPostedFile.FileName;
            var fullpath = Path.Combine(imagefolder, filename);
            //dubbelkolla så mappen finns att spara ner i.
            if (!Directory.Exists(imagefolder)) Directory.CreateDirectory(imagefolder);
            httpPostedFile.SaveAs(fullpath);
            //returnera en path
            return filename;
        }

        public bool RemoveImageFromDiskIfExists(string imagefilename) {
            var imagefolder = HttpContext.Current.Server.MapPath("~/UploadedImages");
            var fullpath = Path.Combine(imagefolder, imagefilename);
            try {
                if (File.Exists(fullpath)) {
                    File.Delete(fullpath);
                    return true;
                }

                return false;
            } catch {
                return false;
            }
        }
    }
}
