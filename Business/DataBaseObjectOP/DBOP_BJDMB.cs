using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessControls.DataBaseObjectOP
{
    /// <summary> 
    /// 行政班信息数据提交操作类 
    /// </summary> 
    public class DBOP_BJDMB : DBOP_BaseClass
    {
        /// <summary> 
        /// 数据来源 
        /// </summary> 
        protected override string DataTargetTable
        {
            get
            {
                return DataSourceList.BJDMB_Source;
            }
        }
        protected override List<string> Pkey
        {
            get
            {
                return new List<string>() { "BJDM" };
            }
        }
        public DBOP_BJDMB()
        { this.GD = DataAccess.DAL_Contol.Get_GetDataObject(); }
    }
}
