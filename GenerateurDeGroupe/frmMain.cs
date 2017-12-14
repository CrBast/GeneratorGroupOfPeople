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
            sPath = fileAction.find();
            lblCheminSource.Text = sPath;
            fileAction.write(lblCheminSource.Text, sPath);
        }

        private void panelDragAndDrop_DragDrop(object sender, DragEventArgs e)
        {
            lblCheminSource.Text = e.Data.GetData(DataFormats.Text).ToString();
            ///lblCheminSource.Text = sPath;
        }

        private void panelDragAndDrop_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Link;
        }

        private void btnExportTxt_Click(object sender, EventArgs e)//TEST
        {
            //Juste pour test
            string[] aTest = new string[12];
            List<string> lTest = new List<string>
            {//Affectation des valeurs:
                "test1",
                "Youtube23",
                "Youtube2131",
                "Youtube232",
                "Youtube8756",
                "Youtube0"
            };
            foreach (string sTexte in lTest)//Pour chaques éléments de la liste
            {
                this.dtagrdSource.Rows.Add(sTexte);//Affiche dans une nouvelle ligne
            }
            lblNombrePersonnes.Text = $"Nombre de personnes : {Convert.ToString(dtagrdSource.RowCount - 1)}";//Test
        }

        private void dtagrdSource_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //Juste pour test
            lblNombrePersonnes.Text = $"Nombre de personnes : {Convert.ToString(dtagrdSource.RowCount - 1)}";//Affiche les nombre de lignes de la datagrid
        }

        private void dtagrdSource_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)//Test
        {
            //Juste pour test
            lblNombrePersonnes.Text = $"Nombre de personnes : {Convert.ToString(dtagrdSource.RowCount - 1)}";//Affiche les nombre de lignes de la datagrid
        }
    }
}
