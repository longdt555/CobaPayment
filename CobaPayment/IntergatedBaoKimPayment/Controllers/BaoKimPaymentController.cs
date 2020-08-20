using CobastockPayment;
using CobastockPayment.Common;
using IntergatedBaoKimPayment.Common;
using IntergatedBaoKimPayment.Models;
using IntergatedBaoKimPayment.ViewModels;
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
    public class BaoKimPaymentController : Controller
    {
        // GET: Payment
        public ActionResult Index()
        {
            BaoKimPaymentViewModel baoKimPayment_vm = new BaoKimPaymentViewModel();
            baoKimPayment_vm.bankPaymentModel = GetBankPaymentListData().Result;
            return View(baoKimPayment_vm);
        }

        #region SendOrder
        [HttpPost]
        public async Task<ActionResult> SendOrder([Microsoft.AspNetCore.Mvc.FromBody] OrderParamModel model)
        {
            SendOrderResponseModel<SendOrderDataModel> sendOrderResponseModel = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Constant.proHost);
                    client.DefaultRequestHeaders.Add("Accept-Language", "vi");
                    client.DefaultRequestHeaders.Add("jwt", String.Format(@"Bearer {0}", FunctionHelpers.GenerateJwtToken()));
                    HttpResponseMessage response = await client.PostAsJsonAsync(Constant.sendOrderApi, model);
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
        [HttpGet]
        public async Task<ActionResult> GetBankPaymentList()
        {
            BankModel<BankPaymentDetailModel> bankPaymentList = null;
            try
            {
                using (var client = new HttpClient())
                {
                    // New code:
                    client.BaseAddress = new Uri(Constant.proHost);
                    client.DefaultRequestHeaders.Add("jwt", FunctionHelpers.GenerateJwtToken());
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    // New code:
                    HttpResponseMessage response = await client.GetAsync(Constant.bankPayApi).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        bankPaymentList = await response.Content.ReadAsAsync<BankModel<BankPaymentDetailModel>>();
                        return Json(new { data = bankPaymentList, Message = "Thành công." });
                    }
                }
                return Json(new { data = new BankModel<BankPaymentDetailModel>(), Message = "Có lỗi xẩy ra." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { data = new BankModel<BankPaymentDetailModel>(), Message = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<BankModel<BankPaymentDetailModel>> GetBankPaymentListData()
        {
            BankModel<BankPaymentDetailModel> bankPaymentList = null;
            try
            {
                using (var client = new HttpClient())
                {
                    // New code:
                    client.BaseAddress = new Uri(Constant.proHost);
                    client.DefaultRequestHeaders.Add("jwt", FunctionHelpers.GenerateJwtToken());
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    // New code:
                    HttpResponseMessage response = await client.GetAsync(Constant.bankPayApi).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        bankPaymentList = await response.Content.ReadAsAsync<BankModel<BankPaymentDetailModel>>();
                        return bankPaymentList;
                    }
                }
                return new BankModel<BankPaymentDetailModel>();
            }
            catch (Exception ex)
            {
                return new BankModel<BankPaymentDetailModel>();
            }
        }
        #endregion
    }
}