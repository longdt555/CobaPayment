using Models.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntergatedBaoKimPayment.Controllers
{
    public class BaseController : Controller
    {
        public virtual void GenerateOrderGuid(ProcessPaymentModel model)
        {
            if (model.OrderGUID == Guid.Empty)
            {
                model.OrderGUID = Guid.NewGuid();
                model.OrderGuidGeneratedOnUtc = DateTime.UtcNow;
            }
        }

    }
}