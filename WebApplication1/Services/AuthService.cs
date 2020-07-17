using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class AuthService: BaseService
    {
        public User LoginUser(LoginModal LoginModal)
        {
            try
            {
                string command = "select * from dbo.Users where " +
                    $"Email='{LoginModal.Email}' " +
                    "AND " +
                    $"Password='{LoginModal.Password}' " +
                    "AND " +
                    "is_deleted = 0"; 
                DataSet ds = ExecuteQuery(command);
                if(ds.Tables[0].Rows.Count == 1)
                {
                    return new User {
                        UserId = Convert.ToInt32(ds.Tables[0].Rows[0]["UserId"]),
                        UserName = Convert.ToString(ds.Tables[0].Rows[0]["UserName"]),
                        RoleId = Convert.ToInt32(ds.Tables[0].Rows[0]["RoleId"]),
                        Email = Convert.ToString(ds.Tables[0].Rows[0]["Email"])
                    };
                }
                else
                {
                    throw new Exception("incorrect details provided");
                }

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public string AuthorizeUser(int USerId)
        {
            try
            {
                string command = "select * from dbo.Users " +
                    "where " +
                    "RoleId = 1 " +
                    "and " +
                    $"UserId = {USerId} " +
                    "AND " +
                    "is_deleted = 0"; 
                DataSet ds = ExecuteQuery(command);
                if (ds.Tables[0].Rows.Count == 1)
                {
                    string UserName = Convert.ToString(ds.Tables[0].Rows[0]["UserName"]);
                    return UserName;
                }
                else
                {
                    throw new Exception("user Unauthorized to perform this request");
                }
                
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}