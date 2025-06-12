using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;


namespace SV_Passer
{
    public partial class account_creation_form : Form
    {
        public account_creation_form()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void button_confirm_creation_Click(object sender, EventArgs e)
        {
            // спросить точно ли это нужно?
            var result = MessageBox.Show(
            "NEW PASSWORD CREATIN WILL ERASE ALL EXISTING DATA\nALL DATA WILL BE LOST! (If exist)\n\nContinue?",
            "Warning!",
            MessageBoxButtons.OKCancel,
            MessageBoxIcon.Warning
                                        );

            if (result != DialogResult.OK)
            {
                this.Close(); // Закрываем форму создания, пользователь передумал
                return;
            }




            //пароли при создании
            string pass1 = textbox_pass_create_1.Text;
            string pass2 = textbox_pass_create_2.Text;

            //проверки при создании пароля
            if (string.IsNullOrWhiteSpace(pass1) || string.IsNullOrWhiteSpace(pass2))
            {
                MessageBox.Show("Password can't be empty.");
                return;
            }
            if (pass1 != pass2)
            {
                MessageBox.Show("Passwords are not the same!");
                return;
            }

            // 1. Генерация соли
            byte[] salt = new byte[16];
            RandomNumberGenerator.Fill(salt);

            // 2. Преобразуем Password
            string password = textbox_pass_create_1.Text;
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            // 3. Генерация ключа через PBKDF2 (с указанием алгоритма)
            using var pbkdf2 = new Rfc2898DeriveBytes(
                passwordBytes, //байт массив пароля
                salt, // соль (тоже байты)
                980_000, //количество итераций
                HashAlgorithmName.SHA512 // более современный, чем SHA1
            );

            byte[] key = pbkdf2.GetBytes(32); // 32 байта = 256 бит для AES



            // Создаём путь к папке
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string dataDirectory = Path.Combine(appDirectory, "SV_data");

            // Создаём папку, если не существует
            if (!Directory.Exists(dataDirectory))
            {
                Directory.CreateDirectory(dataDirectory);
            }

            // Путь к файлу соли
            string saltPath = Path.Combine(dataDirectory, "salt.sv");

            // Сохраняем соль в файл (перезапись)
            File.WriteAllBytes(saltPath, salt);

            //формирование хеша
            byte[] passHash;
            using (SHA256 sha = SHA256.Create())
            {
                passHash = sha.ComputeHash(passwordBytes);
            }
            string passHashBase64 = Convert.ToBase64String(passHash);

            //данные для первого файла паролей
            //string json_passwd = "{ \"entries\": [] }";
            string json_passwd =
@"{
  ""entries"": [
    {
      ""Website"": """",
      ""Email"": """",
      ""Login"": """",
      ""Password"": """",
      ""Category"": """",
      ""Telephone"": """",
      ""Note"": """"
    }
  ]
}";

            string contentToEncrypt_passwd = passHashBase64 + "\n" + json_passwd;
            //данные для первого файла заметок
            string json_notes = "{ \"entries\": [] }";
            string contentToEncrypt_notes = passHashBase64 + "\n" + json_notes;


            // Пути к файлам
            string pathPasswd = Path.Combine(dataDirectory, "passwd.sv");
            string pathNotes = Path.Combine(dataDirectory, "notes.sv");

            // Шифруем и сохраняем оба
            EncryptAndSave(contentToEncrypt_passwd, key, pathPasswd);
            EncryptAndSave(contentToEncrypt_notes, key, pathNotes);

            MessageBox.Show("Files are created and encrypted sucessfuly!");

            // Вернуться на форму входа
            this.Close();          
        }


        // Метод шифрования с IV
        void EncryptAndSave(string contentToEncrypt, byte[] key, string filePath)
        {
            byte[] plainBytes = Encoding.UTF8.GetBytes(contentToEncrypt);
            byte[] iv = new byte[16];
            RandomNumberGenerator.Fill(iv);

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (var encryptor = aes.CreateEncryptor())
                using (var ms = new MemoryStream())
                {
                    // Сначала IV
                    ms.Write(iv, 0, iv.Length);

                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        cs.Write(plainBytes, 0, plainBytes.Length);
                        cs.FlushFinalBlock();
                    }

                    File.WriteAllBytes(filePath, ms.ToArray());
                }
            }
        }

        





    }
}
