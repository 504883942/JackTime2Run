using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace RunInService
{
    public class TownCrier
    {
        readonly Timer _timer;
        public TownCrier()
        {
            _timer = new Timer(1000) { AutoReset = true };
            _timer.Elapsed += (sender, eventArgs) => {
                var s = string.Format("It is {0} and all is well", DateTime.Now);
                Console.WriteLine(s);
                WriteLog(s);
                
                };
        }
        public void Start() { _timer.Start(); }
        public void Stop() { _timer.Stop(); }
        public void WriteLog(string msg)
        {
            string path = "D:\\2-Project\\1-智慧水务\\JackTime2Run\\RunInService\\bin\\Debug\\log";

            lock (typeof(TownCrier))
            {
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                path += "\\TaskLog" + DateTime.Now.ToString("yyyyMMdd") + ".log";
                System.IO.File.AppendAllText(path, "【日志信息: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "】" + msg + "\r\n");
            }
        }
    }
}
