namespace SV_Passer
{
    partial class RenameNoteForm
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
            label_prompt = new Label();
            textBox_newTitle = new TextBox();
            button_ok = new Button();
            button_cancel = new Button();
            SuspendLayout();
            // 
            // label_prompt
            // 
            label_prompt.AutoSize = true;
            label_prompt.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label_prompt.Location = new Point(37, 5);
            label_prompt.Name = "label_prompt";
            label_prompt.Size = new Size(88, 21);
            label_prompt.TabIndex = 0;
            label_prompt.Text = "New name:";
            // 
            // textBox_newTitle
            // 
            textBox_newTitle.Location = new Point(5, 29);
            textBox_newTitle.Name = "textBox_newTitle";
            textBox_newTitle.Size = new Size(156, 23);
            textBox_newTitle.TabIndex = 1;
            // 
            // button_ok
            // 
            button_ok.Location = new Point(5, 58);
            button_ok.Name = "button_ok";
            button_ok.Size = new Size(75, 23);
            button_ok.TabIndex = 2;
            button_ok.Text = "Save";
            button_ok.UseVisualStyleBackColor = true;
            button_ok.Click += button_ok_Click;
            // 
            // button_cancel
            // 
            button_cancel.Location = new Point(86, 58);
            button_cancel.Name = "button_cancel";
            button_cancel.Size = new Size(75, 23);
            button_cancel.TabIndex = 3;
            button_cancel.Text = "Cancel";
            button_cancel.UseVisualStyleBackColor = true;
            button_cancel.Click += button_cancel_Click;
            // 
            // RenameNoteForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(175, 88);
            Controls.Add(button_cancel);
            Controls.Add(button_ok);
            Controls.Add(textBox_newTitle);
            Controls.Add(label_prompt);
            Name = "RenameNoteForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Rename";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label_prompt;
        private TextBox textBox_newTitle;
        private Button button_ok;
        private Button button_cancel;
    }
}