/*
 * 使用动态编译,除了注意引用dll的书写方法外代码和VS中的写法一致
 * "//#import System.dll":代表引用了系统动态库System.dll
 * "//#import E:\WebSite\bin\HanZi2PinYin.dll":代表引用了绝对路径下的dll
 * "//#import ~\bin\Demo.dll":代表引用了相对路径下的dll
 */

//#import System.dll
//#import System.Core.dll
//#import Microsoft.CSharp.dll
//#import System.Data.dll
using System;
using System.IO;

namespace Demo2
{
    class Program
    {
        static void Main(string[] args)
        {
            string para = "";
            if (args != null && args.Length > 0)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    para += args[i];
                }
            }
            File.AppendAllText("d:\\demo2.cs.txt", "Demo2.cs" + DateTime.Now.ToString
                ("yyyy-MM-dd HH:mm:ss.fff") + para + "\r\n");
        }
    }
}