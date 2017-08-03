namespace 教务系统是小妖精吗
{
    partial class LoginForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.LoginIn = new System.Windows.Forms.Button();
            this.ViewPic = new System.Windows.Forms.PictureBox();
            this.ViewCode = new System.Windows.Forms.TextBox();
            this.TestView = new System.Windows.Forms.Button();
            this.Year = new System.Windows.Forms.TextBox();
            this.Time = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.TermYear = new System.Windows.Forms.ComboBox();
            this.TermIndex = new System.Windows.Forms.ComboBox();
            this.LoginGroupBox = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.StuAccount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.StuPassword = new System.Windows.Forms.TextBox();
            this.ResetTextBox = new System.Windows.Forms.Button();
            this.LogOut = new System.Windows.Forms.Button();
            this.ClassTableQuery = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Query = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ViewPic)).BeginInit();
            this.LoginGroupBox.SuspendLayout();
            this.ClassTableQuery.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoginIn
            // 
            this.LoginIn.Location = new System.Drawing.Point(6, 141);
            this.LoginIn.Name = "LoginIn";
            this.LoginIn.Size = new System.Drawing.Size(75, 23);
            this.LoginIn.TabIndex = 3;
            this.LoginIn.Tag = "";
            this.LoginIn.Text = "登陆";
            this.LoginIn.UseVisualStyleBackColor = true;
            this.LoginIn.Click += new System.EventHandler(this.button1_Click);
            // 
            // ViewPic
            // 
            this.ViewPic.Location = new System.Drawing.Point(103, 103);
            this.ViewPic.Name = "ViewPic";
            this.ViewPic.Size = new System.Drawing.Size(72, 27);
            this.ViewPic.TabIndex = 2;
            this.ViewPic.TabStop = false;
            this.ViewPic.Click += new System.EventHandler(this.ViewPic_Click);
            // 
            // ViewCode
            // 
            this.ViewCode.Location = new System.Drawing.Point(75, 76);
            this.ViewCode.Name = "ViewCode";
            this.ViewCode.Size = new System.Drawing.Size(100, 21);
            this.ViewCode.TabIndex = 2;
            // 
            // TestView
            // 
            this.TestView.Location = new System.Drawing.Point(24, 247);
            this.TestView.Name = "TestView";
            this.TestView.Size = new System.Drawing.Size(75, 23);
            this.TestView.TabIndex = 6;
            this.TestView.Tag = "";
            this.TestView.Text = "测试";
            this.TestView.UseVisualStyleBackColor = true;
            this.TestView.Click += new System.EventHandler(this.TestView_Click);
            // 
            // Year
            // 
            this.Year.Location = new System.Drawing.Point(141, 247);
            this.Year.Name = "Year";
            this.Year.Size = new System.Drawing.Size(69, 21);
            this.Year.TabIndex = 8;
            // 
            // Time
            // 
            this.Time.Location = new System.Drawing.Point(223, 247);
            this.Time.Name = "Time";
            this.Time.Size = new System.Drawing.Size(69, 21);
            this.Time.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 216);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 10;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // TermYear
            // 
            this.TermYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TermYear.FormattingEnabled = true;
            this.TermYear.Location = new System.Drawing.Point(69, 24);
            this.TermYear.Name = "TermYear";
            this.TermYear.Size = new System.Drawing.Size(94, 20);
            this.TermYear.TabIndex = 11;
            // 
            // TermIndex
            // 
            this.TermIndex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TermIndex.FormattingEnabled = true;
            this.TermIndex.Items.AddRange(new object[] {
            "第1学期",
            "第2学期"});
            this.TermIndex.Location = new System.Drawing.Point(69, 58);
            this.TermIndex.Name = "TermIndex";
            this.TermIndex.Size = new System.Drawing.Size(77, 20);
            this.TermIndex.TabIndex = 12;
            // 
            // LoginGroupBox
            // 
            this.LoginGroupBox.Controls.Add(this.LogOut);
            this.LoginGroupBox.Controls.Add(this.ResetTextBox);
            this.LoginGroupBox.Controls.Add(this.label4);
            this.LoginGroupBox.Controls.Add(this.StuPassword);
            this.LoginGroupBox.Controls.Add(this.label3);
            this.LoginGroupBox.Controls.Add(this.StuAccount);
            this.LoginGroupBox.Controls.Add(this.label2);
            this.LoginGroupBox.Controls.Add(this.ViewCode);
            this.LoginGroupBox.Controls.Add(this.ViewPic);
            this.LoginGroupBox.Controls.Add(this.LoginIn);
            this.LoginGroupBox.Location = new System.Drawing.Point(24, 21);
            this.LoginGroupBox.Name = "LoginGroupBox";
            this.LoginGroupBox.Size = new System.Drawing.Size(187, 170);
            this.LoginGroupBox.TabIndex = 13;
            this.LoginGroupBox.TabStop = false;
            this.LoginGroupBox.Text = "登陆区";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(6, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 21);
            this.label2.TabIndex = 6;
            this.label2.Text = "验证码:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(22, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 21);
            this.label3.TabIndex = 8;
            this.label3.Text = "学号:";
            // 
            // StuAccount
            // 
            this.StuAccount.Location = new System.Drawing.Point(75, 17);
            this.StuAccount.Name = "StuAccount";
            this.StuAccount.Size = new System.Drawing.Size(100, 21);
            this.StuAccount.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(22, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 21);
            this.label4.TabIndex = 10;
            this.label4.Text = "密码:";
            // 
            // StuPassword
            // 
            this.StuPassword.Location = new System.Drawing.Point(75, 46);
            this.StuPassword.Name = "StuPassword";
            this.StuPassword.PasswordChar = '*';
            this.StuPassword.Size = new System.Drawing.Size(100, 21);
            this.StuPassword.TabIndex = 1;
            // 
            // ResetTextBox
            // 
            this.ResetTextBox.Location = new System.Drawing.Point(100, 141);
            this.ResetTextBox.Name = "ResetTextBox";
            this.ResetTextBox.Size = new System.Drawing.Size(75, 23);
            this.ResetTextBox.TabIndex = 4;
            this.ResetTextBox.Tag = "";
            this.ResetTextBox.Text = "重填";
            this.ResetTextBox.UseVisualStyleBackColor = true;
            this.ResetTextBox.Click += new System.EventHandler(this.ResetTextBox_Click);
            // 
            // LogOut
            // 
            this.LogOut.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LogOut.Location = new System.Drawing.Point(36, 54);
            this.LogOut.Name = "LogOut";
            this.LogOut.Size = new System.Drawing.Size(117, 54);
            this.LogOut.TabIndex = 14;
            this.LogOut.Text = "退出登录";
            this.LogOut.UseVisualStyleBackColor = true;
            this.LogOut.Visible = false;
            this.LogOut.Click += new System.EventHandler(this.LogOut_Click);
            // 
            // ClassTableQuery
            // 
            this.ClassTableQuery.Controls.Add(this.Query);
            this.ClassTableQuery.Controls.Add(this.label6);
            this.ClassTableQuery.Controls.Add(this.label5);
            this.ClassTableQuery.Controls.Add(this.TermYear);
            this.ClassTableQuery.Controls.Add(this.TermIndex);
            this.ClassTableQuery.Enabled = false;
            this.ClassTableQuery.Location = new System.Drawing.Point(223, 21);
            this.ClassTableQuery.Name = "ClassTableQuery";
            this.ClassTableQuery.Size = new System.Drawing.Size(188, 134);
            this.ClassTableQuery.TabIndex = 14;
            this.ClassTableQuery.TabStop = false;
            this.ClassTableQuery.Text = "课表查询区";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(6, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 21);
            this.label5.TabIndex = 15;
            this.label5.Text = "学年:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(6, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 21);
            this.label6.TabIndex = 16;
            this.label6.Text = "学期:";
            // 
            // Query
            // 
            this.Query.Location = new System.Drawing.Point(51, 94);
            this.Query.Name = "Query";
            this.Query.Size = new System.Drawing.Size(75, 23);
            this.Query.TabIndex = 17;
            this.Query.Text = "查询";
            this.Query.UseVisualStyleBackColor = true;
            this.Query.Click += new System.EventHandler(this.Query_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(436, 293);
            this.Controls.Add(this.ClassTableQuery);
            this.Controls.Add(this.LoginGroupBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Time);
            this.Controls.Add(this.Year);
            this.Controls.Add(this.TestView);
            this.Name = "LoginForm";
            this.Text = "教务系统模拟登陆程序";
            ((System.ComponentModel.ISupportInitialize)(this.ViewPic)).EndInit();
            this.LoginGroupBox.ResumeLayout(false);
            this.LoginGroupBox.PerformLayout();
            this.ClassTableQuery.ResumeLayout(false);
            this.ClassTableQuery.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LoginIn;
        private System.Windows.Forms.PictureBox ViewPic;
        private System.Windows.Forms.TextBox ViewCode;
        private System.Windows.Forms.Button TestView;
        private System.Windows.Forms.TextBox Year;
        private System.Windows.Forms.TextBox Time;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ComboBox TermYear;
        private System.Windows.Forms.ComboBox TermIndex;
        private System.Windows.Forms.GroupBox LoginGroupBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox StuAccount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ResetTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox StuPassword;
        private System.Windows.Forms.Button LogOut;
        private System.Windows.Forms.GroupBox ClassTableQuery;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button Query;
    }
}

