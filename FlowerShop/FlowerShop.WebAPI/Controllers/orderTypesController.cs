using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FlowerShop.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderTypesController : ControllerBase
    {

        private static List<string> orderTypes = new List<string>
        {
            "Bouquet", "Flower Box", "Flower Basket", "Wedding Floral Arrangement", "Table Floral Arrangement"
        };

        private readonly ILogger<orderTypesController> _logger;

        [HttpGet(Name = "seeOrderTypes")]
        public IActionResult seeOrderTypes()
        {

            try
            {
                return Ok(orderTypes.ToArray());
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpPost(Name = "AddNewOrderType")]
        public HttpResponseMessage AddNewOrderType(string newType)
        {
            if (newType == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("null exception") };
            }
            try
            {
               
               orderTypes.Add(newType);
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("Succesfully added new order type.")
                };

            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred while placing new order type")
                };
            }

        }

        [HttpDelete(Name = "DeleteType")]
        public IActionResult DeleteType(string typeToDelete)
        {

            if (!orderTypes.Contains(typeToDelete))
            {
                return StatusCode(400, "Summary doesnt exists. Cannot delet summary that does not exist.");
            }

            orderTypes.RemoveAt(orderTypes.IndexOf(typeToDelete));
            return StatusCode(200, "Order type successfully deleted.");
        }

        [HttpPut(Name = "UpdateOrderType")]
        public HttpResponseMessage UpdateOrderType(string existingType, string updatedType)
        {
            try
            {
                int index = orderTypes.IndexOf(existingType);
                if (index == -1)
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent($"Order type '{existingType}' not found")
                    };
                }
               
                orderTypes[index] = updatedType;
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent($"Order type '{existingType}' updated to '{updatedType}'")
                };

            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred: {ex.Message}");
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred while updating the order type")
                };

            }
        }


    }
}
