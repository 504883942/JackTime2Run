using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace RunInService
{
    public class TimeToRun
    {
        private static Hashtable ht = new Hashtable();
        HttpSelfHostConfiguration config;
        HttpSelfHostServer server;
        string srvName;
        string srvDesc;
        public TimeToRun(string srvName, string srvDesc)
        {
            config = new HttpSelfHostConfiguration("http://localhost:3333");
            config.Routes.MapHttpRoute("default", "api/{controller}/{action}/{id}", new { id = RouteParameter.Optional });
            server = new HttpSelfHostServer(config);
            server.OpenAsync().Wait();
            this.srvName = srvName;
            this.srvDesc = srvDesc;

        }
        public void Start()
        {
            try
            {
                //服务启动
                WriteLog("服务:【" + srvName + "】将要启动了...");
                LoadJob();
                WriteLog("服务:【" + srvName + "】启动成功!");
            }
            catch (Exception ex)
            {
                WriteLog("服务:【" + srvName + "】启动失败:" + ex.ToString());
                throw ex;
            }
        }

        public void Stop()
        {
            //服务停止
            WriteLog("服务:【" + srvName + "】停止了!");
        }

        public void Shutdown()
        {
            //服务关闭
            WriteLog("服务:【" + srvName + "】关闭了!");
        }

        public void Continue()
        {
            JobHelper.Sche();
            //服务继续
            WriteLog("服务:【" + srvName + "】继续了!");
        }

        public void Pause()
        {
            //服务暂停
            WriteLog("服务:【" + srvName + "】暂停了!");
        }
        private void LoadJob()
        {
            JobHelper.Sche();
        }

        public static void WriteLog(string msg)
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory.Trim('\\').Trim('/') + "\\log";

            lock (typeof(TimeToRun))
            {
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                path += "\\SrvManage" + DateTime.Now.ToString("yyyyMMdd") + ".log";
                System.IO.File.AppendAllText(path, "【日志信息: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "】" + msg + "\r\n");
            }
        }
    }
}
