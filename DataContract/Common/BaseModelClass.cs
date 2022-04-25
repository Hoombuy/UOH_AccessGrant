using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;


namespace DataContract.Common
{
    /// <summary>
    /// 数据模型基类
    /// </summary>
    [DataContract]
    [Serializable]
    public abstract class BaseModelClass : ICloneable
    {
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
