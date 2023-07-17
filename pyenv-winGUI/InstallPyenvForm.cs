using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using pyenv_winGUI.utils;
using static pyenv_winGUI.InstallPyenvForm;

namespace pyenv_winGUI
{
    public partial class InstallPyenvForm : Form
    {
        public delegate void Refresh();

        public event Refresh RunRefresh;

        private WebRequest httpRequest;
        private WebResponse httpResponse;
        private byte[] buffer;
        private Thread downloadThread;
        Stream ns;
        string zipFilePath;
        private FileStream fs;
        private long length;
        private long downlength = 0;
        private long lastlength = 0;
        public delegate void updateData(string value); //设置委托用来更新主界面
        private int totalseconds = 0; //总用时
        private updateData UIDel;
        private bool downok = false;
        private string filePath;

        public InstallPyenvForm(string path)
        {
            InitializeComponent();
            buffer = new byte[100000];
            filePath = path;
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
            comboBox1.Visible = false;
            progressBar1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = true;
            label2.BringToFront();
            label3.BringToFront();
            button3.Enabled = false;
            label1.Text = "准备下载。。。";
            string filename = comboBox1.SelectedItem.ToString() + ".zip";
            this.zipFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);
            this.Text = "下载" + filename;
            string url = "https://github.com/pyenv-win/pyenv-win/archive/refs/tags/" + filename;
            httpRequest = WebRequest.Create(url);
            httpResponse = httpRequest.GetResponse();
            //MessageBox.Show(httpResponse.ContentLength.ToString());
            length = httpResponse.ContentLength;
            if (length <= 0)
            {
                MessageBox.Show("下载失败！");
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                label1.Text = "选择需要安装版本：";
                comboBox1.Visible = true;
                progressBar1.Visible = false;

                return;
            }
            this.progressBar1.Maximum = (int)length;
            this.progressBar1.Minimum = 0;
            downloadThread = new Thread(new ThreadStart(downloadFile));
            //showThread = new Thread(new ThreadStart(show));

            fs = new FileStream(this.zipFilePath, FileMode.OpenOrCreate, FileAccess.Write);
            downloadThread.Start();
            this.timer1.Enabled = true;


            //_ = Cmd.SwitchSystemRunPythonVersion(comboBox1.SelectedItem.ToString());
            //RunRefresh();
            //this.Close();
        }

        private void InstallPyenvForm_Load(object sender, EventArgs e)
        {
            // 初始化下拉框内的值
            string[] versions = HttpPYENV.getPYENVVersions();
            if (versions.Length == 0)
            {
                this.Close();
            }
            comboBox1.Items.AddRange(versions);
        }




        private void downloadFile()
        {

            ns = httpResponse.GetResponseStream();

            int i;
            UIDel = new updateData(updateUI);
            while ((i = ns.Read(buffer, 0, buffer.Length)) > 0)
            {

                downlength += i;
                string value = downlength.ToString();
                this.Invoke(UIDel, value);
                fs.Write(buffer, 0, i);
            }
            MessageBox.Show("下载完毕");
            this.timer1.Enabled = false;
            // 释放占用
            fs.Flush();
            fs.Close();

            this.label3.BeginInvoke((Action)delegate
            {
                this.label3.Text = (length / (1024 * totalseconds)) + "KB/s";
                // 解压文件
                if (Directory.Exists(filePath))     // 返回bool类型，存在返回true，不存在返回false
                {
                    // 判断目录内是否有文件
                    DirectoryInfo directoryInfo = new DirectoryInfo(filePath);
                    if (directoryInfo.GetDirectories().Length > 0 || directoryInfo.GetFiles().Length > 0)
                    {
                        MessageBox.Show("安装目录下" + filePath + "存在其他文件，请自行备份，点ok将全部删除。");
                    }
                    Directory.Delete(filePath, true);
                }
                DirectoryInfo directoryInfo1 = new DirectoryInfo(filePath);
                DirectoryInfo cache = directoryInfo1.Parent.CreateSubdirectory(Guid.NewGuid().ToString());
                ZipFile.ExtractToDirectory( this.zipFilePath, cache.FullName, Encoding.UTF8, true);
                new DirectoryInfo(cache.FullName).GetDirectories()[0].GetDirectories().Where(w => w.Name == "pyenv-win").Single().MoveTo(filePath);
                cache.Delete(true);
                this.button3.Enabled = true;
            });

        }

        void updateUI(string value)
        {
            this.label1.Text = "总大小：" + ConvertFileSize(long.Parse(value));
            this.progressBar1.Value = Int32.Parse(value);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.label2.Text = ConvertFileSize(downlength - lastlength);
            lastlength = downlength;
            totalseconds++;
        }

        /// <summary>
        /// 将文件大小(字节)转换为最适合的显示方式
        /// </summary>
        /// <param name="size">文件字节</param>
        /// <returns>返回转换后的字符串</returns>
        public static string ConvertFileSize(long size)
        {
            string result = "0KB";
            int filelength = size.ToString().Length;
            if (filelength < 4)
                result = size + "byte";
            else if (filelength < 7)
                result = Math.Round(Convert.ToDouble(size / 1024d), 2) + "KB";
            else if (filelength < 10)
                result = Math.Round(Convert.ToDouble(size / 1024d / 1024), 2) + "MB";
            else if (filelength < 13)
                result = Math.Round(Convert.ToDouble(size / 1024d / 1024 / 1024), 2) + "GB";
            else
                result = Math.Round(Convert.ToDouble(size / 1024d / 1024 / 1024 / 1024), 2) + "TB";
            return result;
        }

        /// <summary>
        /// 下载安装完成，关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            this.RunRefresh();
            this.Close();
        }
    }
}