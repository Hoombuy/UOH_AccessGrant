using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataContract.DataTrans
{
    /// <summary>
    /// 数据交换数据源信息类
    /// </summary>
    public class DataTrans_Log : Common.BaseModelClass
    {
        /// <summary>
        /// 业务名称
        /// </summary>
        public string BUSINESSNAME
        {
            get;
            set;
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string BUSINESSMS
        {
            get;
            set;
        }


        /// <summary>
        /// 业务开始时间
        /// </summary>
        public DateTime TRANSBEGINTIME
        {
            get;
            set;
        }

        /// <summary>
        /// 业务结束时间
        /// </summary>
        public DateTime TRANSENDTIME
        {
            get;
            set;
        }

        /// <summary>
        /// 业务处理量
        /// </summary>
        public int TRANSCOUNT
        {
            get;
            set;
        }

        /// <summary>
        /// 业务运行状态
        /// </summary>
        public Common.Codes.DataTrans_Log_StateType STATE
        {
            get;
            set;
        }

        /// <summary>
        /// 业务处理的末尾ID
        /// </summary>
        public string LASTID
        {
            get;
            set;
        }

        /// <summary>
        /// 业务处理的开始ID
        /// </summary>
        public string BEGINID
        {
            get;
            set;
        }

        /// <summary>
        /// 用于前台显示的信息
        /// </summary>
        public string TitleInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 业务名称
        /// </summary>
        public string _STATEMC
        {
            get
            {
                return this.STATE.MC;
            }
        }

    }
}
