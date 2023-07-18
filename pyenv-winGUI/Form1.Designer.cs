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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            button1 = new Button();
            label1 = new Label();
            pyenv_path = new TextBox();
            label2 = new Label();
            button2 = new Button();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            button3 = new Button();
            toolTip1 = new ToolTip(components);
            button4 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(88, 35);
            button1.Margin = new Padding(6, 5, 6, 5);
            button1.Name = "button1";
            button1.Size = new Size(216, 42);
            button1.TabIndex = 0;
            button1.Text = "安装PYENV";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(316, 40);
            label1.Margin = new Padding(6, 0, 6, 0);
            label1.Name = "label1";
            label1.Size = new Size(171, 31);
            label1.TabIndex = 1;
            label1.Text = "PYENV 路径：";
            // 
            // pyenv_path
            // 
            pyenv_path.Location = new Point(482, 35);
            pyenv_path.Margin = new Padding(6, 5, 6, 5);
            pyenv_path.Name = "pyenv_path";
            pyenv_path.Size = new Size(1006, 38);
            pyenv_path.TabIndex = 2;
            pyenv_path.DoubleClick += pyenv_path_DoubleClick;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(98, 120);
            label2.Margin = new Padding(6, 0, 6, 0);
            label2.Name = "label2";
            label2.Size = new Size(230, 31);
            label2.TabIndex = 3;
            label2.Text = "选择需要安装的版本";
            // 
            // button2
            // 
            button2.Location = new Point(94, 735);
            button2.Margin = new Padding(6, 5, 6, 5);
            button2.Name = "button2";
            button2.Size = new Size(172, 64);
            button2.TabIndex = 5;
            button2.Text = "安装-卸载";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(278, 757);
            label3.Margin = new Padding(6, 0, 6, 0);
            label3.Name = "label3";
            label3.Size = new Size(0, 31);
            label3.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(1016, 757);
            label4.Margin = new Padding(6, 0, 6, 0);
            label4.Name = "label4";
            label4.Size = new Size(312, 31);
            label4.TabIndex = 7;
            label4.Text = "当前系统环境Python版本：";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(1338, 757);
            label5.Margin = new Padding(6, 0, 6, 0);
            label5.Name = "label5";
            label5.Size = new Size(38, 31);
            label5.TabIndex = 8;
            label5.Text = "无";
            // 
            // button3
            // 
            button3.Location = new Point(832, 735);
            button3.Margin = new Padding(6, 5, 6, 5);
            button3.Name = "button3";
            button3.Size = new Size(172, 64);
            button3.TabIndex = 9;
            button3.Text = "切换版本";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(1338, 105);
            button4.Name = "button4";
            button4.Size = new Size(150, 46);
            button4.TabIndex = 10;
            button4.Text = "更新列表";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(14F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1600, 833);
            Controls.Add(button4);
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
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(6, 5, 6, 5);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "pyenv-win GUI";
            Load += Form1_Load;
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
        private ToolTip toolTip1;
        private Button button4;
    }
}