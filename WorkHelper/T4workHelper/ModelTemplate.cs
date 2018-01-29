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