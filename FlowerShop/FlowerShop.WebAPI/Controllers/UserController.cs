using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using FlowerShop.WebAPI.Models;

namespace FlowerShop.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private string connectionString = WebApplication.Create().Configuration.GetConnectionString("DefaultConnection");

        [HttpDelete(Name = "DeleteUser")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = @"DELETE FROM ""User"" WHERE ""Id"" = @Id";

                        // Dodavanje parametra
                        command.Parameters.AddWithValue("@Id", id);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            return NotFound($"User with Id = {id} not found.");
                        }
                    }
                }

                return Ok($"User with Id = {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(Name = "GetUsers")]
        public IActionResult GetUsers()
        {
            try
            {
                List<User> users = new List<User>();
                using (var connection = new NpgsqlConnection(connectionString))
                {

                    connection.Open();

                    using (var command = new NpgsqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = @"SELECT * FROM ""User"" ";

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    int id = reader.GetInt32(reader.GetOrdinal("Id"));
                                    string firstName = reader.GetString(reader.GetOrdinal("FirstName"));
                                    string lastName = reader.GetString(reader.GetOrdinal("LastName"));
                                    string role = reader.GetString(reader.GetOrdinal("Role"));

                                    User user = new User();

                                    user.Id = id;
                                    user.FirstName = firstName;
                                    user.LastName = lastName;
                                    user.Role = role;

                                    users.Add(user);

                                }

                            }
                        }
                    }

                }

                return Ok(users);
            }
            catch (Exception ex)
            {


                return BadRequest(ex.Message);
            }

        }
        [HttpPut(Name = "UpdateUserByRole")]
        public IActionResult UpdateUserByRole(string oldRole, string newRole, User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest();
                }

                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = @"UPDATE ""User"" 
                                        SET ""FirstName"" = @FirstName, ""LastName"" = @LastName, ""Role"" = @NewRole
                                        WHERE ""Role"" = @OldRole";

                        // Dodavanje parametara
                        command.Parameters.AddWithValue("@FirstName", user.FirstName);
                        command.Parameters.AddWithValue("@LastName", user.LastName);
                        command.Parameters.AddWithValue("@NewRole", newRole);
                        command.Parameters.AddWithValue("@OldRole", oldRole);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            return NotFound($"No users with Role = {oldRole} found.");
                        }
                    }
                }

                return Ok($"Users with Role = {oldRole} updated successfully to new Role = {newRole}.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost(Name = "PostUser")]
        public IActionResult PostUser(User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest();
                }

                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = @"INSERT INTO ""User"" (""FirstName"", ""LastName"", ""Role"") 
                                        VALUES (@FirstName, @LastName, @Role)";

                        // Dodavanje parametara
                        command.Parameters.AddWithValue("@FirstName", user.FirstName);
                        command.Parameters.AddWithValue("@LastName", user.LastName);
                        command.Parameters.AddWithValue("@Role", user.Role);

                        command.ExecuteNonQuery();
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

   




}














