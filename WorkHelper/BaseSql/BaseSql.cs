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

namespace DevLogHelper.BaseSql
{
    /// <summary>
    /// 代码生成基类 
    /// </summary>
    public class BaseSql
    {
        #region 默认生成基本的插入语句 Sql插入、删除、更新 、查询、Excel导出方法+StringBuilder
        /// <summary>
        /// 默认生成基本的插入语句 Sql插入、删除、更新
        /// </summary>
        /// github地址：https://github.com/Jimmey-Jiang
        /// <param name="inputSql">多参数
        /// 0:对应的Sql
        /// 1:参数是否为实体</param>
        /// <returns></returns>
        public virtual StringBuilder BuilderCode(params object[] inputSql)
        {
            string inputCode = inputSql[0].ToString();
            CheckBox cb = inputSql[1] as CheckBox;
            string Table = inputSql[2].ToString();
            if (string.IsNullOrWhiteSpace(inputCode))
            {
                inputCode = "select *from " + Table;
            }
            List<string> listPara = new List<string>();
            Regex rex = new Regex("@[A-Za-z0-9]+");
            MatchCollection matchs = rex.Matches(inputCode);
            if (matchs.Count > 0)
            {
                foreach (Match match in matchs)
                {
                    string temp = match.Groups[0].Value;
                    var t = listPara.Where(p => p == temp).ToList();
                    if (listPara.Count == 0 || t.Count <= 0) //先判断之前是否存在
                    {
                        listPara.Add(temp);
                    }
                }
            }

            StringBuilder returnstr = new StringBuilder();
            StringBuilder strBuilder = new StringBuilder();
            DataSet ds = SqlHelper.Query(inputCode);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            string Id = string.Empty; //业务主Id,一般情况第一个字段是主键，当然如果第一个字段不是主键，那就需要修改了

            #region 封装实体Model

            strBuilder.AppendLine(@"
               public class Model
            {
               ");
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                if (i == 0)
                {
                    Id = ds.Tables[0].Columns[0].ToString();//一般情况第一个字段是主键，当然如果第一个字段不是主键，那就需要修改了
                }
                string Type = ds.Tables[0].Columns[i].DataType.ToString();
                switch (Type)
                {
                    case "System.String":
                       // strBuilder.AppendLine("       private string " + "_" + ds.Tables[0].Columns[i] + ";");
                        strBuilder.AppendLine("       public string " + ds.Tables[0].Columns[i] + "");
                        strBuilder.AppendLine("         { get; set; }");
                    //    strBuilder.AppendLine("            get { return " + "_" + ds.Tables[0].Columns[i] + "; }");
                      //  strBuilder.AppendLine("            set { " + "_" + ds.Tables[0].Columns[i] + " = value; }");
                       // strBuilder.AppendLine("          }");
                        break;
                    case "System.Int":
                       // strBuilder.AppendLine("       private Int " + "_" + ds.Tables[0].Columns[i] + ";");
                        strBuilder.AppendLine("       public System.Int " + ds.Tables[0].Columns[i] + "");
                        strBuilder.AppendLine("         { get; set; }");
                        //strBuilder.AppendLine("         {");
                        //strBuilder.AppendLine("            get { return " + "_" + ds.Tables[0].Columns[i] + "; }");
                        //strBuilder.AppendLine("             set { " + "_" + ds.Tables[0].Columns[i] + " = value; }");
                        //strBuilder.AppendLine("         }");
                        break;
                    case "System.Int32":
                      //  strBuilder.AppendLine("       private Int " + "_" + ds.Tables[0].Columns[i] + ";");
                        strBuilder.AppendLine("       public System.Int " + ds.Tables[0].Columns[i] + "");
                        strBuilder.AppendLine("         { get; set; }");
                        //strBuilder.AppendLine("         {");
                        //strBuilder.AppendLine("            get { return " + "_" + ds.Tables[0].Columns[i] + "; }");
                        //strBuilder.AppendLine("             set { " + "_" + ds.Tables[0].Columns[i] + " = value; }");
                        //strBuilder.AppendLine("         }");
                        break;
                    case "System.DateTime":
                     //   strBuilder.AppendLine("       private System.DateTime " + "_" + ds.Tables[0].Columns[i] + ";");
                        strBuilder.AppendLine("       public System.DateTime " + ds.Tables[0].Columns[i] + "");
                        strBuilder.AppendLine("         { get; set; }");
                        //strBuilder.AppendLine("          {");
                        //strBuilder.AppendLine("             get { return " + "_" + ds.Tables[0].Columns[i] + "; }");
                        //strBuilder.AppendLine("             set { " + "_" + ds.Tables[0].Columns[i] + " = value; }");
                        //strBuilder.AppendLine("          }");
                        break;
                    case "System.Decimal":
                   //     strBuilder.AppendLine("       private System.Decimal " + "_" + ds.Tables[0].Columns[i] + ";");
                        strBuilder.AppendLine("       public System.Decimal " + ds.Tables[0].Columns[i] + "");
                        strBuilder.AppendLine("         { get; set; }");
                        //strBuilder.AppendLine("        {");
                        //strBuilder.AppendLine("            get { return " + "_" + ds.Tables[0].Columns[i] + "; }");
                        //strBuilder.AppendLine("            set { " + "_" + ds.Tables[0].Columns[i] + " = value; }");
                        //strBuilder.AppendLine("         }");
                        break;
                    default:
                       // strBuilder.AppendLine("       private string " + "_" + ds.Tables[0].Columns[i] + ";");
                        strBuilder.AppendLine("       public string " + ds.Tables[0].Columns[i] + "");
                        strBuilder.AppendLine("         { get; set; }");
                        //strBuilder.AppendLine("        {");
                        //strBuilder.AppendLine("             get { return " + "_" + ds.Tables[0].Columns[i] + "; }");
                        //strBuilder.AppendLine("             set { " + "_" + ds.Tables[0].Columns[i] + " = value; }");
                        //strBuilder.AppendLine("         }");
                        break;
                }
            }
            strBuilder.AppendLine(@"
              }
               ");

            #endregion

            string istrue = inputSql[3].ToString();
            if ((bool)inputSql[3])
            {
                returnstr.AppendLine(strBuilder.ToString());
            }
            strBuilder = new StringBuilder();
            #region 生成插入Insert方法

            #region 生成插入sql语句

            StringBuilder strTmp = new StringBuilder();
            try
            {
                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)//生成insert 
                {
                    if (i == 0)
                    {
                        strTmp.AppendLine("               INSERT " + Table + "(");
                    }
                    if (i == ds.Tables[0].Columns.Count - 1)
                    {
                        strTmp.AppendLine("                 " + ds.Tables[0].Columns[i].ToString() + ")");
                    }
                    else
                    {
                        strTmp.AppendLine("                 " + ds.Tables[0].Columns[i].ToString() + ",");
                    }
                }

                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    if (i == 0)
                    {
                        strTmp.AppendLine("                 VALUES " + "(");
                    }
                    if (i == ds.Tables[0].Columns.Count - 1)
                    {
                        strTmp.AppendLine("                   @" + ds.Tables[0].Columns[i].ToString() + ")");
                    }
                    else
                    {
                        strTmp.AppendLine("                    @" + ds.Tables[0].Columns[i].ToString() + ",");
                    }

                }
            }
            catch (System.Exception ex)
            {

                throw ex;
            }

