using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessControls.DataBaseObjectOP
{
    /// <summary> 
    /// 学生基本信息数据提交操作类 
    /// </summary> 
    public class DBOP_XSJBXXB_ZF : DBOP_BaseClass
    {
        /// <summary> 
        /// 数据来源 
        /// </summary> 
        protected override string DataTargetTable
        {
            get
            {
                return DataSourceList.XSJBXXB_ZF_Source;
            }
        }
        /// <summary> 
        /// 数据库表主键
        /// </summary> 
        protected override List<string> Pkey
        {
            get
            {
                return new List<string>() { "XH" };
            }
        }
        public DBOP_XSJBXXB_ZF()
        { this.GD = DataAccess.DAL_Contol.Get_GetDataObject(); }

    }
}
