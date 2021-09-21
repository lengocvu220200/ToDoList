using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ToDoList.Models
{
    public class DBConnection
    {
        string strCon;

        public DBConnection()
        {
            strCon = ConfigurationManager.ConnectionStrings["todolistEntities"].ConnectionString;

        }

        public SqlConnection getConnection()
        {
            return new SqlConnection(strCon);
        }
    }
}