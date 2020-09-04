using Models.Payment;
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
            orderParamModel = new OrderParamModel();
        }
        public BankModel<BankPaymentDetailModel> bankPaymentModel = new BankModel<BankPaymentDetailModel>();
        public OrderParamModel orderParamModel { get; set; }
    }
}