using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageResizer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string sourcePath = Path.Combine(Environment.CurrentDirectory, "images");
            string destinationPath = Path.Combine(Environment.CurrentDirectory, "output"); ;

            ImageProcess imageProcess = new ImageProcess();

            imageProcess.Clean(destinationPath);
            List<long> first_list = new List<long>();
            List<long> last_list = new List<long>();
            long first_avg = 0;
            long last_avg = 0;
            for (int i = 0; i < 3; i++)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                await imageProcess.ResizeImagesAsync(sourcePath, destinationPath, 2.0);
                sw.Stop();
                Console.WriteLine($"花費時間: {sw.ElapsedMilliseconds} ms");
                first_list.Add(sw.ElapsedMilliseconds);
            }
            
            Console.WriteLine($"非同步平均------------------------------{first_list.Average()}ms");
            for (int i = 0; i < 3; i++)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                imageProcess.ResizeImages(sourcePath, destinationPath, 2.0);
                sw.Stop();
                Console.WriteLine($"花費時間: {sw.ElapsedMilliseconds} ms");
                last_list.Add(sw.ElapsedMilliseconds);
            }
            Console.WriteLine($"同步平均------------------------------{last_list.Average()}ms");

            Console.WriteLine($"提升{(last_list.Average()- first_list.Average())/ last_list.Average() * 100}%");
        }
    }
}
