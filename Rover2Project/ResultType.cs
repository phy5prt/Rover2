using System;
using System.Collections.Generic;
using System.Text;

namespace TitanRoverProject
{
    public struct ResultType
    {
        public bool succeeded;
        public string failInformation;



        public ResultType(bool succeeded, string failInformation = "")
        {
            this.succeeded = succeeded;
            if (failInformation == "") { this.failInformation = this.succeeded ? "succeeded" : "failed"; }
            this.failInformation = failInformation;
        }
         
    }
}
