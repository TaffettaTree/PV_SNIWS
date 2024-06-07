using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quick_script
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IList<string> names = new List<string>();
            string directory = @"C:\Users\mikol\Desktop\SNIWS\Weather_script\Symbols\";
            foreach (string file in Directory.EnumerateFiles(directory,"*m.png"))
            {
                File.Delete(file);
            }


            foreach (string file in Directory.EnumerateFiles(directory))
            {
                names.Add(file);
            }
            int i = 0;
            string message = "";

            foreach (string name in names)
            {

                i++;

                File.Move(name, directory + "weather_"+ i.ToString()+".png");
                message += "name: " + name + " || to: weather_" + i + "\n"; 
            }
            MessageBox.Show(message);
            

        }
    }
}
