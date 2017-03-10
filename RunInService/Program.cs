 
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using Topshelf;
using Topshelf.Options;

namespace RunInService
{
    class Program
    {
        static void Main(string[] args)
        {


            Console.WriteLine("Server is opened");
            HostFactory.Run(x =>                                 //1
            {
                x.Service<TimeToRun>(s =>                        //2
                {
                    s.ConstructUsing(name => new TimeToRun("timetorun","timetorun"));     //3
                    s.WhenStarted(tc => tc.Start());              //4
                    s.WhenStopped(tc => tc.Stop());               //5
                });
                x.RunAsLocalSystem();                            //6

                x.SetDescription("Task System");        //7
                x.SetDisplayName("EasyRun");                       //8
                x.SetServiceName("EasyRun");                       //9
            });
 
 
        }
       
    }
}
