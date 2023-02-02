using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProject1.Libraries
{
    public class Reporter
    {
        string msg = "";

        public void ReportStep(string status, string message)
        {
            msg = $"<html><body><br>{message} </br></body></html>";
        }

        public string getStepDetails()
        {
            return msg;
        }
    }
}
