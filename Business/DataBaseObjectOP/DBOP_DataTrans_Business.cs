using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessControls.DataBaseObjectOP
{
    public class DBOP_DataTrans_Business : DBOP_BaseClass
    {
        /// <summary>
        /// 数据来源
        /// </summary>
        protected override string DataTargetTable
        {
            get
            {
                return DataSourceList.DataTrans_BusinessTarget;
            }
        }

        protected override List<string> NoInsertField
        {
            get
            {
                return new List<string>() { };
            }
        }

        protected override List<string> Pkey
        {
            get
            {
                return new List<string>() { "BUSINESSID" };
            }
        }

        public DBOP_DataTrans_Business()
        { this.GD = DataAccess.DAL_Contol.Get_GetDataObject(); }

        public bool updateBussinessInfo( DataContract.DataTrans.DataTrans_Business TheB)
        {
            return this.GD.Excute("update DATATRANS_BUSINESS set TRANSINDEXLASTVALUE='" + TheB.TRANSINDEXLASTVALUE + "' where BUSINESSID='" + TheB.BUSINESSID + "'");
        }
        

    }
}
