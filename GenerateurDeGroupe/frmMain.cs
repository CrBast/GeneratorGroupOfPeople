using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GenerateurDeGroupe;

namespace Maquette_1
{
    public partial class FrmMain : Form
    {
        string sPath = "";

        public FrmMain()
        {
            InitializeComponent();
        }

        private void ouvrirToolStripMenuItem_Click(object sender, EventArgs e)
        {

            OpenFileDialog op = new OpenFileDialog(); //déclaré un nouveau dialog
            op.ShowDialog();
            lblCheminSource.Text = op.FileName; //affiche le nom de fiche dans lblcheminSource "menu ouvrir"
            btnCSV.Visible = true;
            btnText.Visible = true;
            
           /*
            sPath = fileAction.find();
            lblCheminSource.Text = sPath;*/
        }

        private void panelDragAndDrop_DragDrop(object sender, DragEventArgs e)
        {
            // "Bastien" -> je m'en occupe
        }

        private void panelDragAndDrop_DragEnter(object sender, DragEventArgs e)
        {
            // "Bastien" -> je m'en occupe
        }

        private void btnExportTxt_Click(object sender, EventArgs e)//TEST
        {
            
        }

        private void dtagrdSource_Click(object sender, EventArgs e)
        {
            //lblNombrePersonnes.Text = $"Nombre de personnes : {Convert.ToString(dtagrdSource.RowCount - 1)}";
        }

        private void dtagrdSource_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //lblNombrePersonnes.Text = $"Nombre de personnes : {Convert.ToString(dtagrdSource.RowCount - 1)}";
        }

        private void btnCSV_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable(); //déclaré un nouveau datatable
            dataTable.Columns.Add("Nom"); // déclaré un colume on peut ajout plusieur colums
            string sPath = lblCheminSource.Text;
            StreamReader read = new StreamReader(sPath);
            string[] totalData = new string[File.ReadAllLines(sPath).Length];
            totalData = read.ReadLine().Split(';');
            var totalline = totalData[0].Split(';').Length; // counter de nombre de rows
            while (!read.EndOfStream)
            {

                totalData = read.ReadLine().Split(';'); //le boucle de lire de fichier CSV
                for (int i = 0; i < totalline; i++)
                {
                    dataTable.Rows.Add(totalData[i]);

                }

            }
            dtagrdSource.DataSource = dataTable; // affichage dataTable dans datagridview
            
        }
    }
}
