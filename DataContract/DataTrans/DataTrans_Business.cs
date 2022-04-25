using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataContract.DataTrans
{
    /// <summary>
    /// 数据交换数据交换业务类
    /// </summary>
    public class DataTrans_Business : Common.BaseModelClass
    {
        /// <summary>
        /// 业务编号
        /// </summary>
        public string BUSINESSID
        {
            get;
            set;
        }

        /// <summary>
        /// 交换源数据源
        /// </summary>
        public DataTrans_DataSourceInfo SOURCEDATABASE
        {
            get;
            set;
        }

        /// <summary>
        /// 交换目标数据源
        /// </summary>
        public DataTrans_DataSourceInfo TARGETDATABASE
        {
            get;
            set;
        }

        /// <summary>
        /// 交换源表
        /// </summary>
        public string SOURCETABLE
        {
            get;
            set;
        }

        /// <summary>
        /// 交换目标表
        /// </summary>
        public string TARGETTABLE
        {
            get;
            set;
        }

        /// <summary>
        /// 交换源表
        /// </summary>
        public string SOURCEKEY
        {
            get;
            set;
        }

        /// <summary>
        /// 交换目标表
        /// </summary>
        public string TARGETKEY
        {
            get;
            set;
        }

        /// <summary>
        /// 交换业务开始运行时间
        /// </summary>
        public string RUNBEGINTIME
        {
            get;
            set;
        }

        /// <summary>
        /// 交换业务终止运行时间
        /// </summary>
        public string RUNENDTIME
        {
            get;
            set;
        }

        /// <summary>
        /// 交换业务状态
        /// </summary>
        public DataContract.Common.Codes.DataTrans_Business_StateType STATE
        {
            get;
            set;
        }

        /// <summary>
        /// 交换业务类别
        /// </summary>
        public string BUSINESSTYPE
        {
            get;
            set;
        }

        /// <summary>
        /// 交换业务说明
        /// </summary>
        public string SM
        {
            get;
            set;
        }

        /// <summary>
        /// 业务名称
        /// </summary>
        public string BUSINESSNAME
        {
            get;
            set;
        }

        /// <summary>
        /// 采集依据字段
        /// </summary>
        public string TRANSINDEXFIELD
        {
            get;
            set;
        }

        /// <summary>
        /// 采集依据字段最后值
        /// </summary>
        public string TRANSINDEXLASTVALUE
        {
            get;
            set;
        }

        /// <summary>
        /// 采集依据字段类型
        /// </summary>
        public string TRANSINDEXFIELDTYPE
        {
            get;
            set;
        }

        /// <summary>
        /// 更新频率
        /// </summary>
        public int TRANSRATE
        {
            get;
            set;
        }

        /// <summary>
        /// 每次更新规模
        /// </summary>
        public int TRANSSCALE
        {
            get;
            set;
        }

        /// <summary>
        /// 更新间隔
        /// </summary>
        public int TRANSSPLIT
        {
            get;
            set;
        }

      
 
    }
}
