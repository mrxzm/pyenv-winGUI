using System.Diagnostics;
using System.Runtime.InteropServices;

namespace pyenv_winGUI
{
    public partial class Form1 : Form
    {
        private CheckedListBox checkedListBox1;

        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole(); //显示控制台
        [DllImport("kernel32.dll")]
        public static extern Boolean FreeConsole(); //释放控制台、关闭控制台

        public Form1()
        {
            InitializeComponent();
            InitCheckedListBox();
            checkedListBox1.Items.Clear();
            if (EnvironmentVariable.IsContains())
            {
                pyenv_path.Text = EnvironmentVariable.GetPYENV();
                button1.Enabled = false;
                RefreshCheckedListBox();
                RefreshVersion();
            }
            else
            {
                pyenv_path.Text = "未配置";
                button1.Enabled = true;
            }
            //
        }

        public void InitCheckedListBox()
        {
            checkedListBox1 = new ColorCodedCheckedListBox();
            // 
            // checkedListBox1
            // 
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Location = new Point(47, 87);
            checkedListBox1.MultiColumn = true;
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new Size(699, 310);
            checkedListBox1.TabIndex = 4;
            base.Controls.Add(checkedListBox1);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult msg = MessageBox.Show("正在初始化");
            // 写入环境变量

        }

        /// <summary>
        /// 选择 PYENV 文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pyenv_path_DoubleClick(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件夹";
            dialog.SelectedPath = pyenv_path.Text == "未配置" ? System.IO.Directory.GetCurrentDirectory() : pyenv_path.Text;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string file = dialog.SelectedPath;
                if (File.Exists(file))
                {
                    MessageBox.Show(this, "文件夹路径不能为空", "提示");
                }
                else
                {
                    MessageBox.Show("已选择：" + file);
                    // 判断是否是PYENV的文件夹



                }

            }

        }

        /// <summary>
        /// 安装
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {

            List<string> systemList = Cmd.GetSystemPythonVersions();
            List<string> checkedList = checkedListBox1.CheckedItems.Cast<string>().ToList();
            // 获取选择的版本信息
            List<string> installList = checkedList.Except(systemList).ToList();
            List<string> uninstallList = systemList.Except(checkedList).ToList();
            string msg = "即将安装：" + string.Join(", ", installList);
            msg += "\r\n卸载：" + string.Join(", ", uninstallList);
            if (installList.Count <= 0 && uninstallList.Count <= 0)
            {
                msg = "请选择需要更新的包！";
            }

            DialogResult dialogResult = MessageBox.Show(msg, "更新Python包", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                // 执行一个个卸载后安装
                Cmd.UninstallPythonPackages(uninstallList.ToArray());
                Cmd.InstallPythonPackages(installList.ToArray());

            }
        }

        /// <summary>
        /// 刷新列表
        /// </summary>
        public void RefreshCheckedListBox()
        {
            // 给选择框初始值
            checkedListBox1.Items.AddRange(Cmd.GetInstallPythonVersions().ToArray());
            List<string> checkList = Cmd.GetSystemPythonVersions();
            foreach (var item in checkList)
            {
                int index = checkedListBox1.Items.IndexOf(item);
                if (index < 0)
                {
                    // 这里应该弹窗报错
                }
                checkedListBox1.SetItemChecked(index, true);
            }
        }

        /// <summary>
        /// 切换Python版本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            // 打开一个选择框
            SwitchPythonForm switchPythonForm = new SwitchPythonForm();
            switchPythonForm.RunRefresh += RefreshVersion;
            switchPythonForm.ShowDialog();
        }

        /// <summary>
        /// 刷新当前版本信息
        /// </summary>
        private void RefreshVersion()
        {
            string version = Cmd.GetSystemRunPythonVersion();
            if (string.IsNullOrEmpty(version))
            {
                label5.Text = "无";
            }
            else
            {
                label5.Text = version;
            }

        }
    }
}