using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emp_task
{
    public class emp
    {
        private string comp = "TraceArt";
        public string Comp
        {
            get { return comp; }
        }

        public long Emp_phone { get; set;}
        public string Emp_mail { get; set; }
        public string Emp_location { get; set;}
        public string Emp_Department { get; set;}
    }
}
