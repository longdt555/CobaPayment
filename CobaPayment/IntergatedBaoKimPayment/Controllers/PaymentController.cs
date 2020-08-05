using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntergatedBaoKimPayment.Controllers
{
    public class PaymentController : Controller
    {

        private const string URL = "https://api.baokim.vn/";
        // GET: Payment
        public ActionResult Index()
        {
            return View();
        }
    }
}