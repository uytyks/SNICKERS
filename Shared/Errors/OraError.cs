using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNICKERS.Shared.Errors
{
    public class OraError
    {
        public OraError()
        {

        }
        public OraError(int oraErrorNumber, string oraErrorMsg)
        {
            this.OraErrorMsg = oraErrorMsg;
            this.OraErrorNumber = oraErrorNumber;
        }
        public int OraErrorNumber { get; set; }
        public string OraErrorMsg { get; set; }
        public bool bOraTranslated { get; set; }
    }
}
