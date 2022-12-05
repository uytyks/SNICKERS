using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SNICKERS.Shared.Utils
{
    public interface IOraTransMsgs
    {
        public void LoadMsgs();
        public string TranslateMsg(string strMessage);
    }
}