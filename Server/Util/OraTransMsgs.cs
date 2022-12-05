using Microsoft.EntityFrameworkCore;
using SNICKERS.EF;
using SNICKERS.EF.Models;
using SNICKERS.EF.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SNICKERS.Shared.Utils
{
    public class OraTransMsgs : IOraTransMsgs
    {
        private DbContextOptions<SNICKERSOracleContext> _dbContextOptions;
        public List<OraTranslateMsg> lstOraTranslateMsgs { get; set; }

        public OraTransMsgs(DbContextOptions<SNICKERSOracleContext> dbContextOptions)
        {
            this._dbContextOptions = dbContextOptions;
            LoadMsgs();
        }

        public void LoadMsgs()
        {
            using (var db = new SNICKERSOracleContext(_dbContextOptions))
            {
                lstOraTranslateMsgs = db.OraTranslateMsgs.ToList();
            }
        }

        public string TranslateMsg(string strMessage)
        {

            foreach (var msg in lstOraTranslateMsgs)
            {
                if (strMessage.ToUpper().Contains(msg.OraConstraintName.ToUpper()))
                {
                    return msg.OraErrorMessage;
                }
            }
            return strMessage;

        }
    }
}
