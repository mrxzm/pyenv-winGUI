using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pyenv_winGUI
{
    /// <summary>
    /// 系统环境变量
    /// </summary>
    internal class EnvironmentVariable
    {
        public static bool IsContains() 
        {
            if (GetPYENV() != null )
            {
                return true;
            }
            return false;
        }

        public static string GetPYENV()
        {
            string PYENV = Environment.GetEnvironmentVariable("PYENV");
            //MessageBox.Show(PYENV);

            // TODO 测试
            return PYENV;
        }



    }
}
