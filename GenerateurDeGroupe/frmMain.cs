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
        int iNbrPersonne = 0;
        public FrmMain()
        {
            InitializeComponent();
            DataTable mainDataTable = new DataTable(); //Déclaration d'une DataTable
            mainDataTable.Columns.Add("Personne"); // déclaré un colume on peut ajout plusieur colums
            dtagrdSource.DataSource = mainDataTable;
            dtagrdSource.Columns[0].Width = 176; //Agrandissement de la taille des cellules datagridview
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
            iNbrPersonne = dtagrdSource.RowCount - 1;//Affecte le nombre de personnes
            lblNombrePersonnes.Text = $"Nombre de personnes : {iNbrPersonne}";
            btnValidationSource.Visible = true;
            dtagrdSource.Update();//Mise à jour de la datatable source
        }

        private void dtagrdSource_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            iNbrPersonne = dtagrdSource.RowCount - 1;//Affecte le nombre de personnes
            lblNombrePersonnes.Text = $"Nombre de personnes : {iNbrPersonne}";
            btnValidationSource.Visible = true;
            dtagrdSource.Update();//Mise à jour de la datatable source


        }

        private void btnouvrir_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            dataTable = ((DataTable)dtagrdSource.DataSource).Copy();
            dataTable.Clear();
            string sPath = "";
            sPath = FileAction.FindFile();
            if (sPath == "Aucun fichier sélectionné")
            {
                lblCheminSource.Text = "Aucun fichier sélectionné";
                return;//Si égal à 0 alors ne fait pas la suite
            }
            lblCheminSource.Text = sPath;//Affichage du chemin dans le label
            
            string sline;
            this.Refresh();
            System.IO.StreamReader streamCsv = new System.IO.StreamReader(sPath);
            while ((sline = streamCsv.ReadLine()) != null)//Test si le contenu est vide
            {
                dataTable.Rows.Add(sline);//Ajoute les valeurs ligne par ligne
            }
            streamCsv.Close();//Fermeture de la stream

            dtagrdSource.DataSource = dataTable;
            iNbrPersonne = dtagrdSource.RowCount - 1;//Affecte le nombre de personnes
            lblNombrePersonnes.Text = $"Nombre de personnes : {iNbrPersonne}";
            btnValidationSource.Visible = true;
        }

        private void btnValidationSource_Click(object sender, EventArgs e)
        {
            dtagrdSource.Update();
            DataTable dataTable = new DataTable();
            dataTable = ((DataTable)dtagrdSource.DataSource).Copy();

            CreationGroupe(dataTable, Convert.ToInt32(tbxNombreGroupes.Text), iNbrPersonne, 0);
        }

        public void CreationGroupe(DataTable dataTableSource, int iNbrGroupe, int NbrPersonne, int NbrPersonneParGroupe)
        {
            DataTable dataTableRelultat = new DataTable();
            dataTableRelultat.Columns.Add("Groupe");
            dataTableRelultat.Columns.Add("Personne");
            Random rndm = new Random();            
            for (int i = 0; i <= NbrPersonne -1; i++)
            {
                DataRow row = dataTableRelultat.NewRow();
                int iNbr = rndm.Next(1, iNbrGroupe + 1);
                row["Groupe"] = "Groupe " + iNbr;
                row["Personne"] = dataTableSource.Rows[i].Field<string>(0);
                dataTableRelultat.Rows.Add(row);
            }
            
            dtagrdResultat.DataSource = dataTableRelultat;
            dtagrdResultat.Sort(this.dtagrdResultat.Columns["Groupe"], ListSortDirection.Ascending);
        }

        private void effacerContenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
    }
}
