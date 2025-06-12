namespace SV_Passer
{
    partial class ChangePasswordForm
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
            label1 = new Label();
            label2 = new Label();
            textbox_old = new TextBox();
            textbox_new1 = new TextBox();
            textbox_new2 = new TextBox();
            button_ok = new Button();
            button_cancel = new Button();
            label3 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.Location = new Point(9, 9);
            label1.Name = "label1";
            label1.Size = new Size(90, 17);
            label1.TabIndex = 0;
            label1.Text = "Old password";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label2.Location = new Point(13, 51);
            label2.Name = "label2";
            label2.Size = new Size(95, 17);
            label2.TabIndex = 1;
            label2.Text = "New password";
            // 
            // textbox_old
            // 
            textbox_old.Location = new Point(118, 8);
            textbox_old.Name = "textbox_old";
            textbox_old.Size = new Size(180, 23);
            textbox_old.TabIndex = 3;
            // 
            // textbox_new1
            // 
            textbox_new1.Location = new Point(118, 50);
            textbox_new1.Name = "textbox_new1";
            textbox_new1.Size = new Size(180, 23);
            textbox_new1.TabIndex = 4;
            // 
            // textbox_new2
            // 
            textbox_new2.Location = new Point(118, 79);
            textbox_new2.Name = "textbox_new2";
            textbox_new2.Size = new Size(180, 23);
            textbox_new2.TabIndex = 5;
            // 
            // button_ok
            // 
            button_ok.Location = new Point(15, 108);
            button_ok.Name = "button_ok";
            button_ok.Size = new Size(140, 23);
            button_ok.TabIndex = 6;
            button_ok.Text = "Save";
            button_ok.UseVisualStyleBackColor = true;
            button_ok.Click += button_ok_Click;
            // 
            // button_cancel
            // 
            button_cancel.Location = new Point(161, 108);
            button_cancel.Name = "button_cancel";
            button_cancel.Size = new Size(140, 23);
            button_cancel.TabIndex = 7;
            button_cancel.Text = "Cancel";
            button_cancel.UseVisualStyleBackColor = true;
            button_cancel.Click += button_cancel_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label3.Location = new Point(-2, 80);
            label3.Name = "label3";
            label3.Size = new Size(110, 17);
            label3.TabIndex = 8;
            label3.Text = "Repeat password";
            // 
            // ChangePasswordForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(304, 136);
            Controls.Add(label3);
            Controls.Add(button_cancel);
            Controls.Add(button_ok);
            Controls.Add(textbox_new2);
            Controls.Add(textbox_new1);
            Controls.Add(textbox_old);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "ChangePasswordForm";
            Text = "Passwod change";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox textbox_old;
        private TextBox textbox_new1;
        private TextBox textbox_new2;
        private Button button_ok;
        private Button button_cancel;
        private Label label3;
    }
}