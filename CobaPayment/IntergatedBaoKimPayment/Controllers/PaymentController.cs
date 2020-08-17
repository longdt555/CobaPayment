using CobastockPayment;
using CobastockPayment.Common;
using IntergatedBaoKimPayment.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Xml;

namespace IntergatedBaoKimPayment.Controllers
{
    public class PaymentController : Controller
    {
        // request host
        private const string devHost = "https://sandbox-api.baokim.vn/payment/";
        private const string proHost = "https://api.baokim.vn/payment/";
        // get bank api
        private const string bankApi = "api/v4/bank/list";
        // get bank pay api
        private const string bankPayApi = "api/v4/bpm/list";
        // send order api
        private const string sendOrderApi = "api/v4/order/send";
        // order detail api
        private const string orderDetailApi = "api/v4/order/detail";
        // order list detail api
        private const string orderListDetailApi = "api/v4/order/list";
        // cancel order api
        private const string cancelOrderApi = "api/v4/order/cancel";
        // account detail
        private const string accountDetailApi = "api/v4/account/detail";


        // GET: Payment
        public ActionResult Index()
        {
            return View();
        }

        private static String GetQueryFromParams()
        {

            String request = "";
            return request;
        }

        #region SendOrder
        public async Task<ActionResult> SendOrder([Microsoft.AspNetCore.Mvc.FromBody] OrderParamModel model)
        {
            SendOrderResponseModel<SendOrderDataModel> sendOrderResponseModel = null;
            var query = new Dictionary<string, string> { };
            query.Add("mrc_order_id", model.mrc_order_id);
            query.Add("total_amount", model.total_amount.ToString());
            query.Add("description", model.description);
            query.Add("url_success", model.url_success);
            query.Add("merchant_id", model.merchant_id.ToString());
            query.Add("url_detail", model.url_detail);
            query.Add("lang", model.lang);
            query.Add("bpm_id", model.bpm_id.ToString());
            query.Add("accept_bank", model.accept_bank.ToString());
            query.Add("accept_cc", model.accept_cc.ToString());
            query.Add("accept_qrpay", model.accept_qrpay.ToString());
            query.Add("accept_e_wallet", model.accept_e_wallet.ToString());
            query.Add("webhooks", model.webhooks.ToString());
            query.Add("customer_email", model.customer_email.ToString());
            query.Add("customer_phone", model.customer_phone.ToString());
            query.Add("customer_name", model.customer_name.ToString());
            query.Add("customer_address", model.customer_address.ToString());

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(proHost);
                    client.DefaultRequestHeaders.Add("Accept-Language", "vi");
                    client.DefaultRequestHeaders.Add("jwt", String.Format(@"Bearer {0}", FunctionHelpers.GenerateJwtToken()));
                    HttpResponseMessage response = await client.PostAsJsonAsync(sendOrderApi, model);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        if (responseString != null)
                        {
                            sendOrderResponseModel = JsonConvert.DeserializeObject<SendOrderResponseModel<SendOrderDataModel>>(responseString);
                            return Json(new { data = sendOrderResponseModel, Message = "Thành công." });
                        }
                    }
                }
                return Json(new { data = new SendOrderResponseModel<SendOrderDataModel>(), Message = "Có lỗi xẩy ra." });
            }
            catch (Exception ex)
            {
                return Json(new { data = new SendOrderResponseModel<SendOrderDataModel>(), Message = ex.ToString() });
            }
        }
        #endregion
        #region GetBankList: get list of bank from BAOKIM api
        public async Task<ActionResult> OrderDetail(string orderId, string merchantOrderid)
        {
            BankModel<BankPaymentDetailModel> bankPaymentList = null;
            var query = string.Empty;
            if (!string.IsNullOrEmpty(orderId) && !string.IsNullOrEmpty(merchantOrderid))
            {
                query = "id=" + orderId + "&mrc_order_id=" + merchantOrderid;
            }
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(devHost);
                    client.DefaultRequestHeaders.Add("jwt", FunctionHelpers.GenerateJwtToken());
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync(orderDetailApi + "?" + query);
                    if (response.IsSuccessStatusCode)
                    {
                        bankPaymentList = await response.Content.ReadAsAsync<BankModel<BankPaymentDetailModel>>();
                        return Json(new { data = bankPaymentList, Message = "Thành công." });
                    }
                }
                return Json(new { data = new BankModel<BankPaymentDetailModel>(), Message = "Có lỗi xẩy ra." });
            }
            catch (Exception ex)
            {
                return Json(new { data = new BankModel<BankPaymentDetailModel>(), Message = ex.ToString() });
            }
        }
        #endregion

        #region GetBankList: get list of bank from BAOKIM api
        public async Task<ActionResult> GetBankList()
        {
            BankModel<BankPaymentDetailModel> bankPaymentList = null;
            try
            {
                using (var client = new HttpClient())
                {
                    // New code:
                    client.BaseAddress = new Uri(proHost);
                    client.DefaultRequestHeaders.Add("jwt", FunctionHelpers.GenerateJwtToken());
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    // New code:
                    HttpResponseMessage response = await client.GetAsync(bankPayApi).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        bankPaymentList = await response.Content.ReadAsAsync<BankModel<BankPaymentDetailModel>>();
                        return Json(new { data = bankPaymentList, Message = "Thành công." });
                    }
                }
                return Json(new { data = new BankModel<BankPaymentDetailModel>(), Message = "Có lỗi xẩy ra." });
            }
            catch (Exception ex)
            {
                return Json(new { data = new BankModel<BankPaymentDetailModel>(), Message = ex.ToString() });
            }
        }
        #endregion
    }
}