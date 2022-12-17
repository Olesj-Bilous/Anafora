using AnaforaData.Context;
using AnaforaData.Model.Global;
using AnaforaData.Utils.Enums;
using AnaforaWeb.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnaforaWeb.Controllers
{
    public class EventController : Controller<Event>
    {
        private DataContext _context;

        public EventController(DataContext context)
        {
            _context = context;
        }

        //[ContentAuthorize(typeof(Event), Permissions.Read)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
