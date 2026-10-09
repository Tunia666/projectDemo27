namespace projectDemo27
{
    partial class AuthForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.title = new System.Windows.Forms.Label();
            this.labelLogin = new System.Windows.Forms.Label();
            this.labelPaswd = new System.Windows.Forms.Label();
            this.tbLogin = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.btnAuth = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Location = new System.Drawing.Point(186, 83);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(135, 25);
            this.title.TabIndex = 0;
            this.title.Text = "Авторизация";
            // 
            // labelLogin
            // 
            this.labelLogin.AutoSize = true;
            this.labelLogin.Location = new System.Drawing.Point(178, 167);
            this.labelLogin.Name = "labelLogin";
            this.labelLogin.Size = new System.Drawing.Size(68, 25);
            this.labelLogin.TabIndex = 1;
            this.labelLogin.Text = "Логин";
            // 
            // labelPaswd
            // 
            this.labelPaswd.AutoSize = true;
            this.labelPaswd.Location = new System.Drawing.Point(178, 230);
            this.labelPaswd.Name = "labelPaswd";
            this.labelPaswd.Size = new System.Drawing.Size(80, 25);
            this.labelPaswd.TabIndex = 2;
            this.labelPaswd.Text = "Пароль";
            // 
            // tbLogin
            // 
            this.tbLogin.Location = new System.Drawing.Point(311, 167);
            this.tbLogin.Name = "tbLogin";
            this.tbLogin.Size = new System.Drawing.Size(154, 29);
            this.tbLogin.TabIndex = 3;
            this.tbLogin.TextChanged += new System.EventHandler(this.tbLogin_TextChanged);
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(311, 227);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(154, 29);
            this.tbPassword.TabIndex = 4;
            // 
            // btnAuth
            // 
            this.btnAuth.Location = new System.Drawing.Point(191, 310);
            this.btnAuth.Name = "btnAuth";
            this.btnAuth.Size = new System.Drawing.Size(261, 40);
            this.btnAuth.TabIndex = 5;
            this.btnAuth.Text = "Войти";
            this.btnAuth.UseVisualStyleBackColor = true;
            this.btnAuth.Click += new System.EventHandler(this.btnAuth_Click);
            // 
            // AuthForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 504);
            this.Controls.Add(this.btnAuth);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.tbLogin);
            this.Controls.Add(this.labelPaswd);
            this.Controls.Add(this.labelLogin);
            this.Controls.Add(this.title);
            this.Name = "AuthForm";
            this.Text = "Авторизация";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Label labelLogin;
        private System.Windows.Forms.Label labelPaswd;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Button btnAuth;
        public System.Windows.Forms.TextBox tbLogin;
    }
}

