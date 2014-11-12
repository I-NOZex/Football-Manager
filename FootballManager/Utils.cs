using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballManager {
    public class Utils {
        public static string UPLOAD = "~/Content/uploads";

        public static string EncodeFile(string fileName) {
            return @"data:image/gif;base64," + Convert.ToBase64String(System.IO.File.ReadAllBytes(fileName));
        }
    }
}