using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RunInWebSite
{
    public class BaseJob
    {

            public string Name { set; get; }
            public string Cron { set; get; }
            public string LogWhen { set; get; }
            public string JobType { set; get; }
            public string SearchPath { set; get; }
            public string TypeName { set; get; }
            public string SrcCodeFilePath { set; get; }
            public string Method { set; get; }
            public string Enable { set; get; }
            public List<string> Paras = new List<string>();
            public string LastDate { set; get; }
            public string NextDate { set; get; }
            public string State { set; get; }
        }
}