using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;





namespace SV_Passer
{

    public partial class form_login : Form
    {
        // Кэшируем картинки, чтобы не создавать каждый раз новые объекты
        Bitmap eyeOpen;
        Bitmap eyeClosed;
        bool isPasswordVisible = false;


        public form_login()
        {



            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.AcceptButton = button_enter;
            textBox_login.PasswordChar = '#'; // Символ который выводится в текстбоксе вместо текста


            // Загружаем картинки один раз в начале
            using (var ms = new MemoryStream(Properties.Resources.eye))
                eyeOpen = new Bitmap(ms);

            using (var ms = new MemoryStream(Properties.Resources.eye_closed))
                eyeClosed = new Bitmap(ms);

            pictureBox_login.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox_login.Image = eyeOpen;

        }




        //фокус сразу на текстбокс, без лишних кликов
        private void form_login_Shown(object sender, EventArgs e)
        {
            textBox_login.Focus();
        }


        //кнопка для отправки введённого пароля
        private void button_enter_Click(object sender, EventArgs e)
        {
            string password = textBox_login.Text;

            string dataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SV_data");
            string pathPasswd = Path.Combine(dataDir, "passwd.sv");
            string pathSalt = Path.Combine(dataDir, "salt.sv");


            bool decrypt_check = DecryptFile(pathPasswd, password);

            if (decrypt_check)
            {
                Form_secret secretForm = new Form_secret(password);
                secretForm.FormClosed += (s, e) => Application.Exit();
                secretForm.Show();
                this.Hide(); // Скрываем текущую форму.
            }
            else
            {
                
                //сообщение уже было показано в функции
                

            }

        }


        //текстбокс с вводом пароля
        private void textBox_login_TextChanged(object sender, EventArgs e)
        {
            label_char_count.Text = "Symbol count: " + textBox_login.Text.Length;
        }



        private void pictureBox_login_Click(object sender, EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;

            if (isPasswordVisible)
                textBox_login.PasswordChar = '\0';
            else
                textBox_login.PasswordChar = '#';

            // Меняем картинку из кеша, без создания новых объектов
            pictureBox_login.Image = isPasswordVisible ? eyeClosed : eyeOpen;

            textBox_login.Refresh();
        }

   
        

        private void button_create_account_Click(object sender, EventArgs e)
        {
            account_creation_form acc_form = new account_creation_form();
            acc_form.Show(); // открыть форму создания пароля
            this.Hide(); // скрыть текущую.
            acc_form.FormClosed += (s, e) => this.Show(); // Если форма была закрыта крестиком или ещё как-либо, надо вернуть эту          
        }


        private bool DecryptFile(string filepath, string password)
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

                _ = reader.ReadToEnd(); // обязательно! запускает расшифровку
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erorr: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
       



    }
}
