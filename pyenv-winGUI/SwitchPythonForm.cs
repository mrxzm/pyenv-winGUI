using pyenv_winGUI.utils;

namespace pyenv_winGUI
{
    public partial class SwitchPythonForm : Form
    {
        public SwitchPythonForm()
        {
            InitializeComponent();
            // 初始化下拉框内的值
            List<string> checkList = Cmd.GetSystemPythonVersions();
            comboBox1.Items.AddRange(checkList.ToArray());
            string version = Cmd.GetSystemRunPythonVersion();
            if (!string.IsNullOrEmpty(version))
            {
                comboBox1.SelectedItem = version;
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 确定选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("未选择版本");
                return;
            }
            _ = Cmd.SwitchSystemRunPythonVersion(comboBox1.SelectedItem.ToString());
            RunRefresh();
            this.Close();
        }

        public delegate void Refresh();

        public event Refresh RunRefresh;
    }
}
