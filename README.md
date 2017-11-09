>添加了安装包，直接点击下一步下一步就可以了，在桌面生成工具，点击工具可以用了

![安装包.png](http://upload-images.jianshu.io/upload_images/6855212-60c3dd32953cafe8.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)
>### WorkHelper
>
>1. 自己动手写的ado.net 的一个工具，根据数据库表自动生成Model,自动生成insert，update,delete,select，以及导出Excel的方法。
>2. 涉及知识主要是ado.net、winform,sql基本无难点。
>3. 主要解决公司webform大量重复机械的动作，由于本人太懒，所以就写了个工具。
>4. 另外还包括，写日志，审批认可和否决操作代码生成，SQL分页方法生成，Excel导出代码生成手动添加字段实体等映射关系等扩展。
就这样，具体看代码。
>5. 添加了T4模板根据数据库自动生成实体类模板，另外还有生成insert的方法，可以自行在此基础上扩展select，update，delete的方法。
>6. 另外还有数据库存储过程生成实体类的方法，[博客地址](http://www.cnblogs.com/anyushengcms/p/7573289.html)：http://www.cnblogs.com/anyushengcms/p/7573289.html




之前写了篇文章，懒人小工具：[自动生成Model,Insert,Select,Delete以及导出Excel的方法](http://www.jianshu.com/p/d5b11589174a),但是有人觉得这种方法很麻烦。其实我感觉确实是有点麻烦，麻烦在于字符串的拼接。
这种时候我想到了T4模板，用过EF的 DatabaseFirst自动生成实体的同学就明白，dbfirst 自带T4模板，之前我们在学习spring.net框架的时候，也有用过T4模板根据数映射到实体类自动创建仓储。T4模板其实还有很多应用的场景。
T4模板确实挺方便的，但是其实其中用过的原理和我之前做winform小工具差不多。都是根据数据字段和类型的映射生成实体。另外就是ado.net基础知识。
但是这种方法你得了解T4模板的基础语法。各有利弊，但是其实语法也不是很难.
##一、下面我就用最简单的方式用T4模板创建Model.
```
<#@ template language="C#" debug="True" hostspecific="True" #>
<#@ output extension=".cs" #>
<#@ assembly name="System.Data" #>
<#@ assembly name="System.xml" #>
<#@ import namespace="System.Collections.Generic" #>
<#@ import namespace="System.Data.SqlClient" #>
<#@ import namespace="System.Data" #>
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
namespace MyProject.Entities
{     
      <#
          string connectionString = "server=192.168.2.230;database=databasename;uid=uid;pwd=123456";
		     string selectQuery = "select * from Team_";
			 string tableName="Team_";
          SqlConnection conn = new SqlConnection(connectionString);
          conn.Open();
	    	 System.Data.DataTable schema = null;
		        SqlCommand selectcommand = new SqlCommand(selectQuery, conn);
                SqlDataAdapter sda = new SqlDataAdapter(selectcommand);
                System.Data.DataSet dss = new   System.Data.DataSet();
                sda.Fill(dss);
		 	   schema=dss.Tables[0];
                System.Data.DataTable   dt = dss.Tables[0];
                System.Data.DataRow   dr = dss.Tables[0].Rows[0];
           SqlCommand command = new SqlCommand(selectQuery,conn);
           SqlDataAdapter ad = new SqlDataAdapter(command); 

     #>  
 public class <#= tableName#>Biz 
 {      
      public class <#= tableName#>Model      
       {         
		  <#   foreach (DataColumn dc in dss.Tables[0].Columns)
                  {    #>                    
            private <#= dc.DataType.Name #> _<#= dc.ColumnName.Replace(dc.ColumnName[0].ToString(), dc.ColumnName[0].ToString().ToLower())      #>;                      
            public <#= dc.DataType.Name #> <#= dc.ColumnName #>
            {
                   get { return _<#= dc.ColumnName.Replace(dc.ColumnName[0].ToString(), dc.ColumnName[0].ToString().ToLower()) #>; }
                   set { _<#= dc.ColumnName.Replace(dc.ColumnName[0].ToString(), dc.ColumnName[0].ToString().ToLower()) #> = value; }
             }                                                
              <# }  #>     
     
           }     
		   

   }
          
}
```
这种方式是挺简单的。<#@ import namespace="System.Data" #>这段代码是引用命名空间，在你后台代码中用，也就是包括下面ado.net中需要引用的命名空间，基本都写在这里。用 <#...dosomething...#>这段就是不用展示出来的代码，其他的代码就是在你用T4模板生成的时候要用的代码。是不是很简单。上面的代码无需详细解释了吧,运用ado.net链接数据库，获取表字段和类型。Ctrl+S就自动生成了实体类。

![1.png](http://upload-images.jianshu.io/upload_images/6855212-74742dfbf2463036.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)
##二、 根据模板生成的Model
```
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
namespace MyProject.Entities
{

    public class Team_Biz
    {
        public class Team_Model
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

    }

}
```
上面这种是单独某张表的Model生成，下面这段代码是数据库所有表生成实体。大同小异，注意细节。
##三、数据库所有表所有实体
```
<#@ template language="C#" debug="True" hostspecific="True" #>

<#@ assembly name="System.Data" #> 

<#@ assembly name="System.xml" #>

<#@ import namespace="System.Collections.Generic" #>

<#@ import namespace="System.Data.SqlClient" #>

<#@ import namespace="System.Data" #>

<#@ output extension=".cs" #>

 using System;

namespace Test.T4

{     

      <#

           string connectionString="data source=(local);initial catalog=musicstore;user id=sa;password=123456;";

           SqlConnection conn = new SqlConnection(connectionString);

           conn.Open();

           DataTable schema = conn.GetSchema("TABLES");

           string strSql = "select * from @tableName";

           SqlCommand command = new SqlCommand(strSql,conn);

           SqlDataAdapter ad = new SqlDataAdapter(command);

           DataSet ds = new DataSet();        

           foreach(DataRow row in schema.Rows)

           {  #>  

           public class <#= row["TABLE_NAME"].ToString().Trim() #>                   

           {    <#                     

                   ds.Tables.Clear();

                  command.CommandText = strSql.Replace("@tableName",row["TABLE_NAME"].ToString());

                  ad.FillSchema(ds, SchemaType.Mapped, row["TABLE_NAME"].ToString());         
               
                  foreach (DataColumn dc in ds.Tables[0].Columns)

                  {    #>                    
                  public <#= dc.DataType.Name #> <#= dc.ColumnName #> { get; set; }
              <# }  #>         
           }                  

           <# 
                  
           } #>                
           <# conn.Close(); #>
}
```
DataTable schema = conn.GetSchema("TABLES");是获取数据库所有表名，然后遍历所有表名遍历表字段和类型，根据这些表生成实体。
另外还有一种方法是用数据存储过程自动生成实体。这种方法就需要SQL的基础知识了。
##四、存储过程生成实体
```
SET ANSI_NULLS ON;  
SET QUOTED_IDENTIFIER ON;  
GO  
   
CREATE PROC [dbo].[p_db_wsp]
    @dbname VARCHAR(50) ,   --数据库名  
    @path VARCHAR(100) ,    --实体类所在目录名，如D:/My/Models  
    @namespace VARCHAR(50) --实体类命名空间,默认值为Models  
AS --判断数据库是否存在  
    IF ( DB_ID(@dbname) IS NOT NULL )
        BEGIN  
            IF ( ISNULL(@namespace, '') = '' )
                SET @namespace = 'Models';  
-- 允许配置高级选项  
            EXEC sp_configure 'show advanced options', 1;  
-- 重新配置  
            RECONFIGURE;  
-- 启用Ole Automation Procedures   
            EXEC sp_configure 'Ole Automation Procedures', 1;  
-- 启用xp_cmdshell,可以向磁盘中写入文件  
            EXEC sp_configure 'xp_cmdshell', 1;  
-- 重新配置  
            RECONFIGURE;  
            DECLARE @dbsql VARCHAR(1000) ,
                @tablename VARCHAR(100);  
            SET @dbsql = 'declare wsp cursor for select name from ' + @dbname
                + '..sysobjects where xtype=''u''  and name <>''sysdiagrams''';  
            EXEC(@dbsql);  
            OPEN wsp;  
            FETCH wsp INTO @tablename;--使用游标循环遍历数据库中每个表  
            WHILE ( @@fetch_status = 0 )
                BEGIN  
--根据表中字段组合实体类中的字段和属性  
                    DECLARE @nsql NVARCHAR(4000) ,
                        @sql VARCHAR(8000);  
                    SET @nsql = 'select @s=isnull(@s+char(9)+''private '',''using System;'
                        + CHAR(13) + 'using System.Collections.Generic;'
                        + CHAR(13) + 'using System.Text;' + CHAR(13)
                        + 'namespace ' + @namespace + CHAR(13) + '{' + CHAR(13)
                        + CHAR(9) + 'public class ' + @tablename + CHAR(13)
                        + '{''+char(13)+char(9)+''private '')+  
case when a.name in(''image'',''uniqueidentifier'',''ntext'',''varchar'',''ntext'',''nchar'',''nvarchar'',''text'',''char'') then ''string''  
when a.name in(''tinyint'',''smallint'',''int'',''bigint'') then ''int''  
when a.name in(''datetime'',''smalldatetime'') then ''DateTime''  
when a.name in(''float'',''decimal'',''numeric'',''money'',''real'',''smallmoney'') then ''decimal''  
when a.name =''bit'' then ''bool''  
else a.name end+'' ''+lower(''_''+b.name)+'';''+char(13)+char(9)+''public ''+  
case when a.name in(''image'',''uniqueidentifier'',''ntext'',''varchar'',''ntext'',''nchar'',''nvarchar'',''text'',''char'') then ''string''  
when a.name in(''tinyint'',''smallint'',''int'') then ''int''  
when a.name=''bigint'' then ''long''  
when a.name in(''datetime'',''smalldatetime'') then ''DateTime''  
when a.name in(''float'',''decimal'',''numeric'',''money'',''real'',''smallmoney'') then ''decimal''  
when a.name =''bit'' then ''bool''  
else a.name end  
+'' ''+b.name+char(13)+char(9)+''{''+char(13)+char(9)+char(9)+''get{return ''+lower(''_''+b.name)+'';}''+  
char(13)+char(9)+char(9)+''set{''+lower(''_''+b.name)+''=value;}''+char(13)+char(9)+''}''+char(13)  
from ' + @dbname + '..syscolumns b,  
(select distinct name,xtype from ' + @dbname + '..systypes where status=0) a  
where a.xtype=b.xtype and b.id=object_id(''' + @dbname + '..' + @tablename
                        + ''')';  
                    EXEC sp_executesql @nsql, N'@s varchar(8000) output',
                        @sql OUTPUT;  
                    SET @sql = @sql + CHAR(9) + '}' + CHAR(13) + '}';  
--print @sql  
                    DECLARE @err INT ,
                        @fso INT ,
                        @fleExists BIT ,
                        @file VARCHAR(100);  
                    SET @file = @path + '/' + @tablename + '.cs';  
                    EXEC @err= sp_OACreate 'Scripting.FileSystemObject',
                        @fso OUTPUT;  
                    EXEC @err= sp_OAMethod @fso, 'FileExists',
                        @fleExists OUTPUT, @file;  
                    EXEC @err = sp_OADestroy @fso;  
   
                    IF @fleExists != 0
                        EXEC('exec xp_cmdshell ''del '+@file+''''); --存在则删除  
                    EXEC('exec xp_cmdshell ''echo '+@sql+' > '+@file+''''); --将文本写进文件中  
                    SET @sql = NULL;  
                    FETCH wsp INTO @tablename;  
                END;  
            CLOSE wsp;  
            DEALLOCATE wsp;  
            PRINT '生成成功！';  
        END;  
    ELSE
        PRINT '数据库不存在！';  
```
调用存储过程：    EXEC [dbo].[p_db_wsp] '数据库名字', '保存的路径：D:\work\新建文件夹', '生成实体类名字';
另外：[github地址](https://github.com/Jimmey-Jiang/JWorkHelper)：https://github.com/Jimmey-Jiang/JWorkHelper
