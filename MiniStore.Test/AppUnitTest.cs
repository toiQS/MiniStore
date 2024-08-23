using MiniStore.Services.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStore.Test
{
    class AppUnitTest
    {
       private App _app;
        [SetUp]
        public void Setup()
        {
            _app = new App();
        }
        [Test]
        public void Test()
        {
            var result = _app.GetPathCurrentFolder("folder1","file1");
            Console.WriteLine(result);
        }
    }
}
