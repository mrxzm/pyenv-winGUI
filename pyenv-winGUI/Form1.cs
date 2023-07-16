using System.Diagnostics;
using System.Runtime.InteropServices;

namespace pyenv_winGUI
{
    public partial class Form1 : Form
    {
        private CheckedListBox checkedListBox1;

        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole(); //��ʾ����̨
        [DllImport("kernel32.dll")]
        public static extern Boolean FreeConsole(); //�ͷſ���̨���رտ���̨

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
                pyenv_path.Text = "δ����";
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
            DialogResult msg = MessageBox.Show("���ڳ�ʼ��");
            // д�뻷������

        }

        /// <summary>
        /// ѡ�� PYENV �ļ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pyenv_path_DoubleClick(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "��ѡ���ļ���";
            dialog.SelectedPath = pyenv_path.Text == "δ����" ? System.IO.Directory.GetCurrentDirectory() : pyenv_path.Text;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string file = dialog.SelectedPath;
                if (File.Exists(file))
                {
                    MessageBox.Show(this, "�ļ���·������Ϊ��", "��ʾ");
                }
                else
                {
                    MessageBox.Show("��ѡ��" + file);
                    // �ж��Ƿ���PYENV���ļ���



                }

            }

        }

        /// <summary>
        /// ��װ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {

            List<string> systemList = Cmd.GetSystemPythonVersions();
            List<string> checkedList = checkedListBox1.CheckedItems.Cast<string>().ToList();
            // ��ȡѡ��İ汾��Ϣ
            List<string> installList = checkedList.Except(systemList).ToList();
            List<string> uninstallList = systemList.Except(checkedList).ToList();
            string msg = "������װ��" + string.Join(", ", installList);
            msg += "\r\nж�أ�" + string.Join(", ", uninstallList);
            if (installList.Count <= 0 && uninstallList.Count <= 0)
            {
                msg = "��ѡ����Ҫ���µİ���";
            }

            DialogResult dialogResult = MessageBox.Show(msg, "����Python��", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                // ִ��һ����ж�غ�װ
                Cmd.UninstallPythonPackages(uninstallList.ToArray());
                Cmd.InstallPythonPackages(installList.ToArray());

            }
        }

        /// <summary>
        /// ˢ���б�
        /// </summary>
        public void RefreshCheckedListBox()
        {
            // ��ѡ����ʼֵ
            checkedListBox1.Items.AddRange(Cmd.GetInstallPythonVersions().ToArray());
            List<string> checkList = Cmd.GetSystemPythonVersions();
            foreach (var item in checkList)
            {
                int index = checkedListBox1.Items.IndexOf(item);
                if (index < 0)
                {
                    // ����Ӧ�õ�������
                }
                checkedListBox1.SetItemChecked(index, true);
            }
        }

        /// <summary>
        /// �л�Python�汾
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            // ��һ��ѡ���
            SwitchPythonForm switchPythonForm = new SwitchPythonForm();
            switchPythonForm.RunRefresh += RefreshVersion;
            switchPythonForm.ShowDialog();
        }

        /// <summary>
        /// ˢ�µ�ǰ�汾��Ϣ
        /// </summary>
        private void RefreshVersion()
        {
            string version = Cmd.GetSystemRunPythonVersion();
            if (string.IsNullOrEmpty(version))
            {
                label5.Text = "��";
            }
            else
            {
                label5.Text = version;
            }

        }
    }
}