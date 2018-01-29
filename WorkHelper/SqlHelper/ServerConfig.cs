using System;
using System.Web;
using CMS.Utilities.Encryption;
using System.Configuration;
using System.Web.Caching;
using System.Web;

namespace CMS.Utilities
{
    public class ServerConfig
    {

        /// <summary>
        /// ���������ù�����
        /// </summary>
        public ServerConfig()
        {
        }

        /// <summary>
        /// �õ���������
        /// DES����
        /// </summary>
        /// <returns></returns>
        public static string GetLimitDate()
        {
            return DecryptStringReverse(System.Configuration.ConfigurationManager.AppSettings[VariableName.LimitDate]);
        }
        /// <summary>
        /// DES����
        /// </summary>
        /// <param name="encryptText">����</param>
        /// <returns></returns>
        public static string DecryptStringReverse(string encryptText)
        {
            if (IsEncrypt() != Boolean.TrueString) return encryptText;
            return EncryptionDES.DecryptStringReverse(encryptText);
        }
        public static string IsEncrypt()
        {
            return System.Configuration.ConfigurationManager.AppSettings[VariableName.IsEncrypt];
        }
        private static string SafeConfigString(string configKey, string defaultValue)
        {
            string configValue = DecryptStringReverse(System.Configuration.ConfigurationManager.AppSettings[configKey]);
            if (configValue != null)
            {
                return configValue;
            }
            return defaultValue;
        }
        #region ���ݿ�
        /// <summary>
        /// �õ�Access�����ļ�·��
        /// DES����
        /// </summary>
        /// <returns></returns>
        public static string GetAccessDataBasePath()
        {
            return DecryptStringReverse(System.Configuration.ConfigurationManager.AppSettings[VariableName.AccessDataBasePathKey]);
        }
        /// <summary>
        /// �õ����ݿ�ĵ�ַ
        /// DES����
        /// </summary>
        /// <returns></returns>
        public static string GetDBServerAddress()
        {
            return DecryptStringReverse(System.Configuration.ConfigurationManager.AppSettings[VariableName.DBServerAddressKey]);
        }

        /// <summary>
        /// �õ��������ݿ���û���
        /// DES����
        /// </summary>
        /// <returns></returns>
        public static string GetDBServerUser()
        {
            return DecryptStringReverse(System.Configuration.ConfigurationManager.AppSettings[VariableName.DBServerUserKey]);
        }

        /// <summary>
        /// �õ��������ݿ���û�������
        /// DES����
        /// </summary>
        /// <returns></returns>
        public static string GetDBServerPassword()
        {
            return DecryptStringReverse(System.Configuration.ConfigurationManager.AppSettings[VariableName.DBServerPasswordKey]);
        }

