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
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        string sql = @"
    SELECT [password], [role],
           [failed_attempts], [is_blocked]
    FROM [Users] WITH (UPDLOCK, ROWLOCK)
    WHERE [user_name] = @login";
                        string dbPassword = "";
                        string roleName = "";
                        int attempts = 0;
                        bool userFound = false;
                        bool blocked = false;

                        /*string sql = @"
                        SELECT *
                        FROM [Users]
                        WHERE [user_name] = @login AND [password] = @password";*/

                        using (SqlCommand cmd = new SqlCommand(sql, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@login", login);
                            cmd.Parameters.AddWithValue("@password", password);

                            using (SqlDataReader r = cmd.ExecuteReader())
                            {
                                if (r.Read())
                                {

                                    userFound = true;
                                    dbPassword = Convert.ToString(r["password"]);
                                    roleName = Convert.ToString(r["role"]);
                                    attempts = Convert.ToInt32(r["failed_attempts"]);
                                    blocked = Convert.ToBoolean(r["is_blocked"]);
                                }
                            }
                        }
                        if (!userFound)
                        {
                            transaction.Commit();
                            MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        if (blocked)
                        {
                            transaction.Commit();
                            MessageBox.Show("Учетная запись заблокирована", "Блокировка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (!string.Equals(dbPassword, password, StringComparison.Ordinal))
                        {
                            attempts++;
                            bool needBlock = attempts >= 3;
                            string updateSql = @"UPDATE[Users] SET [failed_attempts] = @attempts, [is_blocked] = @blocked WHERE [user_name] = @login";
                            using (SqlCommand cmd = new SqlCommand(updateSql, connection, transaction))
                            {
                                cmd.Parameters.AddWithValue("@attempts", attempts);
                                cmd.Parameters.AddWithValue("@blocked", needBlock);
                                cmd.Parameters.AddWithValue("@login", login);
                                cmd.ExecuteNonQuery();
                            }
                            transaction.Commit();
                            if (needBlock)
                            {
                                MessageBox.Show("Пароль введен неверно 3 раза\n" +
                                    "Учетная запись забокирована.",
                                            "Блокировка",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error);
                            }
                            else
                            {
                                MessageBox.Show("Неверный логин или пароль\n" +
                                    "осталось попыток:" + (3 - attempts),
                                            "Ошибка",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Warning);
                            }
                            return;

                        }
                        string resetSql = @"UPDATE [Users] SET [failed_attempts] = 0 WHERE [user_name] = @login";

                        using (SqlCommand cmd = new SqlCommand(resetSql, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@login", login);
                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                        LoginClass.Role = ParseRole(roleName);

                        MessageBox.Show(
                            "Успешный вход!\n",
                            "Авторизация",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                        Form nextForm;
                        if (LoginClass.Role == LoginClass.UserRole.Admin)
                        {
                            nextForm = new AdminPanel();

                        }
                        else
                        {
                            nextForm = new ClientPanel();
                        }
                        nextForm.FormClosed += (s, args) => this.Close();
                        nextForm.Show();
                        this.Hide();
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
