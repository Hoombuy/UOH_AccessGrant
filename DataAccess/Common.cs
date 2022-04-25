using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;
using MySql.Data.MySqlClient;
using Npgsql;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess
{
    /// <summary>
    /// 数据连接信息类
    /// </summary>
    public class DataConnectionInfo
    {
        /// <summary>
        /// 数据连接对象
        /// </summary>
        public DbConnection myConn
        {
            get;
            set;
        }

        /// <summary>
        /// 数据库类别（SQL、Oracle）
        /// </summary>
        public string DataBaseType
        {
            get;
            set;
        }

        /// <summary>
        /// 数据连接信息类构造函数
        /// </summary>
        /// <param name="ConnectString">数据连接字符串</param>
        /// <param name="TYPE">数据库类别（SQL、Oracle）</param>
        public DataConnectionInfo(String ConnectString, String Type)
        {
            this.DataBaseType = Type;
            if (Type == "SQL")
            {
                myConn = new SqlConnection(ConnectString);
                return;
            }
            if (Type == "Oracle")
            {
                myConn = new OracleConnection(ConnectString);
                return;
            }

            if (Type == "MySql")
            {
                myConn = new MySqlConnection(ConnectString);
                return;
            }

            if (Type == "PostgreSQL")
            {
                myConn = new NpgsqlConnection(ConnectString);
                return;
            }
        }
    }








}