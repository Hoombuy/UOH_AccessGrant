using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessControls.DataBaseObjectOP
{
    public class DBOP_DataTrans_DataSourceInfo: DBOP_BaseClass
    {
        /// <summary>
        /// 数据来源
        /// </summary>
        protected override string DataTargetTable
        {
            get
            {
                return DataSourceList.DataTrans_DataSourceInfoTarget;
            }
        }

        protected override List<string> NoInsertField
        {
            get
            {
                return new List<string>() {  };
            }
        }

        protected override List<string> Pkey
        {
            get
            {
                return new List<string>() { "DATASOURCEID" };
            }
        }

        public DBOP_DataTrans_DataSourceInfo()
        { this.GD = DataAccess.DAL_Contol.Get_GetDataObject(); }


    }
}
