using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace COVID_DETECT
{
    public partial class UPLOAD : System.Web.UI.Page
    {
        XRayEntities db = new XRayEntities();
        string filePath = "files";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(Server.MapPath("./" + filePath));
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string uploadedFileName = Path.GetFileName(uploadFile.PostedFile.FileName);
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(uploadedFileName);
            string fileFullPath = "./" + filePath + "/" + fileName;
            uploadFile.PostedFile.SaveAs(Server.MapPath(fileFullPath));

            UserFile file = new UserFile();
            file.FileName = fileName;
            file.OriginalFileName = uploadedFileName;
            file.FileUploadedDate = DateTime.Now;
            file.PredictedDate = DateTime.Now;

            db.UserFile.Add(file);
            db.SaveChanges();

            imgFile.ImageUrl = "/" + filePath + "/" + file.FileName;

            lblResult.Text = "File uploaded successfully!<BR>";
        }
    }
}