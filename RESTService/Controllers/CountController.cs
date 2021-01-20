using Microsoft.AspNetCore.Mvc;

namespace RESTService.Controllers
{
    public class CountController : Controller
    {
        private static long _count;

        public IActionResult Count(ContadorRequest request)
        {
            var obj = new RESTCommon.ContadorReply
            {
                Message = $"Name: {request.Name}, Count: {_count++}"
            };

            return Ok(obj);
        }
    }
}