using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessControls.Common
{
    public class Control_TotleQuery : Control_BaseClass
    {
        public Control_TotleQuery()
        { this.GD = DataAccess.DAL_Contol.Get_GetDataObject(); }

        public TotleTJJG TotleTJ()
        {
            TotleTJJG Result = new TotleTJJG();
            //DataTable Table = this.GD.GetDataTable(" select (select count(*) from tp_baseinfo ) as 人员总数, (select count(*) from view_tp_baseinfo x where x.zt = 1) as 在逃人员数, (select count(*) from view_tp_baseinfo x where x.WFFZ = 1) as 违法犯罪人员数 , (select count(*) from view_tp_baseinfo x where x.ZDGZ = 1) as 重点关注人员, (select count(*) from view_tp_baseinfo x where x.WW = 1) as 维稳关注人员 , (select count(*) from view_tp_baseinfo x where x.SDRY = 1) as 涉毒人员 , (select count(*) from view_tp_baseinfo x where x.ZSJSBR = 1) as 肇事肇祸精神病人, (select count(*) from view_tp_baseinfo x where x.HC = 1) as 黑车司机, (select count(*) from view_tp_baseinfo x where x.WW = 0 and x.State =0 and x.WFFZ=0 and x.SDRY=0) as 其他人员,              (select count(*) from view_tp_baseinfo x where x.WW = 0 and x.State =0 and x.WFFZ=0 and x.SDRY=0) as 物品痕迹,              (select count(*) from vehicle_info x   ) as 车辆,              (select count(*) from internetbar_record x  ) as 网吧记录,              (select count(*) from hotal_record x  ) as 住宿记录,              (select count(*) from driving_license  x  ) as 驾驶执照数据,                  0 as 案事件,                  0 as 刑事案件,                  0 as 离散情报               from dual");

            DataTable Table = this.GD.GetDataTable(" select  * from totaldatastatistics ");
            for (int i = 0; i < Table.Rows.Count; i++)
            {
                if (Table.Rows[i]["ITEMMC"].ToString() == "人员总数")
                {
                    Result.ZS = int.Parse(Table.Rows[i]["ITEMValue"].ToString());
                }
                if (Table.Rows[i]["ITEMMC"].ToString() == "在逃人员数")
                {
                    Result.ZT = int.Parse(Table.Rows[i]["ITEMValue"].ToString());
                }
                if (Table.Rows[i]["ITEMMC"].ToString() == "违法犯罪人员数")
                {
                    Result.WFFZ = int.Parse(Table.Rows[i]["ITEMValue"].ToString());
                }
                if (Table.Rows[i]["ITEMMC"].ToString() == "重点关注人员")
                {
                    Result.ZDGZ = int.Parse(Table.Rows[i]["ITEMValue"].ToString());
                }
                if (Table.Rows[i]["ITEMMC"].ToString() == "维稳关注人员")
                {
                    Result.WW = int.Parse(Table.Rows[i]["ITEMValue"].ToString());
                }
                if (Table.Rows[i]["ITEMMC"].ToString() == "涉毒人员")
                {
                    Result.SDRY = int.Parse(Table.Rows[i]["ITEMValue"].ToString());
                }
                if (Table.Rows[i]["ITEMMC"].ToString() == "肇祸肇事精神病人")
                {
                    Result.ZSJSBR = int.Parse(Table.Rows[i]["ITEMValue"].ToString());
                }
                if (Table.Rows[i]["ITEMMC"].ToString() == "黑车司机")
                {
                    Result.HC = int.Parse(Table.Rows[i]["ITEMValue"].ToString());
                }
                if (Table.Rows[i]["ITEMMC"].ToString() == "其他人员")
                {
                    Result.QT = int.Parse(Table.Rows[i]["ITEMValue"].ToString());
                }
                if (Table.Rows[i]["ITEMMC"].ToString() == "物品痕迹")
                {
                    Result.WPHJ = int.Parse(Table.Rows[i]["ITEMValue"].ToString());
                }
                if (Table.Rows[i]["ITEMMC"].ToString() == "车辆")
                {
                    Result.CL = int.Parse(Table.Rows[i]["ITEMValue"].ToString());
                }
                if (Table.Rows[i]["ITEMMC"].ToString() == "网吧记录")
                {
                    Result.WBJL = int.Parse(Table.Rows[i]["ITEMValue"].ToString());
                }
                if (Table.Rows[i]["ITEMMC"].ToString() == "住宿记录")
                {
                    Result.ZSJL = int.Parse(Table.Rows[i]["ITEMValue"].ToString());
                }
                if (Table.Rows[i]["ITEMMC"].ToString() == "驾驶执照数据")
                {
                    Result.JZ = int.Parse(Table.Rows[i]["ITEMValue"].ToString());
                }
                if (Table.Rows[i]["ITEMMC"].ToString() == "案事件")
                {
                    Result.ASJ = int.Parse(Table.Rows[i]["ITEMValue"].ToString());
                }
                if (Table.Rows[i]["ITEMMC"].ToString() == "刑事案件")
                {
                    Result.XSAJ = int.Parse(Table.Rows[i]["ITEMValue"].ToString());
                }
                if (Table.Rows[i]["ITEMMC"].ToString() == "离散情报")
                {
                    Result.LSQB = int.Parse(Table.Rows[i]["ITEMValue"].ToString());
                }

            }
            //for (int i = 0; i < Table.Columns.Count; i++)
            //{
            //    Result.Add(new TotleTJJG() { ItemMC = Table.Columns[i].ColumnName.ToString(), ItemCount = (int)Table.Rows[0][i] });
            //}

            return Result;
        }
    }

    public class TotleTJJG
    {
        public int ZS
        {
            get;
            set;
        }
        public int HC
        {
            get;
            set;
        }
        public int ZT
        {
            get;
            set;
        }
        public int WW
        {
            get;
            set;
        }
        public int WFFZ
        {
            get;
            set;
        }
        public int SDRY
        {
            get;
            set;
        }
        public int ZDGZ
        {
            get;
            set;
        }
        public int ZSJSBR
        {
            get;
            set;
        }
        public int QT
        {
            get;
            set;
        }

        public int WPHJ
        {
            get;
            set;
        }
        public int CL
        {
            get;
            set;
        }
        public int WBJL
        {
            get;
            set;
        }
        public int ZSJL
        {
            get;
            set;
        }
        public int JZ
        {
            get;
            set;
        }


        public int ASJ
        {
            get;
            set;
        }

        public int XSAJ
        {
            get;
            set;
        }
        public int LSQB
        {
            get;
            set;
        }

    }
}
