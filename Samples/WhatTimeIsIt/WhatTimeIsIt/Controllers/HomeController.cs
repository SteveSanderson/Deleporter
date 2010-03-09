using System.Web.Mvc;
using WhatTimeIsIt.Services;

namespace WhatTimeIsIt.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDateProvider _dateProvider;
        public HomeController(IDateProvider dateProvider)
        {
            _dateProvider = dateProvider;
        }

        public ActionResult Index()
        {
            return View(_dateProvider.CurrentDate);
        }
    }
}
