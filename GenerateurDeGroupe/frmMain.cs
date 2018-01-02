using System;
using System.ComponentModel;
using System.Data;
using System.Text;
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
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (string sPath in FileList)
            {
                lblCheminSource.Text = sPath;
            }
            GetValueToDTV(lblCheminSource.Text);
        }

        private void panelDragAndDrop_DragEnter(object sender, DragEventArgs e)
        {
            // "Bastien" -> je m'en occupe
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy; // Okay
            else
                e.Effect = DragDropEffects.None; // Unknown data, ignore it
        }

        private void btnExportTxt_Click(object sender, EventArgs e)//TEST
        {
            string sPath = FileAction.FindDirectory();//Recherche de répertoire d'enregistrement
            if (sPath != "")
            {
                string sTemp = DGVToString(dtagrdResultat, "\t");
                DateTime time = DateTime.Now;
                FileAction.Write($"{sPath}\\{time.ToString("dd.MM.yyyy_HH'h'mm")}_ExportGroupes.txt", sTemp);
            } 
        }
        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            string sPath = FileAction.FindDirectory();//Recherche de répertoire d'enregistrement
            if (sPath != "")
            {
                string sTemp = DGVToString(dtagrdResultat, ";");
                DateTime time = DateTime.Now;
                FileAction.Write($"{sPath}\\{time.ToString("dd.MM.yyyy_HH'h'mm")}_ExportGroupes.csv", sTemp);
            }
        }

        private void dtagrdSource_Click(object sender, EventArgs e)
        {
            iNbrPersonne = dtagrdSource.RowCount - 1;//Affecte le nombre de personnes
            lblNombrePersonnes.Text = $"Nombre de personnes : {iNbrPersonne}";
            btnValidationSource.Enabled = true;
            dtagrdSource.Update();//Mise à jour de la datatable source
        }

        private void dtagrdSource_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            iNbrPersonne = dtagrdSource.RowCount - 1;//Affecte le nombre de personnes
            lblNombrePersonnes.Text = $"Nombre de personnes : {iNbrPersonne}";
            btnValidationSource.Enabled = true;
            dtagrdSource.Update();//Mise à jour de la datatable source


        }

        private void btnouvrir_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            dataTable = ((DataTable)dtagrdSource.DataSource).Copy();
            dataTable.Clear();
            string sPath = "";
            sPath = FileAction.FindFile();
            if (sPath == "Veuillez sélectionner un fichier")
            {
                lblCheminSource.Text = "Veuillez sélectionner un fichier";
                return;//Si égal à 0 alors ne fait pas la suite
            }
            lblCheminSource.Text = sPath;//Affichage du chemin dans le label

            GetValueToDTV(sPath);
        }

        private void btnValidationSource_Click(object sender, EventArgs e)
        {
            int iMode = 0;
            int iNbrPersonneParGroupe = 1;
            int iNbrGroupe = 1;
            dtagrdSource.Update();
            DataTable dataTable = new DataTable();
            dataTable = ((DataTable)dtagrdSource.DataSource).Copy();
            if (rdobtnNombreDeGroupes.Checked == true && chkboxAleatoire.Checked == true) { iMode = 1; }
            if (rdobtnNombreDeGroupes.Checked == true && chkboxCroissant.Checked == true) { iMode = 2; }
            if (rdobtnPersonnesParGroupe.Checked == true && chkboxAleatoire.Checked == true) { iMode = 3; }
            if (rdobtnPersonnesParGroupe.Checked == true && chkboxCroissant.Checked == true) { iMode = 4; }
            if (Convert.ToInt32(tbxNombrePersonneParGroupe.Text) < 1) { iNbrPersonneParGroupe = 1; } else { iNbrPersonneParGroupe = Convert.ToInt32(tbxNombrePersonneParGroupe.Text); }
            if (Convert.ToInt32(tbxNombreGroupes.Text) < 1) { iNbrGroupe = 1; } else { iNbrGroupe = Convert.ToInt32(tbxNombreGroupes.Text); }


            CreationGroupe(dataTable, iNbrGroupe, iNbrPersonne, iNbrPersonneParGroupe, iMode);
            btnExportCsv.Enabled = true;
            btnExportTxt.Enabled = true;
        }

        public void CreationGroupe(DataTable dataTableSource, int iNbrGroupe, int NbrPersonne, int iNbrPersonneParGroupe, int iMode)
        {
            //Mode 1 : Se base sur le nombre de groupe                          :   aléatoire
            //Mode 2 : Se base sur le nombre de groupe                          :   distribution de carte
            //Mode 3 : Se base sur le nombre de personne par groupe             :   aléatoire
            //Mode 4 : Se base sur le nombre de personne par groupe             :   distribution de carte

            int iNumGroupe = 1;
            DataTable dataTableResultat = new DataTable();
            dataTableResultat.Columns.Add("IndexHidden", typeof(int));//Cette colonne sert d'index contenant seulement le numéro du groupe
            dataTableResultat.Columns.Add("Groupe");
            dataTableResultat.Columns.Add("Personne");
            Random rndm = new Random();


            switch (iMode)
            {
                case 1:
                    //Méthode pour création groupe avec le nombre de groupe
                    for (int i = 0; i < iNbrPersonne; i++)
                    {
                        int iNbrAlea = rndm.Next(0, dataTableSource.Rows.Count);
                        if (iNumGroupe > iNbrGroupe)
                        {
                            iNumGroupe = 1;
                        }
                        DataRow row = dataTableResultat.NewRow();
                        row["IndexHidden"] = Convert.ToSingle(iNumGroupe);
                        row["Groupe"] = "Groupe " + iNumGroupe;
                        row["Personne"] = dataTableSource.Rows[iNbrAlea].Field<string>(0);
                        dataTableResultat.Rows.Add(row);
                        dataTableSource.Rows[iNbrAlea].Delete();
                        iNumGroupe++;
                    }
                    break;
                case 2:
                    for (int i = 0; i < iNbrPersonne; i++)
                    {
                        if (iNumGroupe > iNbrGroupe)
                        {
                            iNumGroupe = 1;
                        }
                        DataRow row = dataTableResultat.NewRow();
                        row["IndexHidden"] = Convert.ToSingle(iNumGroupe);
                        row["Groupe"] = "Groupe " + iNumGroupe;
                        row["Personne"] = dataTableSource.Rows[i].Field<string>(0);
                        dataTableResultat.Rows.Add(row);
                        iNumGroupe++;
                    }
                    break;
                case 3:
                    foreach (var SertARien in dtagrdSource.Rows)
                    {
                        for (int i = 0; i < iNbrPersonneParGroupe; i++)
                        {
                            try
                            {
                                int iNbrAlea = rndm.Next(0, dataTableSource.Rows.Count);
                                DataRow row = dataTableResultat.NewRow();
                                row["IndexHidden"] = Convert.ToSingle(iNumGroupe);
                                row["Groupe"] = "Groupe " + iNumGroupe;
                                row["Personne"] = dataTableSource.Rows[iNbrAlea].Field<string>(0);
                                dataTableResultat.Rows.Add(row);
                                dataTableSource.Rows[iNbrAlea].Delete();
                            }
                            catch { }
                        }
                        iNumGroupe++;
                    }
                    break;
                case 4:
                    iNbrGroupe = (int) iNbrPersonne / iNbrPersonneParGroupe + 1;
                    for (int i = 0; i < iNbrPersonne; i++)
                    {
                        if (iNumGroupe > iNbrGroupe)
                        {
                            iNumGroupe = 1;
                        }
                        DataRow row = dataTableResultat.NewRow();
                        row["IndexHidden"] = Convert.ToSingle(iNumGroupe);
                        row["Groupe"] = "Groupe " + iNumGroupe;
                        row["Personne"] = dataTableSource.Rows[i].Field<string>(0);
                        dataTableResultat.Rows.Add(row);
                        iNumGroupe++;
                    }
                    break;
            }
            dtagrdResultat.DataSource = dataTableResultat;
            dtagrdResultat.Columns[0].Visible = false; //Cette colonne sert d'index contenant seulement le numéro du groupe
            this.dtagrdResultat.Sort(this.dtagrdResultat.Columns["IndexHidden"], ListSortDirection.Ascending);
            dtagrdResultat.Columns[2].Width = 176;
            dtagrdResultat.Columns[1].Width = 66;
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
        public static string DGVToString(DataGridView dgv, string delimiter)
        {
            int iRows = 0;
            StringBuilder sb = new StringBuilder();
            foreach (DataGridViewRow row in dgv.Rows)
            {
                for (int i = 1; i < 3; i++)
                {
                    sb.Append(dgv.Rows[iRows].Cells[i].Value);
                    sb.Append(delimiter);
                }
                sb.Remove(sb.Length - 1, 1); // Removes the last delimiter 
                sb.AppendLine();
                iRows++;
            }
            return sb.ToString();
        }
        public void GetValueToDTV( string sPath)
        {
            DataTable dataTable = new DataTable();
            dataTable = ((DataTable)dtagrdSource.DataSource).Copy();
            dataTable.Clear();
            string sline;
            this.Refresh();
            System.IO.StreamReader streamCsv = new System.IO.StreamReader(lblCheminSource.Text);
            while ((sline = streamCsv.ReadLine()) != null)//Test si le contenu est vide
            {
                dataTable.Rows.Add(sline);//Ajoute les valeurs ligne par ligne
            }
            streamCsv.Close();//Fermeture de la stream

            dtagrdSource.DataSource = dataTable;
            iNbrPersonne = dtagrdSource.RowCount - 1;//Affecte le nombre de personnes
            lblNombrePersonnes.Text = $"Nombre de personnes : {iNbrPersonne}";
            btnValidationSource.Enabled = true;
        }

        private void deleteSourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            dataTable = ((DataTable)dtagrdSource.DataSource).Copy();
            dataTable.Clear();
            dtagrdSource.DataSource = dataTable;
            btnValidationSource.Enabled = false;
            lblCheminSource.Text = "Veuillez sélectionner un fichier";
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void tbxNombrePersonneParGroupe_Click(object sender, EventArgs e)
        {
            rdobtnPersonnesParGroupe.Checked = true;
            rdobtnNombreDeGroupes.Checked = false;
        }

        private void tbxNombreGroupes_Click(object sender, EventArgs e)
        {
            rdobtnPersonnesParGroupe.Checked = false;
            rdobtnNombreDeGroupes.Checked = true;
        }

        private void tbxNombrePersonneParGroupe_Validated(object sender, EventArgs e)
            try{ Convert.ToInt32(tbxNombrePersonneParGroupe.Text); }
            catch{ tbxNombrePersonneParGroupe.Text = "3"; }
        }

        private void tbxNombreGroupes_Validated(object sender, EventArgs e)
        {
            try{ Convert.ToInt32(tbxNombreGroupes.Text); }
            catch{ tbxNombreGroupes.Text = "3"; }
        }
    }
}