using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

namespace CompiPascal
{
    public static class FileDotEngine
    {
        public static Bitmap Run(string dot, string nameGraph)
        {
            //@"..\..\input", "entrada.txt"
            //string executable = @"..\..\lib\dot.exe";
            string output = @"..\..\..\output\" + nameGraph;
            File.WriteAllText(output, dot);

            System.Diagnostics.Process process = new System.Diagnostics.Process();

            // Stop the process from opening a new window
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            // Setup executable and parameters
            process.StartInfo.FileName = "dot.exe";
            process.StartInfo.Arguments = string.Format(@"{0} -Tjpg -O", output);

            // Go
            process.Start();

            // and wait dot.exe to complete and exit
            process.WaitForExit();

            Bitmap bitmap = null; ;
            using (Stream bmpStream = File.Open(output + ".jpg", System.IO.FileMode.Open))
            {
                Image image = Image.FromStream(bmpStream);
                bitmap = new Bitmap(image);
            }
            //File.Delete(output);
            //File.Delete(output + ".jpg");
            return bitmap;
        }
    }
}
