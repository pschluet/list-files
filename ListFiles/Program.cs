using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ListFiles
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // Get directory to analyze
            String dir = GetDirectory();

            if (dir != null)
            {
                string[] allFiles = Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories);

                using (StreamWriter file = new StreamWriter(@"files.txt"))
                {
                    foreach (string filePath in allFiles)
                    {
                        if (filePath.EndsWith(".zip"))
                        {
                            ListZipFileContents(file, filePath, dir);
                        }
                        else
                        {
                            file.WriteLine(filePath.Replace(dir + "\\",""));
                        }                        
                    }
                }                    
            }
        }

        static void ListZipFileContents(StreamWriter file, String filePath, String rootDir)
        {
            ZipInputStream zip = new ZipInputStream(File.OpenRead(filePath));
            ZipEntry item;
            while ((item = zip.GetNextEntry()) != null)
            {
                file.WriteLine(filePath.Replace(rootDir + "\\","") + "\\" + item.Name.Replace('/','\\'));
            }
        }

        static String GetDirectory()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                return fbd.SelectedPath;
            }
            return null;
        }
    }
}
