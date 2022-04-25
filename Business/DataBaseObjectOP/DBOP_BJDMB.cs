using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessControls.DataBaseObjectOP
{
    /// <summary> 
    /// ��������Ϣ�����ύ������ 
    /// </summary> 
    public class DBOP_BJDMB : DBOP_BaseClass
    {
        /// <summary> 
        /// ������Դ 
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
