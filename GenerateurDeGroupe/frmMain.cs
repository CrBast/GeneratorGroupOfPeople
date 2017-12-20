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
        public FrmMain()
        {
            InitializeComponent();
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
            lblNombrePersonnes.Text = $"Nombre de personnes : {Convert.ToString(dtagrdSource.RowCount - 1)}"; ;//Compte et affiche le nombre de personnes dans la datagrid
            btnValidationSource.Visible = true;
        }

        private void dtagrdSource_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            lblNombrePersonnes.Text = $"Nombre de personnes : {Convert.ToString(dtagrdSource.RowCount - 1)}"; ;//Compte et affiche le nombre de personnes dans la datagrid
            btnValidationSource.Visible = true;
        }

        private void btnImportCSV_Click(object sender, EventArgs e)
        {
            string sPath = "";
            DataTable mainDataTable = new DataTable(); //Déclaration d'une DataTable
            mainDataTable.Columns.Add("Personne"); // déclaré un colume on peut ajout plusieur colums
            sPath = FileAction.FindFile(0);/*0 = csv / 1 = txt*/
            if (sPath == "Aucun fichier sélectionné")
            {
                lblCheminSource.Text = "Aucun fichier sélectionné";
                return;//Si égal à 0 alors ne fait pas la suite
            }
            lblCheminSource.Text = sPath;//Affichage du chemin dans le label

            dtagrdSource.Columns.Remove("Personne");//Supprimme la colonne "Personne" dans dtagrdSource
            dtagrdSource.DataSource = mainDataTable; // affichage dataTable dans datagridview
            dtagrdSource.Columns[0].Width = 176; //Agrandissement de la taille des cellules datagridview 

            string sline;
            System.IO.StreamReader streamCsv = new System.IO.StreamReader(sPath);
            while ((sline = streamCsv.ReadLine()) != null)//Test si le contenu est vide
            {
                mainDataTable.Rows.Add(sline);//Ajoute les valeurs ligne par ligne
            }
            streamCsv.Close();//Fermeture de la stream

            dtagrdSource.DataSource = mainDataTable; // affichage dataTable dans datagridview
            lblNombrePersonnes.Text = $@"Nombre de personnes : {Convert.ToString(dtagrdSource.RowCount - 1)}";//Compte et affiche le nombre de personnes dans la datagrid
        }
    }
}
