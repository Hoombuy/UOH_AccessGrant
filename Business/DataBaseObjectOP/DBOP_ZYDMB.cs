using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessControls.DataBaseObjectOP
{
    /// <summary> 
    /// רҵ��Ϣ�����ύ������ 
    /// </summary> 
    public class DBOP_ZYDMB : DBOP_BaseClass
    {
        /// <summary> 
        /// ������Դ 
        /// </summary> 
        protected override string DataTargetTable
        {
            get
            {
                return DataSourceList.ZYDMB_Source;
            }
        }
        protected override List<string> Pkey
        {
            get
            {
                return new List<string>() { "ZYDM" };
            }
        }
        public DBOP_ZYDMB()
        { this.GD = DataAccess.DAL_Contol.Get_GetDataObject(); }
    }
    }
