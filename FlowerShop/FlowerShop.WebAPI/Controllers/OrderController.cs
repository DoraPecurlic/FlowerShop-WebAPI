using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FlowerShop.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        
        private static List<Order> orders = new List<Order>
        {
            new Order("Rose", 5, "Bouquet"),
            new Order("Tulips", 7, "Flower Basket"),
            new Order("Roses", 10, "Flower Box"),
        };

        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }


        [HttpPost(Name = "Order")]
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
            if (order.TypeOfFlower == null || order.OrderType == null || order.Quantity <= 0)
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
                    displayOrders.Add($"Flower type: {order.TypeOfFlower}, Quantity: {order.Quantity}, Order type: {order.OrderType}");
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
                    if (orders[i].TypeOfFlower == order.TypeOfFlower && orders[i].Quantity == order.Quantity && orders[i].OrderType == order.OrderType)
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
                    if (orders[i].TypeOfFlower == order.TypeOfFlower && orders[i].Quantity == order.Quantity && orders[i].OrderType == order.OrderType)
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

        }



    }
}
