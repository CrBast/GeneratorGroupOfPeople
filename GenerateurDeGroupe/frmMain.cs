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
            int iMode = 0;
            dtagrdSource.Update();
            DataTable dataTable = new DataTable();
            dataTable = ((DataTable)dtagrdSource.DataSource).Copy();
            if (rdobtnNombreDeGroupes.Checked == true && chkboxAleatoire.Checked == true) { iMode = 1; }
            if (rdobtnNombreDeGroupes.Checked == true && chkboxCroissant.Checked == true) { iMode = 2; }
            if (rdobtnPersonnesParGroupe.Checked == true && chkboxAleatoire.Checked == true) { iMode = 3; }
            if (rdobtnPersonnesParGroupe.Checked == true && chkboxCroissant.Checked == true) { iMode = 4; }


            CreationGroupe(dataTable, Convert.ToInt32(tbxNombreGroupes.Text), iNbrPersonne, 0, iMode);
        }

        public void CreationGroupe(DataTable dataTableSource, int iNbrGroupe, int NbrPersonne, int NbrPersonneParGroupe, int iMode)
        {
            //Mode 1 : Se base sur le nombre de groupe                          :   aléatoire
            //Mode 2 : Se base sur le nombre de groupe                          :   distribution de carte
            //Mode 3 : Se base sur le nombre de personne par groupe             :   aléatoire
            //Mode 4 : Se base sur le nombre de personne par groupe             :   distribution de carte

            int i = 1;
            DataTable dataTableRelultat = new DataTable();
            dataTableRelultat.Columns.Add("IndexHidden");//Cette colonne sert d'index contenant seulement le numéro du groupe
            dataTableRelultat.Columns.Add("Groupe");
            dataTableRelultat.Columns.Add("Personne");
            Random rndm = new Random();

            if (iMode == 1)
            //Méthode pour création groupe avec le nombre de groupe
            {
                for (int i2 = 0; i2 < iNbrPersonne; i2++)
                {
                    int iNbrAlea = rndm.Next(0, dataTableSource.Rows.Count);
                    if (i > iNbrGroupe)
                    {
                        i = 1;
                    }
                    DataRow row = dataTableRelultat.NewRow();
                    row["IndexHidden"] = Convert.ToSingle(i);
                    row["Groupe"] = "Groupe " + i;
                    row["Personne"] = dataTableSource.Rows[iNbrAlea].Field<string>(0);
                    dataTableRelultat.Rows.Add(row);
                    dataTableSource.Rows[iNbrAlea].Delete();
                    i++;
                }
            }
            if (iMode == 2)
            {

            }
            if (iMode == 3)
            {

            }
            if (iMode == 4)
            {

            }

            dtagrdResultat.DataSource = dataTableRelultat;
            dtagrdResultat.Columns[0].Visible = false; //Cette colonne sert d'index contenant seulement le numéro du groupe
        }

        private void effacerContenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void chkboxAleatoire_Click(object sender, EventArgs e)
        {
            chkboxAleatoire.Checked = true;
            chkboxCroissant.Checked = false;
        }

        private void chkboxCroissant_Click(object sender, EventArgs e)
        {
            chkboxAleatoire.Checked = false;
            chkboxCroissant.Checked = true;
        }
    }
}
