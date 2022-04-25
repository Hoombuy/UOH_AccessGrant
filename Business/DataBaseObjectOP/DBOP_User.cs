using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessControls.DataBaseObjectOP
{

    public class DBOP_User : DBOP_BaseClass
    {
        /// <summary>
        /// 数据来源
        /// </summary>
        protected override string DataTargetTable
        {
            get
            {
                return DataSourceList.Users_TargetTable;
            }
        }

        protected override List<string> Pkey
        {
            get
            {
                return new List<string>() { "U_USERNAME" };
            }
        }

        public DBOP_User()
        { this.GD = DataAccess.DAL_Contol.Get_GetDataObject(); }
    }
}
