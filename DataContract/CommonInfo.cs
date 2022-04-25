using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DataContract
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

        /// <summary>
        /// 命令参数类构造函数
        /// </summary>
        /// <param name="Name">参数名称</param>
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


    /// <summary>
    /// 系统名称枚举
    /// </summary>
    public enum SystemName
    {
        /// <summary>
        /// 正方教务系统
        /// </summary>
        ZFSystem,
        /// <summary>
        /// 一卡通中间库
        /// </summary>
        YKTMid,
        /// <summary>
        /// 锐捷数据库
        /// </summary>
        RuijieDB



    }


}
