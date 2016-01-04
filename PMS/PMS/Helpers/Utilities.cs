using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace PMS.Helpers
{
    public static class Utilities
    {
        public static string saveFile(HttpPostedFileBase file, string Directory)
        {
            string path = String.Empty;
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                // string Root = System.Web.HttpContext.Current.Server.MapPath(@"~/App_Data/" + Directory);
                string Root = Path.Combine(ConfigurationManager.AppSettings["StoragePath"], Directory);
                if (!System.IO.Directory.Exists(Root))
                {
                    System.IO.Directory.CreateDirectory(Root);
                }
                path = Path.Combine(Root, fileName);
                file.SaveAs(path);
            }
            return path;
        }
    }
}