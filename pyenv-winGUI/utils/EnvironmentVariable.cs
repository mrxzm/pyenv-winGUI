using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace pyenv_winGUI.utils
{
    /// <summary>
    /// 系统环境变量
    /// </summary>
    internal class EnvironmentVariable
    {
        /// <summary>
        /// 判断PYENV环境变量是否存在
        /// </summary>
        /// <returns></returns>
        public static bool IsContains()
        {
            if (GetPYENV() != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取PYENV环境变量
        /// </summary>
        /// <returns></returns>
        public static string GetPYENV()
        {
            string PYENV = Environment.GetEnvironmentVariable("PYENV");
            //MessageBox.Show(PYENV);
            //PYENV = null;
            // TODO 测试
            return PYENV;
        }

        /// <summary>
        /// 设置PYENV环境变量
        /// </summary>
        /// <param name="PYENV"></param>
        public static void SetPYENV(string PYENV)
        {
            if (PYENV != null)
            {
                WindowsIdentity identity = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                if (!principal.IsInRole(WindowsBuiltInRole.Administrator))
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.UseShellExecute = true;
                    startInfo.WorkingDirectory = Environment.CurrentDirectory;
                    startInfo.FileName = Application.ExecutablePath;
                    //设置启动动作,确保以管理员身份运行
                    startInfo.Verb = "runas";
                    startInfo.Arguments = "install " + Convert.ToBase64String(Encoding.UTF8.GetBytes(PYENV));
                    try
                    {
                        Process.Start(startInfo);
                    }
                    catch
                    {
                        return;
                    }
                    // 关闭原先程序
                    Application.Exit();
                }
                try
                {
                    Environment.SetEnvironmentVariable("PYENV", PYENV, EnvironmentVariableTarget.Machine);
                    string path = Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.Machine);
                    if (path == null)
                    {
                        MessageBox.Show("系统 Path 为空，请检查系统环境变量！");
                        return;
                    }
                    if (!path.Contains(@"%PYENV%\bin") && !path.Contains(PYENV + @"\bin"))
                    {
                        path += ";" + "%PYENV%\\bin";
                        Environment.SetEnvironmentVariable("Path", path, EnvironmentVariableTarget.Machine);
                    }
                    if (!path.Contains(@"%PYENV%\shims") && !path.Contains(PYENV + @"\shims"))
                    {
                        path += ";" + "%PYENV%\\shims";
                        Environment.SetEnvironmentVariable("Path", path, EnvironmentVariableTarget.Machine);
                    }
                }
                catch (SecurityException e)
                {
                    MessageBox.Show("设置系统环境变量失败！");
                }
            }

        }



    }
}
