using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataContract.Common.ThreadingInfo
{
    public delegate void UpdateLogHandle(DataContract.DataTrans.DataTrans_Log TheLog);
    public delegate void UpdateRunningStateHandle( DataContract.DataTrans.DataTrans_Log TheLog);
    public delegate void InitializeRunningStateListHandle( );

     
    public class TransBusiness_ThreadingInfo
    {
        /// <summary>
        /// 界面更新句柄
        /// </summary>
        public UpdateLogHandle Update_Log
        {
            get;
            set;
        }

        /// <summary>
        /// 界面更新句柄
        /// </summary>
        public UpdateRunningStateHandle Update_RunningState
        {
            get;
            set;
        }

        public DataContract.DataTrans.DataTrans_Business TheTrBusiness
        {
            get;
            set;
        }
    }
    public class TypeAndState
    {
        public string Type
        {
            get;
            set;
        }
        public string State
        {
            get;
            set;
        }

        public TypeAndState(string arg_Type, string arg_State)
        {
            this.Type = arg_Type;
            this.State = arg_State;
        }
    }

    public class UpdateTypeAndTime
    {
        public string Type
        {
            get;
            set;
        }
        public string Time
        {
            get;
            set;
        }

        public UpdateTypeAndTime(string arg_Type, string arg_Time)
        {
            this.Type = arg_Type;
            this.Time = arg_Time;
        }
    }

    public class InfoAndColor
    {
        public string Type
        {
            get;
            set;
        }
        public string Info
        {
            get;
            set;
        }
        public string Color
        {
            get;
            set;
        }
        public InfoAndColor(string arg_Type, string arg_Info, string arg_Color)
        {
            this.Type = arg_Type;
            this.Info = arg_Info;
            this.Color = arg_Color;
        }
    }

}
