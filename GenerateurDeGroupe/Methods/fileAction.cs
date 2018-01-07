using System.Text;
using System.IO;
using System.Windows.Forms;

namespace GenerateurDeGroupe
{
    class FileAction
    {
        //=======================================================================================
        //FindFile
        //Recherche d'un fichier
        // IN   :   -
        // OUT  :   Chemin fichier(string)
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
                sPath = "Veuillez sélectionner un fichier";
            }
            else
            {
                sPath = ofd.FileName;
            }
            return (sPath);
        }

        //===================================================================================================================
        //Write
        //Ecriture d'un fichier
        // IN   :   Chemin du nouveau fichier(string), Contenu(string)
        // OUT  :   -
        public static void Write(string sPath, string sContent)
        {
            FileStream maFileStream = new FileStream(sPath, FileMode.Create);  // ouverture du canal de communication (flux)
            StreamWriter monStreamWriter = new StreamWriter(maFileStream, ASCIIEncoding.Default);  // mode écriture
            monStreamWriter.Write(sContent);
            monStreamWriter.Close();
            maFileStream.Close();
        }

        //=================================================================================================================================
        //FindDirectory
        //Recherche d'un répertoire
        // IN   :   -
        // OUT  :   Chemin du répertoire(string)
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
            if (result == DialogResult.Ignore || result == DialogResult.No ||result == DialogResult.Cancel || result == DialogResult.Abort)
            {
                sPath = "";
            }
            return sPath;
        }
    }
}
