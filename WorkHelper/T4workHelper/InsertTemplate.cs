using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Data;
using CMS.Utilities;
using System.Data.OleDb;
using System.Configuration;
using System.Data.SqlClient;
using System;
namespace MyProject.Entitiese
{

    public class Team_Bizs
    {
        public class Team_Models
        {

            private String _team_code;
            public String Team_code
            {
                get { return _team_code; }
                set { _team_code = value; }
            }

            private String _team_name;
            public String Team_name
            {
                get { return _team_name; }
                set { _team_name = value; }
            }

            private String _team_status;
            public String Team_status
            {
                get { return _team_status; }
                set { _team_status = value; }
            }

            private String _team_user;
            public String Team_user
            {
                get { return _team_user; }
                set { _team_user = value; }
            }

            private DateTime _team_date;
            public DateTime Team_date
            {
                get { return _team_date; }
                set { _team_date = value; }
            }


        }
        public bool InsertTeam_(Team_Models model)
        {
            string strSql = @"
                   INSERT Team_(
		    Team_code,
              Team_name,
              Team_status,
              Team_user,
              Team_date,
                 
			         )
			     VALUES (
			      Team_code,
               Team_name,
               Team_status,
               Team_user,
               Team_date,
                        
			         )
                     ";
            SqlParameter[] parameters = new SqlParameter[]
                    {
			              new SqlParameter("Team_code,", SqlDbType.NVarChar, 255),
                      new SqlParameter("Team_name,", SqlDbType.NVarChar, 255),
                      new SqlParameter("Team_status,", SqlDbType.NVarChar, 255),
                      new SqlParameter("Team_user,", SqlDbType.NVarChar, 255),
                      new SqlParameter("Team_date,", SqlDbType.NVarChar, 255),
                           
					    };
            parameters[0].Value = model.Team_code;
            parameters[1].Value = model.Team_name;
            parameters[2].Value = model.Team_status;
            parameters[3].Value = model.Team_user;
            parameters[4].Value = model.Team_date;

            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        int i = SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, parameters);
                        if (i > 0)
                        {
                            trans.Commit();
                            return i > 0;
                        }
                        else
                        {
                            trans.Rollback();
                            return false;
                        }
                    }
                    catch (System.Exception e)
                    {
                        trans.Rollback();
                        return false;
                        throw e;
                    }
                }
            }
        }


    }

}