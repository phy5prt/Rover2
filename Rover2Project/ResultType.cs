using System;
using System.Collections.Generic;
using System.Text;

namespace TitanRoverProject
{
    public struct ResultType
    {
        public bool succeeded { get; }
        public string failInformation { get; }



        public ResultType(bool succeeded, string failInformation = "")
        {
            this.succeeded = succeeded;
            if (failInformation == "") { this.failInformation = this.succeeded ? "succeeded" : "failed"; }
            this.failInformation = failInformation;
        }
         
    }
}
