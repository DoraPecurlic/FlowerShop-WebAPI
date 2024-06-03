using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Microsoft.Extensions.Configuration;
using FlowerShop.WebAPI.Models;

namespace FlowerShop.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class OrderController : ControllerBase
    {
        private string connectionString = WebApplication.Create().Configuration.GetConnectionString("DefaultConnection");


        [HttpPost(Name = "Order")]
        public IActionResult Order([FromBody] Order order)
        {
            try
            {

                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = @"
                            INSERT INTO ""Order"" (""FlowerType"", ""Quantity"", ""OrderTypeId"", ""IsActive"", ""DateCreated"", ""DateUpdated"", ""CreatedByUserId"", ""UpdatedByUserId"")
                            VALUES (@FlowerType, @Quantity, @OrderTypeId, @IsActive, @DateCreated, @DateUpdated, @CreatedByUserId, @UpdatedByUserId)";

                        command.Parameters.AddWithValue("FlowerType", order.FlowerType);
                        command.Parameters.AddWithValue("Quantity", order.Quantity);
                        command.Parameters.AddWithValue("OrderTypeId", order.OrderTypeId);
                        command.Parameters.AddWithValue("IsActive", order.IsActive);
                        command.Parameters.AddWithValue("DateCreated", order.DateCreated);
                        command.Parameters.AddWithValue("DateUpdated", order.DateUpdated);
                        command.Parameters.AddWithValue("CreatedByUserId", order.CreatedByUserId);
                        command.Parameters.AddWithValue("UpdatedByUserId", order.UpdatedByUserId);

                        command.ExecuteNonQuery();
                    }
                   
                }
                if(order == null)
                {
                    return BadRequest();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connection failed: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        [HttpGet(Name = "GetOrders")]
        public IActionResult SeeOrders()
        {
            try
            {
                List<string> orders = new List<string>();
                using (var connection = new NpgsqlConnection(connectionString))
                {
                   
                    connection.Open();

                    using (var command = new NpgsqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = @"SELECT ""User"".""FirstName"", ""User"".""LastName"", ""OrderType"".""Type"", ""Order"".""FlowerType"", ""Order"".""Quantity"" FROM  ""Order""  " + 
                           @"INNER JOIN ""User"" ON ""Order"".""CreatedByUserId"" = ""User"".""Id""" +
                            @"INNER JOIN  ""OrderType"" ON ""Order"".""OrderTypeId"" = ""OrderType"".""Id"" ";

                        using (var reader = command.ExecuteReader()) {
                            if (reader.HasRows) {
                                while (reader.Read()) { 
                                    string firstName = reader.GetString(0); //dohvaca vrijednost prvog stupca i vraca u string
                                    string lastName = reader.GetString(1);
                                    string orderType = reader.GetString(2);
                                    string flowerType = reader.GetString(3);
                                    int quantity = reader.GetInt32(4);

                                    //formatiranje
                                    string orderDetails = $"Custumer: {firstName} {lastName}, Order Type: {orderType}, flowers: {flowerType}, Number of Flowers: {quantity}";

                                    orders.Add(orderDetails);
                                }

                            }
                        }
                    }

                }

                    return Ok(orders.ToArray());
            }
            catch( Exception ex) {
            

                return BadRequest(ex.Message);
            }

          

        }

        [HttpDelete(Name = "DeleteOrder")]
        public IActionResult DeleteOrder(int id)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = @"DELETE FROM ""Order"" WHERE ""Id"" = @Id";
                        command.Parameters.AddWithValue("@Id", id);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            return NotFound($"No order with Id = {id} found.");
                        }
                    }
                }

                return Ok("Order successfully deleted.");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }






          


        }
}
