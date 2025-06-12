using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SV_Passer
{
    public partial class Form_secret : Form
    {
        private string _password;
        //лист тем заметок
        private List<NotesEntry> notesEntries = new();



        //Путь к файлам
        private string path_passwd = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SV_data", "passwd.sv");
        private string path_notes = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SV_data", "notes.sv");

        private string decrypted_passwd = "";
        private string decrypted_notes = "";

        public Form_secret(string password) //получение пароля из первой формы
        {
            InitializeComponent();







            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;



            _password = password;

            string decrypted_passwd_raw = DecryptFile(path_passwd, _password);
            string decrypted_notes_raw = DecryptFile(path_notes, _password);

            string json_passwd = ExtractJsonAndValidateHash(decrypted_passwd_raw, _password);
            string json_notes = ExtractJsonAndValidateHash(decrypted_notes_raw, _password);

            if (json_passwd != null && json_notes != null)
            {
                decrypted_passwd = json_passwd;
                decrypted_notes = json_notes;
            }


            //десереализация джсона
            try
            {
                var passData = JsonSerializer.Deserialize<PasswordData>(decrypted_passwd);
                var notesData = JsonSerializer.Deserialize<NotesData>(decrypted_notes);

                dgw_pass.DataSource = passData.entries; // пароли в DGW
                notesEntries = notesData.entries ?? new List<NotesEntry>(); //темы заметок в листбокс
                // Заполняем ListBox
                listBox_notes.Items.Clear();
                foreach (var note in notesEntries)
                {
                    listBox_notes.Items.Add(note.topic);
                }

                //для поиска
                passwordEntries = passData.entries ?? new List<PasswordEntry>();
                filteredEntries = new List<PasswordEntry>(passwordEntries);
                dgw_pass.DataSource = filteredEntries;

            }
            catch (Exception ex)
            {
                MessageBox.Show("JSON reading error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



            //Настройки DataGridView
            dgw_pass.AllowUserToAddRows = false;
            dgw_pass.AllowUserToDeleteRows = false;
            dgw_pass.ReadOnly = false;
            dgw_pass.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgw_pass.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;




        }





        //сохранить данные
        private void button_save_Click(object sender, EventArgs e)
        {
            try
            {
                textBox_search.Text = "";
                UpdateJsonFromTables();

                EncryptAndSave(decrypted_passwd, _password, path_passwd);
                EncryptAndSave(decrypted_notes, _password, path_notes);

                // Повторная десериализация из обновлённого JSON
                var passData = JsonSerializer.Deserialize<PasswordData>(decrypted_passwd);
                passwordEntries = passData.entries ?? new List<PasswordEntry>();
                filteredEntries = new List<PasswordEntry>(passwordEntries);
                dgw_pass.DataSource = null;
                dgw_pass.DataSource = filteredEntries;


                MessageBox.Show("Data is saved.", "Succsess", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch
            {
                MessageBox.Show("Can't save. No pawssword, or you just tried to \"hack\" this app. :P");
            }
        }






        private string DecryptFile(string filepath, string password)
        {
            try
            {
                byte[] encryptedData = File.ReadAllBytes(filepath);

                // 1. Получаем IV из первых 16 байт
                byte[] iv = encryptedData.Take(16).ToArray();
                byte[] cipherText = encryptedData.Skip(16).ToArray();

                // 2. Загружаем соль
                string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string saltPath = Path.Combine(appDirectory, "SV_data", "salt.sv");
                byte[] salt = File.ReadAllBytes(saltPath);

                // 3. Генерируем ключ из пароля и соли
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                using var pbkdf2 = new Rfc2898DeriveBytes(passwordBytes, salt, 980_000, HashAlgorithmName.SHA512);
                byte[] key = pbkdf2.GetBytes(32);

                // 4. Расшифровываем
                using Aes aes = Aes.Create();
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using var decryptor = aes.CreateDecryptor();
                using var ms = new MemoryStream(cipherText);
                using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
                using var reader = new StreamReader(cs, Encoding.UTF8);
                return reader.ReadToEnd();
            }


            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }



        // Сравнить Хеш, вернуть готовый к работе JSON
        private string ExtractJsonAndValidateHash(string decryptedText, string password)
        {
            if (string.IsNullOrWhiteSpace(decryptedText))
                return null;

            string[] lines = decryptedText.Split(new[] { '\n' }, 2);

            if (lines.Length < 2)
            {
                MessageBox.Show("Corrupted file or wrong password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;
            }

            string hashFromFile = lines[0].Trim();
            string json = lines[1];

            // Считаем хеш с текущего пароля
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] passHash;
            using (SHA256 sha = SHA256.Create())
            {
                passHash = sha.ComputeHash(passwordBytes);
            }
            string hashFromPassword = Convert.ToBase64String(passHash);




            if (hashFromFile != hashFromPassword)
            {
                MessageBox.Show("Hashes are not the same.\nProbably wrong password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            return json;
        }


        private void EncryptAndSave(string jsonContent, string password, string targetFile)
        {
            try
            {
                // Считаем хеш пароля (256й для проверки 512 для ключа)
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] passHash;
                using (SHA256 sha = SHA256.Create())
                {
                    passHash = sha.ComputeHash(passwordBytes);
                }
                string hashBase64 = Convert.ToBase64String(passHash);
                // Собираем итоговую строку
                string contentToEncrypt = hashBase64 + "\n" + jsonContent;


                // Загружаем соль
                string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string saltPath = Path.Combine(appDirectory, "SV_data", "salt.sv");
                byte[] salt = File.ReadAllBytes(saltPath);

                // Генерируем ключ                
                using var pbkdf2 = new Rfc2898DeriveBytes(passwordBytes, salt, 980_000, HashAlgorithmName.SHA512);
                byte[] key = pbkdf2.GetBytes(32);

                // Преобразуем текст
                byte[] plainBytes = Encoding.UTF8.GetBytes(contentToEncrypt);

                // Генерируем IV
                byte[] iv = new byte[16];
                RandomNumberGenerator.Fill(iv);

                using Aes aes = Aes.Create();
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using var encryptor = aes.CreateEncryptor();
                using var ms = new MemoryStream();
                ms.Write(iv, 0, iv.Length); // сначала IV

                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    cs.Write(plainBytes, 0, plainBytes.Length);
                    cs.FlushFinalBlock();
                }

                File.WriteAllBytes(targetFile, ms.ToArray());
                //MessageBox.Show("Данные сохранены.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Reading error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateJsonFromTables()
        {
            if (dgw_pass.DataSource is List<PasswordEntry> shownList)
            {
                // Обновляем основную коллекцию из отображаемого списка
                passwordEntries = shownList.ToList(); // клонируем/переносим

                var passData = new PasswordData { entries = passwordEntries };
                decrypted_passwd = JsonSerializer.Serialize(passData, new JsonSerializerOptions { WriteIndented = true });
            }

            var notesData = new NotesData { entries = notesEntries };
            decrypted_notes = JsonSerializer.Serialize(notesData, new JsonSerializerOptions { WriteIndented = true });

        }

        private void button_add_pass_Click(object sender, EventArgs e)
        {
            if (dgw_pass.DataSource is List<PasswordEntry> list)
            {
                list.Add(new PasswordEntry()); // создаём пустую строку
                dgw_pass.DataSource = null;
                dgw_pass.DataSource = list;
                dgw_pass.FirstDisplayedScrollingRowIndex = dgw_pass.RowCount - 1;

            }

        }

        private void button_add_note_Click(object sender, EventArgs e)
        {
            var newNote = new NotesEntry { topic = "New note", note = "" };
            notesEntries.Add(newNote);
            listBox_notes.Items.Add(newNote.topic);
            listBox_notes.SelectedIndex = listBox_notes.Items.Count - 1;
        }

        private void button_delete_pass_Click(object sender, EventArgs e)
        {
            if (dgw_pass.DataSource is List<PasswordEntry> list && dgw_pass.CurrentRow != null)
            {
                int index = dgw_pass.CurrentRow.Index;

                if (list.Count == 1)
                {
                    // Не удаляем последнюю строку — просто очищаем её
                    list[0] = new PasswordEntry();
                }
                else if (index >= 0 && index < list.Count)
                {
                    list.RemoveAt(index);
                }

                dgw_pass.DataSource = null;
                dgw_pass.DataSource = list;
            }
        }

        private void button_delete_note_Click(object sender, EventArgs e)
        {
            int index = listBox_notes.SelectedIndex;
            if (index < 0 || index >= notesEntries.Count)
                return;


            if (index >= 0 && index < notesEntries.Count)
            {
                notesEntries.RemoveAt(index);
                listBox_notes.Items.RemoveAt(index);
                richTextBox_note_body.Clear();
            }
        }

        // Сохранить текст заметки
        private void button_save_note_Click(object sender, EventArgs e)
        {
            int index = listBox_notes.SelectedIndex;
            if (index < 0 || index >= notesEntries.Count)
                return;

            if (index >= 0 && index < notesEntries.Count)
            {
                notesEntries[index].note = richTextBox_note_body.Text;
                MessageBox.Show("Note updated.", "Sucsess", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        //выбор топика в листбоксе
        private void listBox_notes_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = listBox_notes.SelectedIndex;
            if (index < 0 || index >= notesEntries.Count)
                return;


            if (index >= 0 && index < notesEntries.Count)
            {
                richTextBox_note_body.Text = notesEntries[index].note;
            }
        }


        //Для поиска
        private List<PasswordEntry> passwordEntries = new();
        private List<PasswordEntry> filteredEntries = new();

        private void textBox_search_TextChanged(object sender, EventArgs e)
        {
            string query = textBox_search.Text.Trim().ToLowerInvariant();

            filteredEntries = passwordEntries
                .Where(p =>
                    (p.Website != null && p.Website.ToLowerInvariant().Contains(query)) ||
                    (p.Email != null && p.Email.ToLowerInvariant().Contains(query)) ||
                    (p.Category != null && p.Category.ToLowerInvariant().Contains(query)) ||
                    (p.Note != null && p.Note.ToLowerInvariant().Contains(query)) 
                ).ToList();

            dgw_pass.DataSource = null;
            dgw_pass.DataSource = filteredEntries;

            if (textBox_search.Text != "")
            {
                button_add_pass.Enabled = false;
                button_delete_pass.Enabled = false;
                button_save.Enabled = false;
            }
            else
            {
                button_add_pass.Enabled = true;
                button_delete_pass.Enabled = true;
                button_save.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox_notes.SelectedIndex == -1)
            {
                MessageBox.Show("Select a note to rename.");
                return;
            }

            int index = listBox_notes.SelectedIndex;
            string currentTitle = listBox_notes.Items[index].ToString();

            using (var renameForm = new RenameNoteForm())
            {
                if (renameForm.ShowDialog() == DialogResult.OK)
                {
                    string newTitle = renameForm.NewTitle;
                    listBox_notes.Items[index] = newTitle;
                    notesEntries[index].topic = newTitle;
                }
            }

        }

        private void button_change_passwd_Click(object sender, EventArgs e)
        {
            using (var form = new ChangePasswordForm())
            {
                if (form.ShowDialog() != DialogResult.OK)
                    return;

                string oldPass = form.OldPassword;
                string newPass = form.NewPassword;

                if (oldPass == _password)
                {
                    try
                    {

                        _password = newPass;

                        // 2. Сгенерировать новую соль и хеш

                        byte[] salt = new byte[16];
                        RandomNumberGenerator.Fill(salt);

                        // Путь к файлу соли
                        string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
                        string dataDirectory = Path.Combine(appDirectory, "SV_data");
                        string saltPath = Path.Combine(dataDirectory, "salt.sv");

                        // Сохраняем соль в файл (перезапись)
                        File.WriteAllBytes(saltPath, salt);

                        //сохранить
                        button_save_Click(this, EventArgs.Empty);
                        MessageBox.Show("Password changed.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Password changing error: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Old password entered incorrectly");
                }

            }
        }


        private void button_export_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "CSV файл (*.csv)|*.csv";
                dialog.Title = "Password export";

                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("Website;Email;Login;Password;Category;Telephone;Note");

                    foreach (var entry in passwordEntries)
                    {
                        string line = string.Join(";", new[]
                        {
                    entry.Website,
                    entry.Email,
                    entry.Login,
                    entry.Password,
                    entry.Category,
                    entry.Telephone,
                    entry.Note
                }
                        .Select(s => (s ?? "")
                            .Replace(";", "⸺")
                            .Replace("\r\n", "  ")
                            .Replace("\r", "  ")
                            .Replace("\n", "  ")
                        ));

                        sb.AppendLine(line);
                    }

                    File.WriteAllText(dialog.FileName, sb.ToString(), Encoding.UTF8);
                    MessageBox.Show("Export finished.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Export error: " + ex.Message);
                }
            }
        }

        private void button_import_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "CSV файл (*.csv)|*.csv";
                dialog.Title = "Password import";

                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    var lines = File.ReadAllLines(dialog.FileName, Encoding.UTF8).Skip(1); // пропуск заголовка
                    var imported = new List<PasswordEntry>();

                    foreach (var line in lines)
                    {
                        string[] parts = line.Split(';');
                        if (parts.Length < 5) continue; // пропуск неполных строк

                        imported.Add(new PasswordEntry
                        {
                            Website = parts[0].Replace("⸺", ";"),
                            Email = parts[1].Replace("⸺", ";"),
                            Login = parts[2].Replace("⸺", ";"),
                            Password = parts[3].Replace("⸺", ";"),
                            Category = parts[4].Replace("⸺", ";"),
                            Telephone = parts[5].Replace("⸺", ";"),
                            Note = parts[6].Replace("⸺", ";")
                        });
                    }

                    if (imported.Count == 0)
                    {
                        MessageBox.Show("File is empty.");
                        return;
                    }

                    // Вариант: заменить или добавить?
                    if (MessageBox.Show("Replace existing data? \n Click \'no\' to add instead of replace", "Import", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        passwordEntries = imported;
                    }
                    else
                    {
                        passwordEntries.AddRange(imported);
                    }

                    filteredEntries = new List<PasswordEntry>(passwordEntries);
                    dgw_pass.DataSource = null;
                    dgw_pass.DataSource = filteredEntries;

                    //button_save_Click(this, EventArgs.Empty);

                    MessageBox.Show("Import finished.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Import error: " + ex.Message);
                }
            }
        }

        private void dgw_pass_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var value = dgw_pass.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();
                if (!string.IsNullOrEmpty(value))
                {
                    Clipboard.SetText(value);

                    // Показать ToolTip на месте ячейки
                    Rectangle cellRect = dgw_pass.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                    Point location = new Point(cellRect.X + dgw_pass.Left, cellRect.Y + dgw_pass.Top);

                    toolTip_copy.Show("Copied to clipboard!", this, location, 1500); // показать на 1,5 секунду
                }
            }
        }

        //обработка клавиш
        private void Form_secret_Load(object sender, EventArgs e)
        {
            //обработка клавиш
            this.KeyPreview = true;
        }
        private void Form_secret_KeyDown(object sender, KeyEventArgs e)
        {
            // сохранить Ctrl+S
            if (e.Control && e.KeyCode == Keys.S)
            {
                e.SuppressKeyPress = true; // чтобы не пищал
                button_save_Click(this, EventArgs.Empty);
            }
            // Ctrl + V
            // Только если сам DataGridView в фокусе (а не вложенные элементы)
            if (!dgw_pass.Focused)
                return;

            if (e.Control && e.KeyCode == Keys.V)
            {
                var cell = dgw_pass.CurrentCell;

                if (cell != null && !cell.ReadOnly)
                {
                    string clipboardText = Clipboard.GetText();

                    if (!string.IsNullOrEmpty(clipboardText))
                    {
                        cell.Value = clipboardText;
                        e.Handled = true;
                    }
                }
            }
        }

    }







    public class PasswordEntry
    {
        public string Website { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        
        public string Category { get; set; }

        public string Telephone { get; set; }
        public string Note { get; set; }

        
    }

    public class NotesEntry
    {
        public string topic { get; set; }
        public string note { get; set; }
    }

    public class PasswordData
    {
        public List<PasswordEntry> entries { get; set; }
    }

    public class NotesData
    {
        public List<NotesEntry> entries { get; set; }
    }


    

}
