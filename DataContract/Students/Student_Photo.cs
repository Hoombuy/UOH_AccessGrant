using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataContract.Common;

namespace DataContract.Student
{
    /// <summary> 
    /// 学生基本信息 
    /// </summary> 
    [Serializable]
    public class Student_Photo : DataContract.Common.BaseModelClass, IImageObject
    {
        /// <summary> 
        /// 学号 
        /// </summary> 
        public string XP_XH { get; set; }

        /// <summary> 
        ///照片地址
        ///</summary> 
        public byte[] XP_ZP { get; set; }

        /// <summary> 
        ///照片地址
        ///</summary> 
        public string XP_URL { get; set; }

        public string IImageUrl
        {
            get
            {
                return this.XP_URL;
            }

            set
            {
                this.XP_URL = value;
            }
        }

        public byte[] IImageByte
        {
            get
            {
                return this.XP_ZP;
            }

            set
            {
                this.XP_ZP = value;
            }
        }

        public override string ToString()
        {
            if (this.XP_URL != null)
            {
                return this.XP_URL.ToString();
            }
            return "";
        }
    }

}
