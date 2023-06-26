using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaidalyTechMain.Shared
{
    public class FileServices
    {
        // string thisPath = Path.Combine(Directory.GetCurrentDirectory().Replace(@"bin", @"Files")).Replace("Debug", "Images");
        string imagePath = Path.Combine(Directory.GetCurrentDirectory() + "\\Files\\Image");

        public string AddFile(OpenFileDialog fileDialog)
        {

            //string thisPath = Path.Combine(Directory.GetCurrentDirectory().Replace(@"bin", @"Files")).Replace("Debug", "Images");
            try
            {
                string ImageLocation = fileDialog.FileName;//get main file location 
                var fileExtension = Path.GetExtension(fileDialog.SafeFileName);
                var newFileName = String.Concat(Guid.NewGuid(), fileExtension); // new name to the file to copy 
                var filepath = imagePath + $@"\{newFileName}"; //new file path that gonna stored in 

                if (fileDialog.CheckFileExists)
                {
                    File.Copy(ImageLocation, filepath);//copying file 
                                                       // Image img = Image.FromFile(filepath);
                                                       ////  var newImage = ScaleImage(img, 1100, 450);
                                                       // img.Dispose();
                                                       //  newImage.Save(filepath);

                }
                return newFileName;
            }
            catch (Exception)
            {
                MessageBox.Show("برجاء تشغيل البرنامج كـ أدمن ليتم السماح له بالتعامل مع الملفات", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return "";
            }
        }

        public string GetImage(string name)
        {
            return imagePath + $@"\{name}";
        }

    }
}
