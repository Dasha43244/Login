using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Login
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String loginUser = textBox1.Text;
            String passUser = textBox2.Text;
            // Проверяем, что строки не пустые
            if (string.IsNullOrWhiteSpace(loginUser) || string.IsNullOrWhiteSpace(passUser))
            {
                MessageBox.Show("Введите имя пользователя и пароль", "Ошибка");
                return;
            }
            string filePath = "users.txt";

            if (!File.Exists(filePath))
            {
                // Если файла нет, создаем его
                File.Create(filePath).Close();
            }
      
            if (IsUsernameUnique(loginUser, filePath))
            {
                // Добавляем нового пользователя в файл
                using (StreamWriter sw = File.AppendText(filePath))
                {
                    sw.WriteLine($"{ loginUser },{passUser}");
                }
                MessageBox.Show("Регистрация успешна!", "Успех");
                var form3 = new Form3();
                form3.Show();
                this.Hide();

            }
        }
        private bool IsUsernameUnique(string username, string filePath)
        {
            // Проверяем уникальность имени пользователя в файле
            if (File.Exists(filePath))
            {
                string[] existingUsers = File.ReadAllLines(filePath);
                foreach (string user in existingUsers)
                {
                    string[] userInfo = user.Split(',');
                    if (userInfo.Length > 0 && userInfo[0].Equals(username, StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show("Имя пользователя уже занято");
                        return false; // Имя пользователя уже занято
                    }
                }
            }
            return true; // Имя пользователя уникально
        }
        private void button2_Click(object sender, EventArgs e)
        {
         
            var form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.PasswordChar == (char)0)
            {
                textBox2.PasswordChar = '•';

            }
            else
            {
                textBox2.PasswordChar = (char)0;
            }
        }
    }
}

