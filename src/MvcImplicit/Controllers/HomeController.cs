using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace MvcImplicit.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secure()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> CallApi()
        {
            var token = User.FindFirst("access_token").Value;

            var client = new HttpClient();
            client.SetBearerToken(token);

            var response = await client.GetStringAsync("http://localhost:3860/identity");
            ViewBag.Json = JArray.Parse(response).ToString();

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("cookies");
            return Redirect("~/");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
