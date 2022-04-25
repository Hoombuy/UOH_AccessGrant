using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataContract.DataTrans
{
    /// <summary>
    /// 数据交换数据源信息类
    /// </summary>
    public class DataTrans_DataSourceInfo : Common.BaseModelClass
    {
        /// <summary>
        /// 数据源编号
        /// </summary>
        public string DATASOURCEID
        {
            get;
            set;
        }

        /// <summary>
        /// 数据源说明
        /// </summary>
        public string DATASOURCESM
        {
            get;
            set;
        }

        /// <summary>
        /// 数据源连接字符串
        /// </summary>
        public string DATASOURCECONNECTIONSTRING
        {
            get;
            set;
        }

        /// <summary>
        /// 数据源数据库类型
        /// </summary>
        public Common.Codes.DataTrans_DataBaseType DATASOURCETYPE
        {
            get;
            set;
        }

        /// <summary>
        /// 数据源名称
        /// </summary>
        public string DATASOURCENAME
        {
            get;
            set;
        }

     
 

        public override string ToString()
        {
            return this.DATASOURCEID;
        }
    }
}
