using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class UserService: BaseService
    {
        public User GetUserById(int Id)
        {
            try
            {
                string command = "select * from dbo.Users " +
                "where " +
                $"UserId = {Id}" +
                "AND " +
                "is_deleted = 0";
                DataSet ds = ExecuteQuery(command);

                if(ds.Tables[0]?.Rows.Count == 1)
                {
                    return new User
                    {
                        UserId = Convert.ToInt32(ds.Tables[0].Rows[0]["UserId"]),
                        UserName = Convert.ToString(ds.Tables[0].Rows[0]["UserName"]),
                        RoleId = Convert.ToInt32(ds.Tables[0].Rows[0]["RoleId"]),
                        Email = Convert.ToString(ds.Tables[0].Rows[0]["Email"]),
                        PhoneNo = Convert.ToString(ds.Tables[0].Rows[0]["PhoneNo"]),
                        IsDeleted = Convert.ToInt32(ds.Tables[0].Rows[0]["is_deleted"])
                    };
                }
                else
                {
                    throw new Exception("user not found");
                }
            }
            catch (RowNotInTableException ex)
            {
                throw ex;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            
        }
        public List<User> GetAllUsers()
        {
            try
            {
                string command = "select * from dbo.Users where is_deleted = 0";
                DataSet result = ExecuteQuery(command);
                List<User> UsersList = new List<User>();

                if(result.Tables[0]?.Rows.Count > 0)
                {
                    foreach (DataRow Row in result.Tables[0].Rows)
                    {
                        UsersList.Add(new User
                        {
                            UserId = Convert.ToInt32(Row["UserId"]),
                            UserName = Convert.ToString(Row["UserName"]),
                            RoleId = Convert.ToInt32(Row["RoleId"]),
                            Email = Convert.ToString(Row["Email"]),
                            PhoneNo = Convert.ToString(Row["PhoneNo"]),
                            IsDeleted = Convert.ToInt32(Row["is_deleted"])
                        });
                    }
                    return UsersList;
                }
                else
                {
                    throw new Exception("there are no users in the database");
                }
                
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
            
        }
        public void CreateUser(User user)
        {
            try
            {
                string command = "insert into dbo.Users(UserName,PhoneNo,Email,Password,RoleId,CreatedBy,CreatedDate) values " +
                $"('{user.UserName}','{user.PhoneNo}','{user.Email}','{user.Password}',1,'{user.CreatedBy}','{DateTime.Now}')";
                int EffectedRows = ExecuteNonQuery(command);
                if(EffectedRows == 0)
                {
                    throw new Exception("unable to create user");
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            
        }
        public void UpdateUser(User user)
        {
            try
            {
                string command = "update dbo.Users set " +
                $"UserName = '{user.UserName}', "  +
                $"PhoneNo = '{user.PhoneNo}', " +
                $"Password = '{user.Password}', " +
                $"UpdatedBy = '{user.UpdatedBy}', " +
                $"UpdatedDate = '{DateTime.Now}' " +
                "where " +
                $"UserId={user.UserId}";
                int EffectedRows = ExecuteNonQuery(command);
                if (EffectedRows == 0)
                {
                    throw new Exception("unable to update user details");
                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
            

        }
        public void DeleteUser(User user)
        {
            try
            {
                string command = "update dbo.Users set " +
                $"is_deleted = 1, " +
                $"DeletedBy = '{user.DeletedBy}', " +
                $"DeletedDate = '{DateTime.Now}' " +
                "where " +
                $"UserId={user.UserId}";
                ExecuteNonQuery(command);
                int EffectedRows = ExecuteNonQuery(command);
                if (EffectedRows == 0)
                {
                    throw new Exception("unable to delete user");
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            
        }
    }
}