using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace GenerateurDeGroupe
{
    class fileAction
    {
        public static string find()
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
            return (sPath);
        }


        public static void write(string sPath, string sContent)
        {
            FileStream maFileStream = new FileStream(sPath, FileMode.Create);  // ouverture du canal de communication (flux)
            StreamWriter monStreamWriter = new StreamWriter(maFileStream, ASCIIEncoding.Default);  // mode écriture
            monStreamWriter.Write(sContent);
            monStreamWriter.Close();
            maFileStream.Close();
        }

        public static string read(string sPath)
        {
            // TO DO
            string sContentText = "";
            return(sContentText);
        }

        public static void writeConfig(string sConfigPath)// to do
        {
            FileStream maFileStream = new FileStream(@"./config.conf", FileMode.Create);  // ouverture du canal de communication (flux)
            StreamWriter monStreamWriter = new StreamWriter(maFileStream, ASCIIEncoding.Default);  // mode écriture
            monStreamWriter.Write(sConfigPath);
            monStreamWriter.Close();
            maFileStream.Close();
        }

        public static string findDirectory()
        {//ToDo
            string sTemp = "";
            return sTemp;
        }
    }
}
