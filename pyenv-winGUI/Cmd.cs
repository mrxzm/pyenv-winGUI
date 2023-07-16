using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace pyenv_winGUI
{
    internal class Cmd
    {
        public Cmd()
        {
            
        }


        /// <summary>
        /// 获取Python 所有版本信息
        /// </summary>
        /// <returns></returns>
        public static List<string> GetInstallPythonVersions() 
        {
            String cmdStr = "pyenv install --list";
            String result = execCMD(cmdStr);
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
            String cmdStr = "pyenv versions";
            String result = execCMD(cmdStr);
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
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Substring(0, 1) == "*") 
                {
                    list[i] = list[i].Substring(1);
                    list[i] = list[i].Substring(0, list[i].IndexOf("(set by"));
                }
                list[i] = list[i].Trim();
            }
            //System.Console.WriteLine(result);
            return list;
        }

        /// <summary>
        /// 获取当前已安装的Python版本
        /// </summary>
        /// <returns></returns>
        public static string GetSystemRunPythonVersion()
        {
            String cmdStr = "pyenv global";
            String result = execCMD(cmdStr);
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
            return list[0];
        }

        /// <summary>
        /// 切换当前系统运行的Python 版本
        /// </summary>
        /// <returns></returns>
        public static bool SwitchSystemRunPythonVersion(string version)
        {
            try
            {
                String cmdStr = "pyenv global " + version;
                String result = execCMD(cmdStr);
                System.Console.WriteLine(result);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }



        public static string execCMD(string command)
        {
            System.Diagnostics.Process pro = new System.Diagnostics.Process();
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
