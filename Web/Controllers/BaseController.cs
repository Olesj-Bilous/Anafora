using AnaforaData.Model;
using AnaforaWeb.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnaforaWeb.Controllers
{
    public abstract class Controller<T> : Controller where T : IGlobalModel
    {
        public Controller()
        {
        }

        private static string _type = typeof(T).ToString();

        //[ContentAuthorize(_type, AnaforaData.Utils.Enums.Permissions.Read)]
        [HttpGet]
        public IActionResult Get(Guid guid)
        {
            return View();
        }
    }
}
