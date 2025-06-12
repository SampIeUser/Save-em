namespace SV_Passer
{
    partial class form_login
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button_enter = new Button();
            textBox_login = new TextBox();
            label_char_count = new Label();
            pictureBox_login = new PictureBox();
            button_create_account = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox_login).BeginInit();
            SuspendLayout();
            // 
            // button_enter
            // 
            button_enter.Location = new Point(100, 70);
            button_enter.Name = "button_enter";
            button_enter.Size = new Size(80, 30);
            button_enter.TabIndex = 0;
            button_enter.Text = "Enter";
            button_enter.UseVisualStyleBackColor = true;
            button_enter.Click += button_enter_Click;
            // 
            // textBox_login
            // 
            textBox_login.BackColor = Color.White;
            textBox_login.BorderStyle = BorderStyle.FixedSingle;
            textBox_login.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            textBox_login.Location = new Point(12, 36);
            textBox_login.Name = "textBox_login";
            textBox_login.Size = new Size(260, 25);
            textBox_login.TabIndex = 1;
            textBox_login.TextChanged += textBox_login_TextChanged;
            // 
            // label_char_count
            // 
            label_char_count.AutoSize = true;
            label_char_count.Location = new Point(84, 9);
            label_char_count.Name = "label_char_count";
            label_char_count.Size = new Size(96, 15);
            label_char_count.TabIndex = 2;
            label_char_count.Text = "Symbol count: 0 ";
            // 
            // pictureBox_login
            // 
            pictureBox_login.Location = new Point(186, 67);
            pictureBox_login.Name = "pictureBox_login";
            pictureBox_login.Size = new Size(61, 45);
            pictureBox_login.TabIndex = 3;
            pictureBox_login.TabStop = false;
            pictureBox_login.Click += pictureBox_login_Click;
            // 
            // button_create_account
            // 
            button_create_account.Location = new Point(-2, 89);
            button_create_account.Name = "button_create_account";
            button_create_account.Size = new Size(60, 23);
            button_create_account.TabIndex = 4;
            button_create_account.Text = "Create";
            button_create_account.UseVisualStyleBackColor = true;
            button_create_account.Click += button_create_account_Click;
            // 
            // form_login
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(284, 111);
            Controls.Add(button_create_account);
            Controls.Add(pictureBox_login);
            Controls.Add(label_char_count);
            Controls.Add(textBox_login);
            Controls.Add(button_enter);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "form_login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SV Passer";
            Shown += form_login_Shown;
            ((System.ComponentModel.ISupportInitialize)pictureBox_login).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button_enter;
        private TextBox textBox_login;
        private Label label_char_count;
        private PictureBox pictureBox_login;
        private Button button_create_account;
    }
}
