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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String loginUser = textBox1.Text;
            String passUser = textBox2.Text;

            // Проверяем, что строки не пустые
            if (string.IsNullOrWhiteSpace(loginUser) || string.IsNullOrWhiteSpace(passUser))
            {
                MessageBox.Show("Введите логин и пароль", "Ошибка входа");
                return;
            }

            if (IsValidUser(loginUser, passUser))
            {
                // Если пользователь успешно авторизован, открывайте новую форму
                var form3 = new Form3();
                form3.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Неверные логин или пароль. Попробуйте еще раз.", "Ошибка входа");
            }
        }

        private bool IsValidUser(string loginUser, string passUser)
        {
        
            string filePath = "users.txt";

            // Проверяем наличие файла с данными
            if (File.Exists(filePath))
            {
                string[] existingUsers = File.ReadAllLines(filePath);

                // Проверяем введенные учетные данные с данными в файле
                foreach (string user in existingUsers)
                {
                    string[] userInfo = user.Split(',');
                    if (userInfo.Length >= 2 && userInfo[0].Equals(loginUser, StringComparison.OrdinalIgnoreCase) && userInfo[1] == passUser)
                    {
                        return true; // Учетные данные верны
                    }
                }
            }
            return false; // Неверные учетные данные
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var form2 = new Form1();
              form2.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
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
