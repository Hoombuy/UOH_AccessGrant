using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessControls.DataBaseObjectOP
{
    /// <summary> 
    /// ѧ��������Ϣ�����ύ������ 
    /// </summary> 
    public class DBOP_XSJBXXB_ZF : DBOP_BaseClass
    {
        /// <summary> 
        /// ������Դ 
        /// </summary> 
        protected override string DataTargetTable
        {
            get
            {
                return DataSourceList.XSJBXXB_ZF_Source;
            }
        }
        /// <summary> 
        /// ���ݿ������
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
