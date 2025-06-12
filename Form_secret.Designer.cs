namespace SV_Passer
{
    partial class Form_secret
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
            components = new System.ComponentModel.Container();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            button_save = new Button();
            dgw_pass = new DataGridView();
            button_add_pass = new Button();
            button_add_note = new Button();
            button_delete_pass = new Button();
            button_delete_note = new Button();
            button_save_note = new Button();
            listBox_notes = new ListBox();
            richTextBox_note_body = new RichTextBox();
            label_search = new Label();
            textBox_search = new TextBox();
            button1 = new Button();
            label1 = new Label();
            button_change_passwd = new Button();
            button_export = new Button();
            button_import = new Button();
            toolTip_copy = new ToolTip(components);
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgw_pass).BeginInit();
            SuspendLayout();
            // 
            // button_save
            // 
            button_save.Location = new Point(2, 355);
            button_save.Name = "button_save";
            button_save.Size = new Size(760, 23);
            button_save.TabIndex = 4;
            button_save.Text = "Save data";
            button_save.UseVisualStyleBackColor = true;
            button_save.Click += button_save_Click;
            // 
            // dgw_pass
            // 
            dgw_pass.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgw_pass.Location = new Point(2, 35);
            dgw_pass.Name = "dgw_pass";
            dgw_pass.Size = new Size(866, 314);
            dgw_pass.TabIndex = 5;
            dgw_pass.CellDoubleClick += dgw_pass_CellDoubleClick;
            // 
            // button_add_pass
            // 
            button_add_pass.Location = new Point(195, 5);
            button_add_pass.Name = "button_add_pass";
            button_add_pass.Size = new Size(75, 23);
            button_add_pass.TabIndex = 7;
            button_add_pass.Text = "Add row";
            button_add_pass.UseVisualStyleBackColor = true;
            button_add_pass.Click += button_add_pass_Click;
            // 
            // button_add_note
            // 
            button_add_note.Location = new Point(1004, 122);
            button_add_note.Name = "button_add_note";
            button_add_note.Size = new Size(75, 23);
            button_add_note.TabIndex = 8;
            button_add_note.Text = "Add note";
            button_add_note.UseVisualStyleBackColor = true;
            button_add_note.Click += button_add_note_Click;
            // 
            // button_delete_pass
            // 
            button_delete_pass.Location = new Point(275, 5);
            button_delete_pass.Name = "button_delete_pass";
            button_delete_pass.Size = new Size(75, 23);
            button_delete_pass.TabIndex = 9;
            button_delete_pass.Text = "Delete row";
            button_delete_pass.UseVisualStyleBackColor = true;
            button_delete_pass.Click += button_delete_pass_Click;
            // 
            // button_delete_note
            // 
            button_delete_note.Location = new Point(1004, 151);
            button_delete_note.Name = "button_delete_note";
            button_delete_note.Size = new Size(75, 23);
            button_delete_note.TabIndex = 10;
            button_delete_note.Text = "Remove";
            button_delete_note.UseVisualStyleBackColor = true;
            button_delete_note.Click += button_delete_note_Click;
            // 
            // button_save_note
            // 
            button_save_note.Location = new Point(1006, 180);
            button_save_note.Name = "button_save_note";
            button_save_note.Size = new Size(75, 23);
            button_save_note.TabIndex = 11;
            button_save_note.Text = "Write";
            button_save_note.UseVisualStyleBackColor = true;
            button_save_note.Click += button_save_note_Click;
            // 
            // listBox_notes
            // 
            listBox_notes.FormattingEnabled = true;
            listBox_notes.ItemHeight = 15;
            listBox_notes.Location = new Point(878, 64);
            listBox_notes.Name = "listBox_notes";
            listBox_notes.Size = new Size(120, 139);
            listBox_notes.TabIndex = 13;
            listBox_notes.SelectedIndexChanged += listBox_notes_SelectedIndexChanged;
            // 
            // richTextBox_note_body
            // 
            richTextBox_note_body.Location = new Point(878, 209);
            richTextBox_note_body.Name = "richTextBox_note_body";
            richTextBox_note_body.Size = new Size(203, 140);
            richTextBox_note_body.TabIndex = 14;
            richTextBox_note_body.Text = "";
            // 
            // label_search
            // 
            label_search.AutoSize = true;
            label_search.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label_search.Location = new Point(2, 5);
            label_search.Name = "label_search";
            label_search.Size = new Size(60, 21);
            label_search.TabIndex = 15;
            label_search.Text = "Search:";
            // 
            // textBox_search
            // 
            textBox_search.Location = new Point(63, 5);
            textBox_search.Name = "textBox_search";
            textBox_search.Size = new Size(126, 23);
            textBox_search.TabIndex = 16;
            textBox_search.TextChanged += textBox_search_TextChanged;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button1.Location = new Point(878, 35);
            button1.Name = "button1";
            button1.Size = new Size(192, 23);
            button1.TabIndex = 17;
            button1.Text = "Rename note";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.Location = new Point(941, 4);
            label1.Name = "label1";
            label1.Size = new Size(58, 21);
            label1.TabIndex = 18;
            label1.Text = "NOTES";
            // 
            // button_change_passwd
            // 
            button_change_passwd.Location = new Point(978, 355);
            button_change_passwd.Name = "button_change_passwd";
            button_change_passwd.Size = new Size(100, 23);
            button_change_passwd.TabIndex = 19;
            button_change_passwd.Text = "Change passwd";
            button_change_passwd.UseVisualStyleBackColor = true;
            button_change_passwd.Click += button_change_passwd_Click;
            // 
            // button_export
            // 
            button_export.Location = new Point(768, 355);
            button_export.Name = "button_export";
            button_export.Size = new Size(100, 23);
            button_export.TabIndex = 20;
            button_export.Text = "Export";
            button_export.UseVisualStyleBackColor = true;
            button_export.Click += button_export_Click;
            // 
            // button_import
            // 
            button_import.Location = new Point(873, 355);
            button_import.Name = "button_import";
            button_import.Size = new Size(100, 23);
            button_import.TabIndex = 21;
            button_import.Text = "Import";
            button_import.UseVisualStyleBackColor = true;
            button_import.Click += button_import_Click;
            // 
            // toolTip_copy
            // 
            toolTip_copy.BackColor = Color.FromArgb(255, 192, 128);
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(356, 10);
            label2.Name = "label2";
            label2.Size = new Size(112, 15);
            label2.TabIndex = 22;
            label2.Text = "Dobuel click = copy";
            // 
            // Form_secret
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1084, 381);
            Controls.Add(label2);
            Controls.Add(button_import);
            Controls.Add(button_export);
            Controls.Add(button_change_passwd);
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(textBox_search);
            Controls.Add(label_search);
            Controls.Add(richTextBox_note_body);
            Controls.Add(listBox_notes);
            Controls.Add(button_save_note);
            Controls.Add(button_delete_note);
            Controls.Add(button_delete_pass);
            Controls.Add(button_add_note);
            Controls.Add(button_add_pass);
            Controls.Add(dgw_pass);
            Controls.Add(button_save);
            Name = "Form_secret";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SV Passer";
            Load += Form_secret_Load;
            KeyDown += Form_secret_KeyDown;
            ((System.ComponentModel.ISupportInitialize)dgw_pass).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Button button_save;
        private DataGridView dgw_pass;
        private Button button_add_pass;
        private Button button_add_note;
        private Button button_delete_pass;
        private Button button_delete_note;
        private Button button_save_note;
        private ListBox listBox_notes;
        private RichTextBox richTextBox_note_body;
        private Label label_search;
        private TextBox textBox_search;
        private Button button1;
        private Label label1;
        private Button button_change_passwd;
        private Button button_export;
        private Button button_import;
        private ToolTip toolTip_copy;
        private Label label2;
    }
}