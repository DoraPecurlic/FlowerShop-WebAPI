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
       

        public OrderController()
        {
        }



        
        [HttpPost(Name = "Order")]
        public IActionResult Order([FromBody] Order order)
        {
            try
            {

                string connectionString = WebApplication.Create().Configuration.GetConnectionString("DefaultConnection");



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

        /* [HttpPost(Name = "Order")]
         public HttpResponseMessage Order([FromBody] Order order)
         {
             if (order == null)
             {
                 return new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("Order data cannot be null.") };
             }
             try
             {
                 // Business handling
                 bool isProcessed = ProcessOrder(order);
                 if (isProcessed == false)
                 {
                     return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                     {
                         Content = new StringContent("Order processing faild.")
                     };
                 }

                 orders.Add(order);
                 return new HttpResponseMessage(HttpStatusCode.OK)
                 {
                     Content = new StringContent("Succesfully ordered! Thank you for ordering .")
                 };

             }
             catch (Exception ex)
             {
                 _logger.LogError($"Exception occurred: {ex.Message}");
                 return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                 {
                     Content = new StringContent("An error occurred while placing the order")
                 };
             }

         }

         private bool ProcessOrder(Order order)
         {
             if (order.FlowerType == null || order.OrderTypeId == null || order.Quantity <= 0)
             {
                 return false;
             }
             return true;
         }



         [HttpGet(Name = "SeeOrders")]
         public IActionResult SeeOrders()
         {

             try
             {
                 List<string> displayOrders = new List<string>();
                 foreach (var order in orders)
                 {
                     displayOrders.Add($"Flower type: {order.FlowerType}, Quantity: {order.Quantity}, Order type: {order.OrderTypeId}");
                 }
                 return Ok(displayOrders.ToArray());
             }
             catch
             {
                 return BadRequest();
             }

         }


         [HttpDelete(Name = "DeleteOrder")]
         public HttpResponseMessage DeleteOrder([FromBody] Order order)
         {
             if (order == null)
             {
                 return new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("Order data cannot be null.") };
             }
             try
             {
                 // Business handling
                 bool isProcessed = ProcessOrder(order);
                 if (isProcessed == false)
                 {
                     return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                     {
                         Content = new StringContent("Order deleting faild.")
                     };
                 }

                 int indexToRemove = -1;
                 for (int i = 0; i < orders.Count; i++)
                 {
                     if (orders[i].FlowerType == order.FlowerType && orders[i].Quantity == order.Quantity && orders[i].OrderTypeId == order.OrderTypeId)
                     {
                         indexToRemove = i;
                         break;
                     }
                 }

                 if (indexToRemove != -1)
                 {
                     orders.RemoveAt(indexToRemove);
                     return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("Order deleted successfully.") };
                 }
                 return new HttpResponseMessage(HttpStatusCode.NotFound);

             }
             catch (Exception ex)
             {
                 _logger.LogError($"Exception occurred: {ex.Message}");
                 return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                 {
                     Content = new StringContent("An error occurred while deleting the order.")
                 };
             }

         }


         [HttpPut(Name = "UpdateOrder")]
         public HttpResponseMessage UpdateOrder([FromBody] Order order, [FromQuery] int newQuantity)
         {
             if (order == null)
             {
                 return new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("Order data cannot be null.") };
             }
             try
             {
                 // Business handling
                 bool isProcessed = ProcessOrder(order);
                 if (isProcessed == false)
                 {
                     return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                     {
                         Content = new StringContent("Order updating faild.")
                     };
                 }


                 for (int i = 0; i < orders.Count; i++)
                 {
                     if (orders[i].FlowerType == order.FlowerType && orders[i].Quantity == order.Quantity && orders[i].OrderTypeId == order.OrderTypeId)
                     {
                         orders[i].Quantity = newQuantity;
                         return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("Order updated successfully.") };
                     }
                 }
                 return new HttpResponseMessage(HttpStatusCode.NotFound);

             }
             catch (Exception ex)
             {
                 _logger.LogError($"Exception occurred: {ex.Message}");
                 return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                 {
                     Content = new StringContent("An error occurred while deleting the order.")
                 };
             }

         }*/



    }
}
