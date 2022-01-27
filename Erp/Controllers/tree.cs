using Microsoft.AspNetCore.Mvc;
using Erp.Models;

namespace  Erp.Controllers
{
    public partial class TreeViewController : Controller
    {

        public IActionResult Template()
        {
            ViewBag.data = new TreeviewTemplate().getTreeviewTemplate();
            return View();
        }
    }
}

