namespace TM.Infrastructure.Files
{
    public class FileUtils
    {
        private static string DomainUrl = "work.teamax-wx.com";
        /// <summary>
        /// 返回本地或远程对应路径
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public static string GetUrlPath(string strPath)
        {
            if (string.IsNullOrEmpty(strPath))
                return strPath;
            var bValite = strPath.StartsWith("http");
            if (bValite)
                return strPath;
            else
                return "http://" + DomainUrl + strPath + "";
        }
    }
}
