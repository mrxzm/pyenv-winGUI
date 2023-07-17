using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace pyenv_winGUI.utils
{
    internal class Cmd
    {
        public Cmd()
        {

        }


        /// <summary>
        /// 获取 Python 所有版本信息
        /// </summary>
        /// <returns></returns>
        public static List<string> GetInstallPythonVersions()
        {
            string cmdStr = "pyenv install --list";
            string result = execCMD(cmdStr);
            List<string> list = new List<string>();
            list = result.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (list[i] == cmdStr || list[i].StartsWith(":: [Info] ::") || list[i].EndsWith(">exit")
                    || string.IsNullOrEmpty(list[i]))
                {
                    list.Remove(list[i]);
                }
            }
            //System.Console.WriteLine(result);
            return list;
        }


        /// <summary>
        /// 获取系统内已经安装的Python 版本信息
        /// </summary>
        /// <returns></returns>
        public static List<string> GetSystemPythonVersions()
        {
            List<string> list = new List<string>();
            string cmdStr = "pyenv versions";
            string result = execCMD(cmdStr);
            if (!string.IsNullOrEmpty(result))
            {
                list = result.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (list[i] == cmdStr
                        || list[i].EndsWith(">exit")
                        || string.IsNullOrEmpty(list[i]))
                    {
                        list.Remove(list[i]);
                    }
                }
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Substring(0, 1) == "*")
                    {
                        list[i] = list[i].Substring(1);
                        list[i] = list[i].Substring(0, list[i].IndexOf("(set by"));
                    }
                    list[i] = list[i].Trim();
                }
            }
            return list;
        }

        /// <summary>
        /// 获取当前已安装的Python版本
        /// </summary>
        /// <returns></returns>
        public static string GetSystemRunPythonVersion()
        {
            string cmdStr = "pyenv global";
            string result = execCMD(cmdStr);
            if (string.IsNullOrEmpty(result)) 
            {
                return null;
            }
            List<string> list = result.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (list[i] == cmdStr
                    || list[i].EndsWith(">exit")
                    || string.IsNullOrEmpty(list[i]))
                {
                    list.Remove(list[i]);
                }
            }
            //System.Console.WriteLine(result);
            return list.Count == 0 ? null : list[0];
        }

        /// <summary>
        /// 切换当前系统运行的Python 版本
        /// </summary>
        /// <returns></returns>
        public static bool SwitchSystemRunPythonVersion(string version)
        {
            try
            {
                string cmdStr = "pyenv global " + version;
                string result = execCMD(cmdStr);
                Console.WriteLine(result);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 卸载多个包
        /// </summary>
        /// <param name="versions"></param>
        /// <returns></returns>
        public static bool UninstallPythonPackages(string[] versions)
        {
            if (versions.Length <= 0)
            {
                return false;
            }
            string[] strings = new string[versions.Length];
            for (int i = 0; i < versions.Length; i++)
            {
                strings[i] = "pyenv uninstall " + versions[i];
            }
            execCMDList(strings);
            return true;
        }

        /// <summary>
        /// 安装多个包
        /// </summary>
        /// <param name="versions"></param>
        /// <returns></returns>
        public static bool InstallPythonPackages(string[] versions)
        {
            if (versions.Length <= 0)
            {
                return false;
            }
            string[] strings = new string[versions.Length];
            for (int i = 0; i < versions.Length; i++)
            {
                strings[i] = "pyenv install " + versions[i];
            }
            execCMDList(strings);
            return true;
        }

        /// <summary>
        /// 卸载指定包
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public static bool execCMDList(string[] comment)
        {
            Thread t = new Thread(() =>
            {
                List<string> outputList = new List<string>();
                using (Process pro = new Process())
                {
                    pro.StartInfo.FileName = "cmd.exe";
                    pro.StartInfo.UseShellExecute = false;
                    //pro.StartInfo.RedirectStandardError = true;
                    pro.StartInfo.RedirectStandardInput = true;
                    //pro.StartInfo.RedirectStandardOutput = true;
                    pro.Start();
                    foreach (string com in comment)
                    {
                        pro.StandardInput.WriteLine(com);//输入CMD命令
                    }
                    pro.StandardInput.WriteLine("echo Automatically exit after 10 seconds.");
                    Thread.Sleep(1000 * 10);
                    pro.StandardInput.WriteLine("exit");
                    pro.StandardInput.AutoFlush = true;
                    pro.WaitForExit();//等待程序执行完退出进程
                    pro.Close();
                }
            });
            t.Start();
            return true;
        }



        public static string execCMD(string command)
        {
            Process pro = new Process();
            pro.StartInfo.FileName = "cmd.exe";
            pro.StartInfo.UseShellExecute = false;
            pro.StartInfo.RedirectStandardError = true;
            pro.StartInfo.RedirectStandardInput = true;
            pro.StartInfo.RedirectStandardOutput = true;
            pro.StartInfo.CreateNoWindow = true;
            //pro.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            pro.Start();
            pro.StandardInput.WriteLine(command);
            pro.StandardInput.WriteLine("exit");
            pro.StandardInput.AutoFlush = true;
            //获取cmd窗口的输出信息
            string output = pro.StandardOutput.ReadToEnd();
            pro.WaitForExit();//等待程序执行完退出进程
            pro.Close();
            return output.Substring(output.IndexOf(command), output.Length - output.IndexOf(command));

        }


    }
}
