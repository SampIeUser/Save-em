namespace SV_Passer
{
    partial class account_creation_form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button_confirm_creation = new Button();
            textbox_pass_create_1 = new TextBox();
            textbox_pass_create_2 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // button_confirm_creation
            // 
            button_confirm_creation.Location = new Point(12, 113);
            button_confirm_creation.Name = "button_confirm_creation";
            button_confirm_creation.Size = new Size(207, 32);
            button_confirm_creation.TabIndex = 0;
            button_confirm_creation.Text = "Create";
            button_confirm_creation.UseVisualStyleBackColor = true;
            button_confirm_creation.Click += button_confirm_creation_Click;
            // 
            // textbox_pass_create_1
            // 
            textbox_pass_create_1.Location = new Point(12, 27);
            textbox_pass_create_1.Name = "textbox_pass_create_1";
            textbox_pass_create_1.Size = new Size(207, 23);
            textbox_pass_create_1.TabIndex = 1;
            // 
            // textbox_pass_create_2
            // 
            textbox_pass_create_2.Location = new Point(12, 84);
            textbox_pass_create_2.Name = "textbox_pass_create_2";
            textbox_pass_create_2.Size = new Size(207, 23);
            textbox_pass_create_2.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(70, 9);
            label1.Name = "label1";
            label1.Size = new Size(87, 15);
            label1.TabIndex = 3;
            label1.Text = "Enter password";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(61, 66);
            label2.Name = "label2";
            label2.Size = new Size(96, 15);
            label2.TabIndex = 4;
            label2.Text = "Repeat password";
            // 
            // account_creation_form
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(234, 161);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textbox_pass_create_2);
            Controls.Add(textbox_pass_create_1);
            Controls.Add(button_confirm_creation);
            Name = "account_creation_form";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Create password";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button_confirm_creation;
        private TextBox textbox_pass_create_1;
        private TextBox textbox_pass_create_2;
        private Label label1;
        private Label label2;
    }
}