            #endregion





            strBuilder.AppendLine("   public bool Insert" + Table + "(Model model, out string errMsg)");
            strBuilder.AppendLine(@"    {   
           ");
            strBuilder.AppendLine("                    string strSql = @\"");
            strBuilder.AppendLine(strTmp.ToString());
            strBuilder.AppendLine("                     \";");

            strBuilder.AppendLine(@"                 SqlParameter[] parameters = new SqlParameter[]
                                                  {");
            //参数类型
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                string Type = ds.Tables[0].Columns[i].DataType.ToString();
                switch (Type)
                {
                    case "System.String":
                        strBuilder.AppendLine("                     new SqlParameter(\"" + "@" + ds.Tables[0].Columns[i] + "\", SqlDbType.NVarChar, 255),");
                        break;
                    case "System.Int":
                        strBuilder.AppendLine("                     new SqlParameter(\"" + "@" + ds.Tables[0].Columns[i] + "\", SqlDbType.Int),");
                        break;
                    case "System.Int32":
                        strBuilder.AppendLine("                     new SqlParameter(\"" + "@" + ds.Tables[0].Columns[i] + "\", SqlDbType.Int),");
                        break;
                    case "System.DateTime":
                        strBuilder.AppendLine("                     new SqlParameter(\"" + "@" + ds.Tables[0].Columns[i] + "\", SqlDbType.DateTime),");
                        break;
                    case "System.Decimal":
                        strBuilder.AppendLine("                      new SqlParameter(\"" + "@" + ds.Tables[0].Columns[i] + "\", SqlDbType.Decimal),");
                        break;
                    default:
                        strBuilder.AppendLine("                      new SqlParameter(\"" + "@" + ds.Tables[0].Columns[i] + "\", SqlDbType.NVarChar, 255),");
                        break;
                }
            }
            strBuilder.AppendLine(@"                            };");

            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                strBuilder.AppendLine("                        parameters[" + i + "].Value =" + "model." + ds.Tables[0].Columns[i] + ";");
            }

            strBuilder.AppendLine(@"
            errMsg = string.Empty;
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
                    catch (System.Exception ex)
                    {
                        trans.Rollback();
                        errMsg = ex.Message;
                        throw ex;
                    }
                  }
               }
               ");
            strBuilder.AppendLine("}");

