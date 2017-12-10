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
            fileAction.writeConfig(lblCheminSource.Text);
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
    }
}
