using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class TechnologyService: BaseService
    {
        public Technology GetTechnologyById(int Id)
        {
            try
            {
                string command = "select * from dbo.Technologies " +
                "where " +
                $"TechId = {Id} " +
                "AND " +
                "is_deleted = 0";
                DataSet ds = ExecuteQuery(command);
                if (ds.Tables[0]?.Rows.Count == 1)
                {
                    return new Technology
                    {
                        TechnologyId = Convert.ToInt32(ds.Tables[0].Rows[0]["TechId"]),
                        TechnologyName = Convert.ToString(ds.Tables[0].Rows[0]["TechName"]),
                        IsDeleted = Convert.ToInt32(ds.Tables[0].Rows[0]["is_deleted"]),
                    };
                }
                else
                {
                    throw new Exception("technology not found");
                }

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            
        }
        public List<Technology> GetAllTechnologies()
        {
            try
            {
                string command = "select * from dbo.Technologies where is_deleted = 0";
                DataSet result = ExecuteQuery(command);
                List<Technology> TechnologyList = new List<Technology>();
                if(result.Tables[0]?.Rows.Count > 0)
                {
                    foreach (DataRow Row in result.Tables[0].Rows)
                    {
                        TechnologyList.Add(new Technology
                        {
                            TechnologyId = Convert.ToInt32(Row["TechId"]),
                            TechnologyName = Convert.ToString(Row["TechName"]),
                            IsDeleted = Convert.ToInt32(Row["is_deleted"]),
                        });
                    }
                    return TechnologyList;
                }
                else
                {
                    throw new Exception("there are no technologies in the database");
                }

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            
        }
        public void CreateTechnology(Technology technology)
        {
            try
            {
                string command = "insert into dbo.Technologies(TechName,CreatedBy,CreatedDate) values " +
                $"('{technology.TechnologyName}','{technology.CreatedBy}','{DateTime.Now}')";
                int EffectedRows = ExecuteNonQuery(command);
                if (EffectedRows == 0)
                {
                    throw new Exception("unable to create technology");
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            
        }
        public void UpdateTechnology(Technology technology)
        {
            try
            {
                string command = "update dbo.Technologies set " +
                $"TechName = '{technology.TechnologyName}', " +
                $"UpdatedBy = '{technology.UpdatedBy}', " +
                $"UpdatedDate = '{DateTime.Now}' " +
                "where " +
                $"TechId={technology.TechnologyId}";
                int EffectedRows = ExecuteNonQuery(command);
                if (EffectedRows == 0)
                {
                    throw new Exception("unable to update technology details");
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            
        }
        public void DeleteTechnology(Technology technology)
        {
            try
            {
                string command = "update dbo.Technologies set " +
                $"is_deleted = 1, " +
                $"DeletedBy = '{technology.DeletedBy}', " +
                $"DeletedDate = '{DateTime.Now}' " +
                "where " +
                $"TechId={technology.TechnologyId}";
                int EffectedRows = ExecuteNonQuery(command);
                if (EffectedRows == 0)
                {
                    throw new Exception("unable to delete technology");
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            
        }
    }
}