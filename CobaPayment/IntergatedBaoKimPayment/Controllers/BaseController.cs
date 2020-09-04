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
        public virtual void GenerateOrderGuid(OrderParamModel model)
        {
            //if (processPaymentRequest.OrderGuid == Guid.Empty)
            //{
            //    processPaymentRequest.OrderGuid = Guid.NewGuid();
            //    processPaymentRequest.OrderGuidGeneratedOnUtc = DateTime.UtcNow;
            //}
        }

    }
}