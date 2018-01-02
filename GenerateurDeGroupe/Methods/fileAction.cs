using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace GenerateurDeGroupe
{
    class FileAction
    {
        public static string FindFile()
        {
            string sPath = "";
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = "c:\\";                     
            ofd.Filter = "txt files (*.txt)|*.txt|csv files (*.csv*)|*.csv*|all files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;
            ofd.ShowDialog();
            if (ofd.FileName == "")
            {
                sPath = "Aucun fichier sélectionné";
            }
            else
            {
                sPath = ofd.FileName;
            }
            return (sPath);
        }


        public static void Write(string sPath, string sContent)
        {
            FileStream maFileStream = new FileStream(sPath, FileMode.Create);  // ouverture du canal de communication (flux)
            StreamWriter monStreamWriter = new StreamWriter(maFileStream, ASCIIEncoding.Default);  // mode écriture
            monStreamWriter.Write(sContent);
            monStreamWriter.Close();
            maFileStream.Close();
        }

        public static void WriteConfig(string sConfigPath)//TODO
        {
            FileStream maFileStream = new FileStream(@"./config.conf", FileMode.Create);  // ouverture du canal de communication (flux)
            StreamWriter monStreamWriter = new StreamWriter(maFileStream, ASCIIEncoding.Default);  // mode écriture
            monStreamWriter.Write(sConfigPath);
            monStreamWriter.Close();
            maFileStream.Close();
        }

        public static string FindDirectory()
        {
            string sPath = "";
            FolderBrowserDialog fbdPath = new FolderBrowserDialog();
            DialogResult result = fbdPath.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbdPath.SelectedPath))
            {
                string[] files = Directory.GetFiles(fbdPath.SelectedPath);

                sPath = fbdPath.SelectedPath.ToString();
            }
            return sPath;
        }
    }
}
