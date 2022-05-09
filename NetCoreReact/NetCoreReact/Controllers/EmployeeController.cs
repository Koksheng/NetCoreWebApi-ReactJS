using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NetCoreReact.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreReact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public JsonResult Get()
        {
            string query = @"select * from
                            dbo.Employee";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Employee emp)
        {
            string query = @"Insert into
                            dbo.Employee
                            Values (@EmployeeName, @Department, @DateOfJoining, @PhotoFileName)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
                    myCommand.Parameters.AddWithValue("@Department", emp.Department);
                    myCommand.Parameters.AddWithValue("@DateOfJoining", emp.DateOfJoining);
                    myCommand.Parameters.AddWithValue("@PhotoFileName", emp.PhotoFileName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Employee emp)
        {
            string query = @"Update 
                            dbo.Employee
                            Set EmployeeName = @EmployeeName,
                                Department = @Department,
                                DateOfJoining = @DateOfJoining,
                                PhotoFileName = @PhotoFileName
                            Where EmployeeId = @EmployeeId";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader = null ;

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {


                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@EmployeeId", emp.EmployeeId);
                    myCommand.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
                    myCommand.Parameters.AddWithValue("@Department", emp.Department);
                    myCommand.Parameters.AddWithValue("@DateOfJoining", emp.DateOfJoining);
                    myCommand.Parameters.AddWithValue("@PhotoFileName", emp.PhotoFileName);
                    try
                    {
                        //myReader = myCommand.ExecuteReader();
                        myCommand.ExecuteNonQuery();
                        myCon.Close();
                    }
                    catch (Exception ex)
                    {
                        //myReader.Close();
                        //return new JsonResult("Updated UnSuccessfully");
                    }
                    finally
                    {

                        //table.Load(myReader);
                        //if (myReader.RecordsAffected > 0)
                        //{

                        //}
                        //else
                        //{

                        //}
                        //myReader.Close();
                        //myCon.Close();
                    }

                }
            }
            
            
            
            
            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"Delete dbo.Employee
                            Where EmployeeId = @EmployeeId";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@EmployeeId", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Deleted Successfully");
        }
    }
}
