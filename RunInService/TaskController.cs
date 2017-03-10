using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace RunInService
{
    public class TaskController : ApiController
    {
        [HttpGet]
        public Task_t[] All()
        {
            return JobHelper.Tasks.ToArray();

        }
        [HttpGet]
        public bool refresh() {
            try
            {
                JobHelper.Sche();
                return true;
            }
            catch {
                return false;
            }
        }
    }
}
