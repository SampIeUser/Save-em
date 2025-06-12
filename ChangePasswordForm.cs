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
    public partial class ChangePasswordForm : Form
    {

        public string OldPassword => textbox_old.Text;
        public string NewPassword => textbox_new1.Text;

        


        public ChangePasswordForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.AcceptButton = button_ok;
            this.CancelButton = button_cancel;

        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textbox_old.Text) ||
            string.IsNullOrWhiteSpace(textbox_new1.Text) ||
            string.IsNullOrWhiteSpace(textbox_new2.Text))
            {
                MessageBox.Show("Fill all fields.");
                return;
            }

            if (textbox_new1.Text != textbox_new2.Text)
            {
                MessageBox.Show("Passwords are not the same.");
                return;
            }

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
