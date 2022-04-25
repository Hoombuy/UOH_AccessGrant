using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessControls.DataBaseObjectOP
{
    public class DBOP_DataTrans_Log : DBOP_BaseClass
    {
        /// <summary>
        /// 数据来源
        /// </summary>
        protected override string DataTargetTable
        {
            get
            {
                return DataSourceList.DataTrans_LogTarget;
            }
        }

        protected override List<string> NoInsertField
        {
            get
            {
                return new List<string>() { " " };
            }
        }

        protected override List<string> Pkey
        {
            get
            {
                return new List<string>() { "BUSINESSNAME", "TRANSBEGINTIME" };
            }
        }

        public DBOP_DataTrans_Log()
        { this.GD = DataAccess.DAL_Contol.Get_GetDataObject(); }


    }
}