            #endregion

            if ((bool)inputSql[4])
            {
                returnstr.AppendLine(strBuilder.ToString());
            }
            strBuilder = new StringBuilder();
            #region 生成更新Update 方法

            #region 生成更新Update sql语句

            strTmp = new StringBuilder(); //sql 
            try
            {
                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)//生成Update 
                {
                    if (i == 0)
                    {
                        strTmp.AppendLine("               Update " + Table + "  SET ");
                    }
                    if (i == ds.Tables[0].Columns.Count - 1)
                    {


                        strTmp.AppendLine("                 " + ds.Tables[0].Columns[i].ToString() + "=" + "@" + ds.Tables[0].Columns[i].ToString() + " where " + Id + "=" + "@" + Id + "  ");
                    }
                    else
                    {
                        strTmp.AppendLine("                  " + ds.Tables[0].Columns[i].ToString() + "=" + "@" + ds.Tables[0].Columns[i].ToString() + ",");
                    }
                }
            }
            catch (System.Exception ex)
            {

                throw ex;
            }

            #endregion




            strBuilder.AppendLine("   public bool Update" + Table + "ById(Model model,out string errMsg)");
            strBuilder.AppendLine(@"    {   
           ");
            strBuilder.AppendLine("                    string strSql = @\"");
            strBuilder.AppendLine(strTmp.ToString());
            strBuilder.AppendLine("                     \";");

            strBuilder.AppendLine(@"                 SqlParameter[] parameters = new SqlParameter[]
                                                  {");
            //参数类型
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                string Type = ds.Tables[0].Columns[i].DataType.ToString();
                switch (Type)
                {
                    case "System.String":
                        strBuilder.AppendLine("                     new SqlParameter(\"" + "@" + ds.Tables[0].Columns[i] + "\", SqlDbType.NVarChar, 255),");
                        break;
                    case "System.Int":
                        strBuilder.AppendLine("                     new SqlParameter(\"" + "@" + ds.Tables[0].Columns[i] + "\", SqlDbType.Int),");
                        break;
                    case "System.DateTime":
                        strBuilder.AppendLine("                     new SqlParameter(\"" + "@" + ds.Tables[0].Columns[i] + "\", SqlDbType.DateTime),");
                        break;
                    case "System.Decimal":
                        strBuilder.AppendLine("                      new SqlParameter(\"" + "@" + ds.Tables[0].Columns[i] + "\", SqlDbType.Decimal),");
                        break;
                    default:
                        strBuilder.AppendLine("                      new SqlParameter(\"" + "@" + ds.Tables[0].Columns[i] + "\", SqlDbType.NVarChar, 255),");
                        break;
                }
            }
            strBuilder.AppendLine(@"                            };");

            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                strBuilder.AppendLine("                        parameters[" + i + "].Value =" + "model." + ds.Tables[0].Columns[i] + ";");
            }

            strBuilder.AppendLine(@"
           errMsg=string.Empty;
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
                    catch (System.Exception ex)
                    {
                       trans.Rollback();
                        errMsg = ex.Message;
                        throw ex;
                    }
                  }
               }
               ");
            strBuilder.AppendLine("}");

            #endregion

            if ((bool)inputSql[5])
            {
                returnstr.AppendLine(strBuilder.ToString());
            }
            strBuilder = new StringBuilder();
            #region 生成查询方法

            strBuilder.AppendLine("   public DataTable GetDataBy" + Table + "(Model model , int pageNo, int pageSize, ref int iRecordCount)");
            strBuilder.AppendLine(@"    {    ");
            strTmp = new StringBuilder();
            strTmp.AppendLine("WITH temp AS ( SELECT rn =  ROW_NUMBER() OVER (ORDER BY " + Id + "  desc), *FROM  " + Table + " WHERE 1=1 {0} )");
            strTmp.AppendLine("SELECT *,rc=(select count(1) from temp) FROM temp WHERE rn BETWEEN {1} AND {2}");