        /// <summary>
        /// �õ��������ݿ�����ݿ�
        /// DES����
        /// </summary>
        /// <returns></returns>
        public static string GetDBCataloge()
        {
            return DecryptStringReverse(System.Configuration.ConfigurationManager.AppSettings[VariableName.DBCataloglKey]);
        }
        public static string GetAppSetting(string key)
        {
            return DecryptStringReverse(System.Configuration.ConfigurationManager.AppSettings[key]);
        }
        /// <summary>
        /// �õ��������ݿ�ķ�ʽ
        /// Sql,Ole,Odbc
        /// DES����
        /// </summary>
        /// <returns></returns>
        public static string GetDBServerType()
        {
            return DecryptStringReverse(System.Configuration.ConfigurationManager.AppSettings[VariableName.DBServerTypeKey]);
        }
        /// <summary>
        /// �õ��������ݿ�����Ӵ�
        /// </summary>
        /// <returns></returns>
        public static string GetConnectionString()
        {
            string dataSource = ServerConfig.GetDBServerAddress();
            string dataCatalog = ServerConfig.GetDBCataloge();
            string userName = ServerConfig.GetDBServerUser();
            string userPwd = ServerConfig.GetDBServerPassword();

            //��ȡ���������ݿ����ӷ�ʽ
            if (string.IsNullOrWhiteSpace(HttpRuntime.Cache.Get("dataSource").ToString()))
            {
                return ServerConfig.GetConnectionString("192.168.2.230", "tjprj", "erptest", "test@123456");
            }
            else
            {
                string ConnectionString=ServerConfig.GetConnectionString(HttpRuntime.Cache.Get("dataSource").ToString(), HttpRuntime.Cache.Get("dataCatalog").ToString(), HttpRuntime.Cache.Get("userName").ToString(), HttpRuntime.Cache.Get("userPwd").ToString());
                return ConnectionString;
            }
          //  return ServerConfig.GetConnectionString(dataSource, dataCatalog, userName, userPwd);
         
        }
        /// <summary>
        /// �õ��������ݿ�����Ӵ�
        /// </summary>
        /// <param name="dbAddress">���ݿ��������ַ</param>
        /// <param name="dbCatagory">���ݿ���</param>
        /// <param name="dbUser">�û���</param>
        /// <param name="dbPwd">����</param>
        /// <returns></returns>
        public static string GetConnectionString(string dbAddress, string dbCatagory, string dbUser, string dbPwd)
        {
            return String.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}", new string[] { dbAddress, dbCatagory, dbUser, dbPwd });
        }
        /// <summary>
        /// ���Access���ݿ�����String
        /// </summary>
        /// <returns></returns>
        public static string GetAccessConnectionString()
        {
            return ServerConfig.GetAccessConnectionString(ServerConfig.GetAccessDataBasePath());
        }
        /// <summary>
        /// ���Access���ݿ�����String
        /// </summary>
        /// <param name="accessDataBasePath"></param>
        /// <returns></returns>
        public static string GetAccessConnectionString(string accessDataBasePath)
        {
            return String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}", new string[] { HttpContext.Current.Server.MapPath(accessDataBasePath) });
        }
        #endregion

        #region ��־
        /// <summary>
        /// �õ�������־��·��
        /// DES����
        /// </summary>
        /// <returns></returns>
        public static string GetLogPath()
        {
            return DecryptStringReverse(System.Configuration.ConfigurationManager.AppSettings[VariableName.ServerLogPathKey]);
        }

        /// <summary>
        /// �õ�������־�ļ���
        /// DES����
        /// </summary>
        /// <returns></returns>
        public static int GetLogLevel()
        {
            int i = 0;
            try
            {
                i = System.Convert.ToInt32(DecryptStringReverse(System.Configuration.ConfigurationManager.AppSettings[VariableName.ServerLogLevelKey]));
            }
            catch
            {
            }
            return i;
        }
        /// <summary>
        /// �õ���Ŀ����
        /// DES����
        /// </summary>
        /// <returns></returns>
        public static string GetProjectName()
        {
            return DecryptStringReverse(System.Configuration.ConfigurationManager.AppSettings[VariableName.ProjectName]);
        }
        #endregion

        #region �ļ��ϴ�
        /// <summary>
        /// �õ������ϴ�Ŀ¼
        /// DES����
        /// </summary>
        /// <returns></returns>
        public static string GetFileUploadDirectory()
        {
            return DecryptStringReverse(System.Configuration.ConfigurationManager.AppSettings[VariableName.FileUploadDirectory]);
        }
        #endregion

        #region �ʼ�
        /// <summary>
        /// �õ�SMTP�ʼ���������ַ
        /// DES����
        /// </summary>
        /// <returns></returns>
        public static string GetSMTPServer()
        {
            return DecryptStringReverse(System.Configuration.ConfigurationManager.AppSettings[VariableName.SMTPServer]);
        }
        /// <summary>
        /// ��ȡSMTP�ʼ��������˿�
        /// DES����
        /// </summary>
        public static int GetSMTPServerPort()
        {
            string port = DecryptStringReverse(System.Configuration.ConfigurationManager.AppSettings[VariableName.SMTPServerPort]);
            int rtPort = 25;
            if (String.IsNullOrEmpty(port)) return rtPort;
            int.TryParse(port, out rtPort);
            return rtPort;
        }
        /// <summary>
        /// ��ȡSMTPEnableSsl
        /// ���ʹ��GMail������Ҫ����Ϊtrue 
        /// DES����
        /// </summary>
        public static bool GetSMTPEnableSsl()
        {
            return (DecryptStringReverse(System.Configuration.ConfigurationManager.AppSettings[VariableName.SMTPEnableSsl])==Boolean.TrueString);
        }
        /// <summary>
        /// �õ�SMTP�ʼ��������û���
        /// DES����
        /// </summary>
        /// <returns></returns>
        public static string GetSMTPUserID()
        {
            return DecryptStringReverse(System.Configuration.ConfigurationManager.AppSettings[VariableName.SMTPUserID]);
        }
        /// <summary>
        /// �õ�SMTP�ʼ��������û�����
        /// DES����
        /// </summary>
        /// <returns></returns>
        public static string GetSMTPPassword()
        {
            return DecryptStringReverse(System.Configuration.ConfigurationManager.AppSettings[VariableName.SMTPPassword]);
        }
        /// <summary>
        /// �õ����󱨸�����
        /// DES����
        /// </summary>
        public static string GetErrorEmail()
        {
            return DecryptStringReverse(System.Configuration.ConfigurationManager.AppSettings[VariableName.ErrorEmail]);
        }
        /// <summary>
        /// �õ�֧������
        /// DES����
        /// </summary>
        public static string GetSupportEmail()
        {
            return DecryptStringReverse(System.Configuration.ConfigurationManager.AppSettings[VariableName.SupportEmail]);
        }
        #endregion

        /// <summary>
        /// �õ�Service��������ַ
        /// DES����
        /// </summary>
        /// <returns></returns>
        public static string GetServiceHost()
        {
            return DecryptStringReverse(System.Configuration.ConfigurationManager.AppSettings[VariableName.ServiceHost]);
        }

        #region WCF����
        /// <summary>
        /// WCFUserName
        /// </summary>
        public static string GetWCFUserName()
        {
            return DecryptStringReverse(System.Configuration.ConfigurationManager.AppSettings[VariableName.WCFUserName]);
        }
        /// <summary>
        /// WCFPassWord
        /// </summary>
        public static string GetWCFPassWord()
        {
            return DecryptStringReverse(System.Configuration.ConfigurationManager.AppSettings[VariableName.WCFPassWord]);
        }
        #endregion

        #region IGotMessage
        /// <summary>
        /// �õ�������Ƶ�ļ���ַ·��
        /// DES����
        /// </summary>
        /// <returns></returns>
        public static string GetIAdd()
        {
            return SafeConfigString(VariableName.IAdd, "127.0.0.1/doc_record");
        }
        public static string GetIUrl()
        {
            return SafeConfigString(VariableName.IUrl, "127.0.0.1/FormPagesPlug/Ajax/Video/");
        }
        
        public static string GetVideoDir()
        {
            return SafeConfigString(VariableName.VideoDir, "C:\\Program Files\\Adobe\\Flash Media Server 3.5\\applications\\doc_record\\streams\\_definst_");
        }
        public static string GetImageDir()
        {
            return SafeConfigString(VariableName.ImageDir, "C:\\Program Files\\Adobe\\Flash Media Server 3.5\\applications\\doc_record\\streams\\_definst_");
        }
        public static string GetFfmpegExecFile()
        {
            return SafeConfigString(VariableName.FfmpegExecFile, "C:\\IGotMessage\\ffmpeg.exe");
        }
        #endregion

        public static string GetVirtualDirectory()
        {
            return ConfigurationManager.AppSettings["VirtualDirectory"];
        }
      
    }
}
