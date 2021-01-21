using System.Runtime.InteropServices;
using System.Text;

namespace Gizza.Data.IO
{
    public class IniFile
    {
        /* 
         * USAGE
         * IniFile iniAGuard = new IniFile(Application.StartupPath + "\\AGuard.ini");
         * string sqlKODB = iniAGuard.Read("SQLServer", "Database");
         * iniAGuard.Write("SQLServer", "Database", cmbKODB.Text);
         * */

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        private string filePath;

        public IniFile(string filePath)
        {
            this.filePath = filePath;
        }

        public void Write(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, this.filePath);
        }

        public string Read(string section, string key)
        {
            StringBuilder SB = new StringBuilder(255);
            int i = GetPrivateProfileString(section, key, "", SB, 255, this.filePath);
            return SB.ToString();
        }

        public string FilePath
        {
            get { return this.filePath; }
            set { this.filePath = value; }
        }
    }
}
