using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataContract.Common.Codes
{
   

    /// <summary>
    /// 性别
    /// </summary>
    [Serializable]
    public class Code_XB : Common.Codes.BaseCode
    {
        public override string Code_TableName
        {
            get { return "ZFXFZB.UOH_AS_VIEW_XBDMB"; }
        }
    }

    /// <summary>
    /// 学院对象
    /// </summary>
    [Serializable]
    public class Code_XY : BaseCode
    {
        public override string Code_TableName
        {
            get
            {
                return "ZFXFZB.UOH_AS_VIEW_XYDMB";
            }
        }
    }

    /// <summary>
    /// 专业对象
    /// </summary>
    [Serializable]
    public class Code_ZY : BaseCode
    {
        public override string Code_TableName
        {
            get
            {
                return "ZFXFZB.UOH_AS_VIEW_ZYDMB";
            }
        }

        /// <summary>
        /// 所属学院代码
        /// </summary>
        public Code_XY XYDM
        {
            get;
            set;
        }

        /// <summary>
        /// 所属学院名称
        /// </summary>
        public string _XYMC
        {
            get
            {
                return XYDM.MC;
            }
        }

    }

    /// <summary>
    /// 专业对象
    /// </summary>
    [Serializable]
    public class Code_BJ : BaseCode
    {
        public override string Code_TableName
        {
            get
            {
                return "ZFXFZB.UOH_AS_VIEW_BJDMB";
            }
        }

        public class Code_BM : BaseCode
        {
            public override string Code_TableName
            {
                get
                {
                    return "UOH_AS.VIEW_XYDMB";
                }
            }
        }
        /// <summary>
        /// 所属学院代码
        /// </summary>
        public Code_XY XYDM
        {
            get;
            set;
        }

        /// <summary>
        /// 所属学院名称
        /// </summary>
        public string _XYMC
        {
            get
            {
                return XYDM.MC;
            }
        }

        /// <summary>
        /// 所属专业代码
        /// </summary>
        public Code_ZY ZYDM
        {
            get;
            set;
        }

        /// <summary>
        /// 所属专业名称
        /// </summary>
        public string _ZYMC
        {
            get
            {
                return ZYDM.MC;
            }
        }

        /// <summary>
        /// 所属年级
        /// </summary>
        public string NJ
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 数据交换_数据库类型代码
    /// </summary>
    [Serializable]
    public class DataTrans_DataBaseType : Common.Codes.BaseCode
    {
        public override string Code_TableName
        {
            get { return "DATATRANS_DATABASETYPE"; }
        }
    }

    /// <summary>
    /// 数据交换_日志状态类型代码
    /// </summary>
    [Serializable]
    public class DataTrans_Log_StateType : Common.Codes.BaseCode
    {
        public override string Code_TableName
        {
            get { return "DATATRANS_LOG_STATETYPE"; }
        }
    }

    /// <summary>
    /// 数据交换_业务状态类别代码
    /// </summary>
    [Serializable]
    public class DataTrans_Business_StateType : Common.Codes.BaseCode
    {
        public override string Code_TableName
        {
            get { return "DATATRANS_BUSINESS_STATETYPE"; }
        }
    }

}