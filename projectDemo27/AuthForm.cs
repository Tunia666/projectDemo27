using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projectDemo27
{
    public partial class AuthForm : Form
    {
        static string connectString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=demo27.1; Integrated Security=True";
        public AuthForm()
        {
            InitializeComponent();
        }

        private void btnAuth_Click(object sender, EventArgs e)
        {
            string login = tbLogin.Text.Trim();
            string password = tbPassword.Text;


            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Введите логин и пароль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectString))
                {
                    connection.Open();

                    string sql = @"
                        SELECT *
                        FROM [Users]
                        WHERE [user_name] = @login AND [password] = @password";

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {

                        cmd.Parameters.AddWithValue("@login", login);
                        cmd.Parameters.AddWithValue("@password", password);

                        using (var r = cmd.ExecuteReader())
                        {
                            if (r.Read())
                            {

                                string roleName = Convert.ToString(r["role"]).Trim();

                                LoginClass.Role = ParseRole(roleName);

                                MessageBox.Show(
                                    "Успешный вход!\nРоль: " + roleName,
                                    "Авторизация",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information
                                );

                                if (LoginClass.Role == LoginClass.UserRole.Admin)
                                {
                                    AdminPanel adminPanel = new AdminPanel();
                                    adminPanel.FormClosed += (s, args) => this.Close();
                                    adminPanel.Show();
                                }
                                else
                                {
                                    ClientPanel clientPanel = new ClientPanel();
                                    clientPanel.FormClosed += (s, args) => this.Close();
                                    clientPanel.Show();
                                }

                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения/запроса:\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private LoginClass.UserRole ParseRole(string roleName)
        {
            roleName = (roleName ?? "").Trim().ToLowerInvariant();

            if (roleName.Contains("admin") ||
                roleName.Contains("администратор"))
            {
                return LoginClass.UserRole.Admin;
            }

            return LoginClass.UserRole.Client;
        }

        private void tbLogin_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