            strBuilder.AppendLine("        List<SqlParameter> parameters = new List<SqlParameter>();");
            strBuilder.AppendLine("        StringBuilder sqlWhere = new StringBuilder();");
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                strBuilder.AppendLine("       if (!string.IsNullOrEmpty(model." + ds.Tables[0].Columns[i].ToString() + ".ToString())) ");
                strBuilder.AppendLine("       {");
                strBuilder.AppendLine("          sqlWhere.Append(\" AND " + ds.Tables[0].Columns[i].ToString() + "=@" + ds.Tables[0].Columns[i].ToString() + "\");");
                strBuilder.AppendLine("          parameters.Add(new SqlParameter(\"@" + ds.Tables[0].Columns[i].ToString() + "\", SqlDbType.NVarChar, 255) { SqlValue = model." + ds.Tables[0].Columns[i].ToString() + " });");
                strBuilder.AppendLine("       } ");
            }
            strBuilder.AppendLine("                    string strSql = string.Format(@\"");
            strBuilder.AppendLine(strTmp.ToString());
            strBuilder.AppendLine("                     \" ,sqlWhere.ToString(), (pageNo - 1) * pageSize + 1, pageNo * pageSize);");

            strBuilder.AppendLine(@"    

                    DataTable dt = SqlHelper.QueryDT(strSql, parameters.ToArray());
                  if (dt!=null)
                   {");
            strBuilder.AppendLine("        iRecordCount = int.Parse(dt.Rows[0][\"" + "rc" + "\"].ToString());");
            strBuilder.AppendLine(@"      return dt;
                            }
                           else
                          {
                              iRecordCount = 0;
                            return null;
                        }
                }    ");

            #endregion

            if ((bool)inputSql[6])
            {
                returnstr.AppendLine(strBuilder.ToString());
            }
            strBuilder = new StringBuilder();
            #region 生成删除的方法

            strBuilder.AppendLine("    public bool Delete" + Table + "(string " + Id + ")");
            strBuilder.AppendLine("   {");
            strBuilder.AppendLine("        List<SqlParameter> parameters = new List<SqlParameter>();");
            strTmp = new StringBuilder();
            strTmp.AppendLine("DELETE " + Table + " WHERE " + Id + "=" + Id + "");
            strBuilder.AppendLine("                    string strSql = string.Format(@\"");
            strBuilder.AppendLine(strTmp.ToString());
            strBuilder.AppendLine("                     \";");
            strBuilder.AppendLine("     int rowAffect =SqlHelper.Query(strSql, parameters.ToArray()).ToInt();;");
            strBuilder.AppendLine("      return rowAffect > 0 ? true : false;");
            strBuilder.AppendLine("      }");

            #endregion

            if ((bool)inputSql[7])
            {
                returnstr.AppendLine(strBuilder.ToString());
            }
            strBuilder = new StringBuilder();
            #region 导出excel方法

            strBuilder.AppendLine(@"     protected void btnExcel_Click(object sender, System.EventArgs e)
        {
            int intPageNo = 1;
            int intPageSize = 65535;
            int recordCount = 0;
            DataTable dt = GetUnAuthorizeAgreePassedDT( Model model intPageNo, intPageSize, out recordCount); ");
            strBuilder.AppendLine("     string strFileName = \"" + "导出Excel" + "\" + System.DateTime.Now.ToString(\" " + "yyyyMMddHHmmss" + "\");");
            strBuilder.AppendLine(@"        ExcelUtility excelUtil = new ExcelUtility(this, strFileName);
            List<ExcelHeader> headerS = new List<ExcelHeader>() { ");

            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                string Type = ds.Tables[0].Columns[i].DataType.ToString();
                switch (Type)
                {
                    case "System.DateTime":
                        strBuilder.AppendLine("           new ExcelHeader() { Name = \"" + ds.Tables[0].Columns[i] + "\", DataType = EnumColumnDataType.日期, Width = 15 },");
                        break;
                    default:
                        strBuilder.AppendLine("           new ExcelHeader() { Name = \"" + ds.Tables[0].Columns[i] + "\", DataType = EnumColumnDataType.文本, Width = 15 },");
                        break;
                }
            }
            strBuilder.AppendLine(@"          };
            excelUtil.CreateHeader(headerS);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    List<string> dataVals = new List<string>() { ");


            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                strBuilder.AppendLine("                            dr[\"" + ds.Tables[0].Columns[i] + "\"].ToString(), ");
            }

            strBuilder.AppendLine(@"          };       
                    excelUtil.CreateItemRow(dataVals);
                }
            }
            excelUtil.Export();
            }
           "); 
            #endregion
            if ((bool)inputSql[8])
            {
                returnstr.AppendLine(strBuilder.ToString());
                strBuilder = new StringBuilder();
            }
            return returnstr;
        }


        #endregion

    }
}