using Newtonsoft.Json;
using pyenv_winGUI.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace pyenv_winGUI.utils
{
    /// <summary>
    /// 用于下载 PYENV 的网络管理类
    /// </summary>
    internal class HttpPYENV
    {

        /// <summary>
        /// 获取所有PPENV-win历史版本
        /// https://github.com/pyenv-win/pyenv-win/refs?type=tag
        /// </summary>
        public static string[] getPYENVVersions()
        {
            var versions = new List<string>();
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://github.com/pyenv-win/pyenv-win/refs?type=tag");
            req.Method = "Get";
            req.ContentType = "application/x-www-form-urlencoded";
            req.Accept = "application/json";
            req.Timeout = 1000 * 3;
            try
            {
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                Stream stream = resp.GetResponseStream();
                //获取内容
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    string result = reader.ReadToEnd();
                    if (result != null)
                    {
                        PyenvWinTags tags = JsonConvert.DeserializeObject<PyenvWinTags>(result);
                        return tags.refs;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + "多次失败建议直接前往GitHub进行下载 (https://github.com/pyenv-win/pyenv-win/releases) 并配置系统环境变量(PYENV)");
            }
            return new String[0];

        }


        /// <summary>
        /// 下载文件
        /// https://github.com/pyenv-win/pyenv-win/archive/refs/tags/v3.1.1.zip
        /// </summary>
        /// <param name="savePath">保存路径</param>
        /// <param name="version">版本</param>
        public static async void Download(string savePath, string version)
        {
            string filename = version + ".zip";
            using (var web = new WebClient())
            {
                await web.DownloadFileTaskAsync("https://github.com/pyenv-win/pyenv-win/archive/refs/tags/" + filename, savePath + filename);
            }
              
        }


    }
}
