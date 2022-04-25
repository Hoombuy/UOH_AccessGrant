using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessControls
{
    public static class DataSourceList
    {
        /// <summary>
        /// 用户表信息来源
        /// </summary>
        public static string Users_Source
        {
            get
            {
                return "UOH_AS.View_Users";
            }
        }

        /// <summary>
        /// 用户表信息目标
        /// </summary>
        public static string Users_TargetTable
        {
            get
            {
                return "UOH_AS.Users";
            }
        }

        /// <summary>
        /// 用户表信息来源
        /// </summary>
        public static string Module_Source
        {
            get
            {
                return "UOH_AS.Module";
            }
        }

        /// <summary>
        /// 性别信息来源
        /// </summary>
        public static string Code_XB_Source
        {
            get
            {
                return "UOH_AS.dm_xb";
            }
        }

        #region 学生(人员)数据相关来源

        /// <summary>
        /// 正方学生数据信息数据来源
        /// </summary>
        public static string XSJBXXB_ZF_Source
        {
            get
            {
                return "ZFXFZB.uoh_cd_xsjbxxb";
            }
        }

        /// <summary>
        /// 监控系统人员数据信息数据来源
        /// </summary>
        public static string XSJBXXB_CDB_BIMS_Source
        {
            get
            {
                return " person_base_info  ";
            }
        }

        /// <summary>
        /// 监控系统人员数据信息数据来源
        /// </summary>
        public static string XSJBXXB_CDB_DCMS_Source
        {
            get
            {
                return " edu_person ";
            }
        }

        /// <summary>
        /// 监控系统人员数据信息数据来源
        /// </summary>
        public static string XSJBXXB_CDB_ICMS_Source
        {
            get
            {
                return " person_info ";
            }
        }

        /// <summary>
        /// 监数据交换数据库中人员基本数据信息数据来源（来自BIMS）
        /// </summary>
        public static string XSJBXXB_DCDB_BIMS_Source
        {
            get
            {
                return " BIMS_PERSON_BASE_INFO  ";
            }
        }

        /// <summary>
        /// 监数据交换数据库中人员基本数据信息数据来源（来自DCMS）
        /// </summary>
        public static string XSJBXXB_DCDB_DCMS_Source
        {
            get
            {
                return " DCMS_EDU_PERSON ";
            }
        }

        /// <summary>
        /// 监数据交换数据库中人员基本数据信息数据来源（来自ICMS）
        /// </summary>
        public static string XSJBXXB_DCDB_ICMS_Source
        {
            get
            {
                return " ICMS_PERSON_INFO ";
            }
        }

        /// <summary>
        /// 监数据交换数据库中人员基本数据信息数据来源（来自正方）
        /// </summary>
        public static string XSJBXXB_DCDB_ZF_Source
        {
            get
            {
                return " PERSON_INFO_ZF ";
            }
        }

        #endregion


        /// <summary>
        /// 正方学生住宿数据信息数据来源
        /// </summary>
        public static string XSZS_ZF_Source
        {
            get
            {
                return "xgxtdemon.uoh_view_xszsgl ";
            }
        }

        /// <summary>
        /// 监控系统人员数据信息数据来源
        /// </summary>
        public static string XSZS_CDB_DCMS_Source
        {
            get
            {
                return " edu_dorm  ";
            }
        }

        /// <summary>
        /// 数据交换数据库中人员住宿数据信息数据来源（来自正方）
        /// </summary>
        public static string XSZS_DCDB_ZF_Source
        {
            get
            {
                return "DORM_INFO_ZF";
            }
        }

        /// <summary>
        /// 数据交换数据库中人员住宿数据信息数据来源（来自DCMS）
        /// </summary>
        public static string XSZS_DCDB_DCMS_Source
        {
            get
            {
                return "DCMS_EDU_Dorm";
            }
        }

        /// <summary>
        /// 专业数据信息来源
        /// </summary>
        public static string ZYDMB_Source
        {
            get
            {
                return "ZFXFZB.UOH_AS_VIEW_ZYDMB";
            }
        }

        /// <summary>
        /// 学院数据信息来源
        /// </summary>
        public static string XYDMB_Source
        {
            get
            {
                return "ZFXFZB.UOH_AS_VIEW_XYDMB";
            }
        }

        /// <summary>
        /// 班级数据信息来源
        /// </summary>
        public static string BJDMB_Source
        {
            get
            {
                return "ZFXFZB.UOH_AS_VIEW_BJDMB";
            }
        }

        /// <summary>
        /// 正方教师数据信息数据来源
        /// </summary>
        public static string Teacher_Source
        {
            get
            {
                return "ZFXFZB.JSXXB";
            }
        }



        /// <summary>
        /// 退学数据信息来源
        /// </summary>
        public static string Student_Photo_Source
        {
            get
            {
                return "UOH_AS.XS_Photo";
            }
        }

        /// <summary>
        /// 退学数据信息更新目标
        /// </summary>
        public static string Student_Photo_TargetTable
        {
            get
            {
                return "UOH_AS.XS_Photo";
            }
        }


        /// <summary>
        /// 操作记录数据源
        /// </summary>
        public static string ServiceRequestLog_Source
        {
            get
            {
                return "SERVICEREQUESTLOG ";
            }
        }

        /// <summary>
        /// 操作记录数据源
        /// </summary>
        public static string ServiceRequestLog_TargetTable
        {
            get
            {
                return "SERVICEREQUESTLOG ";
            }
        }

        /// <summary>
        /// 操作记录数据源
        /// </summary>
        public static string DataSynchronism_Record_Source
        {
            get
            {
                return "View_DataSynchronism_Record ";
            }
        }

        /// <summary>
        /// 操作记录数据源
        /// </summary>
        public static string DataSynchronism_Record_TargetTable
        {
            get
            {
                return "DataSynchronism_Record ";
            }
        }


        /// <summary>
        /// 数据交换业务数据源
        /// </summary>
        public static string DataTrans_BusinessSource
        {
            get
            {
                return "View_DataTrans_Business";
            }
        }

        /// <summary>
        /// 数据交换业务数据目标表
        /// </summary>
        public static string DataTrans_BusinessTarget
        {
            get
            {
                return "DataTrans_Business";
            }
        }

        /// <summary>
        /// 数据源信息数据源
        /// </summary>
        public static string DataTrans_DataSourceInfoSource
        {
            get
            {
                return "View_DataTrans_DataSourceInfo";
            }
        }

        /// <summary>
        /// 数据源信息数据目标表
        /// </summary>
        public static string DataTrans_DataSourceInfoTarget
        {
            get
            {
                return "DataTrans_DataSourceInfo";
            }
        }

        /// <summary>
        /// 数据交换日志数据源
        /// </summary>
        public static string DataTrans_LogSource
        {
            get
            {
                return "View_DataTrans_Log";
            }
        }

        /// <summary>
        /// 数据交换日志数据目标表
        /// </summary>
        public static string DataTrans_LogTarget
        {
            get
            {
                return "DataTrans_Log";
            }
        }

    }
}
