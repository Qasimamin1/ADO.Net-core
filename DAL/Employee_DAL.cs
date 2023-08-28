using CRUDwithADONet.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;

namespace CRUDwithADONet.DAL
{
    public class Employee_DAL
    {
        private readonly string _connectionString;
        private SqlCommand _command;

        public Employee_DAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Employee> GetAll()
        {
            List<Employee> employeeList = new List<Employee>();

            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand _command = new SqlCommand("[dbo].[usp_Get_Employees]", _connection))
                {
                    _command.CommandType = CommandType.StoredProcedure;
                    _connection.Open();

                    using (SqlDataReader dr = _command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Employee employee = new Employee();
                            employee.Id = Convert.ToInt32(dr["Id"]);
                            employee.FirstName = dr["FirstName"].ToString();
                            employee.LastName = dr["LastName"].ToString();
                            employee.DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]);
                            employee.Email = dr["Email"].ToString();
                            employee.Salary = Convert.ToDouble(dr["Salary"]);
                            employeeList.Add(employee);
                        }
                    }
                }
            }

            return employeeList;
        }

        public bool Insert(Employee model)
        {
            int id = 0;

            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[dbo].[usp_Insert_Employee]";
                _command.Parameters.AddWithValue("@FirstName", model.FirstName);
                _command.Parameters.AddWithValue("@LastName", model.LastName);
                _command.Parameters.AddWithValue("@DateOfBirth", model.DateOfBirth);
                _command.Parameters.AddWithValue("@Email", model.Email);
                _command.Parameters.AddWithValue("@Salary", model.Salary);

                _connection.Open();
                id = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return id > 0 ? true : false;
        }

        public Employee GetById(int id)
        {
            Employee employee = new Employee();

            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand _command = new SqlCommand("[dbo].[usp_Get_EmployeeById]", _connection))
                    // Assuming the stored procedure to get an employee by id is named "usp_Get_EmployeeById"
                {
                    _command.CommandType = CommandType.StoredProcedure;
                    _command.Parameters.AddWithValue("@Id", id); 
                    // Assuming the stored procedure takes @Id as a parameter
                    _connection.Open();

                    using (SqlDataReader dr = _command.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            employee.Id = Convert.ToInt32(dr["Id"]);
                            employee.FirstName = dr["FirstName"].ToString();
                            employee.LastName = dr["LastName"].ToString();
                            employee.DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]);
                            employee.Email = dr["Email"].ToString();
                            employee.Salary = Convert.ToDouble(dr["Salary"]);
                        }
                    }
                }
            }

            return employee;
        }

        public bool Update(Employee model)
        {
            int id = 0;

            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[dbo].[usp_Update_Employee]";
                _command.Parameters.AddWithValue("@Id", model.Id);
                _command.Parameters.AddWithValue("@FirstName", model.FirstName);
                _command.Parameters.AddWithValue("@LastName", model.LastName);
                _command.Parameters.AddWithValue("@DateOfBirth", model.DateOfBirth);
                _command.Parameters.AddWithValue("@Email", model.Email);
                _command.Parameters.AddWithValue("@Salary", model.Salary);

                _connection.Open();
                id = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return id > 0 ? true : false;
        }

        public bool Delete(int id)
        {
            int rowsAffected;

            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[dbo].[usp_Delete_Employee]";
                _command.Parameters.AddWithValue("@Id", id);

                _connection.Open();
                rowsAffected = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return rowsAffected > 0 ? true : false;
        }


    }
}
