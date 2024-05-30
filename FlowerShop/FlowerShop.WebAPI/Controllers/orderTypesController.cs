using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FlowerShop.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class orderTypesController : ControllerBase
    {

        private static List<string> orderTypes = new List<string>
        {
            "Bouquet", "Flower Box", "Flower Basket", "Wedding Floral Arrangement", "Table Floral Arrangement"
        };


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




    }
}
