using Microsoft.AspNetCore.Mvc;

namespace BookstoreWeb.Controllers
{
    public class ErrorController : Controller
    {
        public ViewResult Error() => View();
    }
}
