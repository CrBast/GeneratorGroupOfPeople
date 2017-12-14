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
            lblNombrePersonnes.Text = $"Nombre de personnes : {Convert.ToString(dtagrdSource.RowCount - 1)}";
        }

        private void dtagrdSource_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            lblNombrePersonnes.Text = $"Nombre de personnes : {Convert.ToString(dtagrdSource.RowCount - 1)}";
        }
    }
}
