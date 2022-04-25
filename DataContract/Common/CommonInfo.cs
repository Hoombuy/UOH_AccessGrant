using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DataContract.Common
{

    //<summary>
    //命令参数类（主要用于存储过程的调用）
    //</summary>
    [DataContract(Namespace = "http://UOH_Dats_WcfService.org/")]
    public class DataParamester
    {
        /// <summary>
        /// 参数字段名
        /// </summary>
        [DataMember]
        public string PName
        {
            get;
            set;
        }

        /// <summary>
        /// 参数字段类型
        /// </summary>
        [DataMember]
        public string PType
        {
            get;
            set;
        }

        /// <summary>
        /// 参数值
        /// </summary>
        [DataMember]
        public string PValue
        {
            get;
            set;
        }

        [DataMember]
        public string Operators
        {
            get;
            set;
        }


        //[DataMember]
        //public string Connector
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// 命令参数类构造函数
        /// </summary>
        /// <param name="NAME">参数名称</param>
        /// <param name="Value">参数值</param>
        public DataParamester(string Name, string Value)
        {
            this.PName = Name;
            this.PValue = Value;
        }
        public DataParamester()
        {

        }
    }


   


}
