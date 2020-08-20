using IntergatedBaoKimPayment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntergatedBaoKimPayment.ViewModels
{
    public class BaoKimPaymentViewModel
    {
        public BaoKimPaymentViewModel()
        {
            bankPaymentModel = new BankModel<BankPaymentDetailModel>();
        }
        public BankModel<BankPaymentDetailModel> bankPaymentModel = new BankModel<BankPaymentDetailModel>();
    }
}