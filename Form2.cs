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
    // Добавляем аккаунты из файла в ComboBox при загрузке формы
            LoadAccountsIntoComboBox();
        }
        private void LoadAccountsIntoComboBox()
        {
            comboBox1.Items.Clear(); // Очищаем ComboBox перед добавлением обновленных аккаунтов

            string filePath = "users.txt";

            if (File.Exists(filePath))
            {
                string[] existingUsers = File.ReadAllLines(filePath);

                foreach (string user in existingUsers)
                {
                    string[] userInfo = user.Split(',');
                    if (userInfo.Length >= 2)
                    {
                        comboBox1.Items.Add(userInfo[0]);
                    }
                }
            }
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


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Заполняем поля логина и пароля при выборе аккаунта из ComboBox
            string selectedUser = comboBox1.SelectedItem.ToString();
            string filePath = "users.txt";

            if (File.Exists(filePath))
            {
                string[] existingUsers = File.ReadAllLines(filePath);

                foreach (string user in existingUsers)
                {
                    string[] userInfo = user.Split(',');
                    if (userInfo.Length >= 2 && userInfo[0] == selectedUser)
                    {
                        textBox1.Text = userInfo[0]; // Логин
                        textBox2.Text = userInfo[1]; // Пароль
                        break;
                    }
                }
            }
        }

        private void textBox1_MouseEnter(object sender, EventArgs e)
        {
            // Показываем ComboBox при наведении мыши на текстовое поле
            comboBox1.Visible = true;
        }

        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            // Скрываем ComboBox при уходе мыши с текстового поля
            comboBox1.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string selectedUser = comboBox1.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedUser))
            {
                MessageBox.Show("Выберите аккаунт для удаления", "Ошибка");
                return;
            }

            // Запрос подтверждения удаления
            DialogResult result = MessageBox.Show($"Вы уверены, что хотите удалить аккаунт {selectedUser}?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Удаляем выбранный аккаунт из ComboBox и файла
                RemoveAccount(selectedUser);
                ClearTextBoxes();
                LoadAccountsIntoComboBox(); // Обновляем ComboBox после удаления
            }
        }
        private void RemoveAccount(string accountToRemove)
        {
            string filePath = "users.txt";

            if (File.Exists(filePath))
            {
                List<string> updatedUsers = new List<string>();
                string[] existingUsers = File.ReadAllLines(filePath);

                foreach (string user in existingUsers)
                {
                    string[] userInfo = user.Split(',');
                    if (userInfo.Length >= 2 && userInfo[0] != accountToRemove)
                    {
                        updatedUsers.Add(user);
                    }
                }

                // Записываем обновленные данные в файл
                File.WriteAllLines(filePath, updatedUsers.ToArray());
            }
        }

        private void ClearTextBoxes()
        {
            // Очищаем текстовые поля
            textBox1.Clear();
            textBox2.Clear();
        }
    }
}
