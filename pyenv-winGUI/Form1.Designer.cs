namespace pyenv_winGUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            label1 = new Label();
            pyenv_path = new TextBox();
            label2 = new Label();
            button2 = new Button();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            button3 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(44, 19);
            button1.Name = "button1";
            button1.Size = new Size(108, 23);
            button1.TabIndex = 0;
            button1.Text = "初始化环境变量";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(158, 22);
            label1.Name = "label1";
            label1.Size = new Size(87, 17);
            label1.TabIndex = 1;
            label1.Text = "PYENV 路径：";
            // 
            // pyenv_path
            // 
            pyenv_path.Location = new Point(241, 19);
            pyenv_path.Name = "pyenv_path";
            pyenv_path.Size = new Size(505, 23);
            pyenv_path.TabIndex = 2;
            pyenv_path.DoubleClick += pyenv_path_DoubleClick;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(49, 66);
            label2.Name = "label2";
            label2.Size = new Size(116, 17);
            label2.TabIndex = 3;
            label2.Text = "选择需要安装的版本";
            // 
            // button2
            // 
            button2.Location = new Point(47, 403);
            button2.Name = "button2";
            button2.Size = new Size(86, 35);
            button2.TabIndex = 5;
            button2.Text = "安装";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(139, 415);
            label3.Name = "label3";
            label3.Size = new Size(68, 17);
            label3.TabIndex = 6;
            label3.Text = "安装成功！";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(508, 415);
            label4.Name = "label4";
            label4.Size = new Size(155, 17);
            label4.TabIndex = 7;
            label4.Text = "当前系统环境Python版本：";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(669, 415);
            label5.Name = "label5";
            label5.Size = new Size(20, 17);
            label5.TabIndex = 8;
            label5.Text = "无";
            // 
            // button3
            // 
            button3.Location = new Point(416, 403);
            button3.Name = "button3";
            button3.Size = new Size(86, 35);
            button3.TabIndex = 9;
            button3.Text = "切换版本";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 457);
            Controls.Add(button3);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(button2);
            Controls.Add(label2);
            Controls.Add(pyenv_path);
            Controls.Add(label1);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "pyenv-win GUI";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Label label1;
        private TextBox pyenv_path;
        private Label label2;
        private Button button2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Button button3;
    }
}