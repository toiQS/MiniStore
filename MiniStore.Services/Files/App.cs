using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MiniStore.Services.Files
{
    public class App
    {
        public App() { }
        /// <summary>
        /// Gets the current folder.
        /// </summary>
        public string CurrentFolder()
        {
            string s = Assembly.GetCallingAssembly().Location;
            return s.Remove(s.LastIndexOf('\\') + 1);
        }
        public string GetPathCurrentFolder(string folderName, string fileName)
        {
            var assemblyLocation = Assembly.GetEntryAssembly().Location;
            var currentDirectory = Path.GetDirectoryName(assemblyLocation);
            var appRootFolder = currentDirectory.Split("\\bin")[0];

            string path = Path.Combine($"{appRootFolder}", $"{fileName}");
            return path;
            //return appRootFolder;
        }

    }
}
//"F:\\Repo\\Local\\MiniStore\\MiniStore.Services\\coupon\\ICouponService.cs"
//F:\Repo\Local\MiniStore\MiniStore.Test\CouponUnitTest.cs