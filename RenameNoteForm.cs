using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SV_Passer
{
    public partial class RenameNoteForm : Form
    {
        public string NewTitle { get; private set; }
        public RenameNoteForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.AcceptButton = button_ok;
            this.CancelButton = button_cancel;

            textBox_newTitle.Text ="";
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox_newTitle.Text))
            {
                MessageBox.Show("name can't be empty.");
                return;
            }

            NewTitle = textBox_newTitle.Text.Trim();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
