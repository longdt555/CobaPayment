using Common;
using Helpers;
using IntergatedBaoKimPayment.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Models.Payment;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
using ActionNameAttribute = System.Web.Mvc.ActionNameAttribute;
using ActionResult = System.Web.Mvc.ActionResult;
using Controller = System.Web.Mvc.Controller;
using HttpGetAttribute = System.Web.Mvc.HttpGetAttribute;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;

namespace IntergatedBaoKimPayment.Controllers
{
    public class BaoKimPaymentController : BaseController
    {
        // GET: Payment
        public ActionResult Index()
        {
            var processPaymentModel = new ProcessPaymentModel();
            GenerateOrderGuid(processPaymentModel);
            BaoKimPaymentViewModel baoKimPayment_vm = new BaoKimPaymentViewModel();
            baoKimPayment_vm.bankPaymentModel = GetBankPaymentListData().Result;
            baoKimPayment_vm.orderParamModel.mrc_order_id = processPaymentModel.OrderGUID.ToString();
            return View(baoKimPayment_vm);
        }
        #region get baokim's order detail by mrc_order_id
        [HttpGet]
        protected virtual async Task<OrderModel<OrderModelDetail>> GetOrderDetailFromBaoKim(string mrc_order_id)
        {
            OrderModel<OrderModelDetail> orderDetailModel = new OrderModel<OrderModelDetail>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Constant.proHost);
                    client.DefaultRequestHeaders.Add("Accept-Language", "vi");
                    client.DefaultRequestHeaders.Add("jwt", String.Format(@"Bearer {0}", FunctionHelpers.GenerateJwtToken()));
                    HttpResponseMessage response = await client.GetAsync(Constant.orderDetailApi + "?mrc_order_id=" + mrc_order_id.ToString());
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        if (result != null)
                        {
                            orderDetailModel = JsonConvert.DeserializeObject<OrderModel<OrderModelDetail>>(result);
                            return orderDetailModel;
                        }
                    }
                }
                return new OrderModel<OrderModelDetail>();
            }
            catch (Exception ex)
            {
                return new OrderModel<OrderModelDetail>();
            }
        }

        #endregion
        #region
        public virtual async Task<OrderModel<OrderModelDetail>> CancelOrder(string mrc_order_id)
        {
            OrderModel<OrderModelDetail> cancelOrderModel = new OrderModel<OrderModelDetail>();
            var orderDetailModel = await GetOrderDetailFromBaoKim(mrc_order_id);
            if (orderDetailModel.code == 0)
            {
                var values = new Dictionary<string, string>();
                values.Add("id", orderDetailModel.data.id.ToString());
                var content = new FormUrlEncodedContent(values);
                try
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(Constant.proHost);
                        client.DefaultRequestHeaders.Add("Accept-Language", "vi");
                        client.DefaultRequestHeaders.Add("jwt", String.Format(@"Bearer {0}", FunctionHelpers.GenerateJwtToken()));
                        HttpResponseMessage response = await client.PostAsync(Constant.cancelOrderApi, content);
                        if (response.IsSuccessStatusCode)
                        {
                            var responseString = await response.Content.ReadAsStringAsync();
                        }
                    }
                }
                catch
                {

                }
                return new OrderModel<OrderModelDetail>();
            }
            else
            {
                return new OrderModel<OrderModelDetail>();
            }
        }
        #endregion
        #region SendOrder
        [HttpPost, ActionName("SendOrder")]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult> SendOrder(OrderParamModel model)
        {
            var isMatch = false;
            var orderDetailModel = await GetOrderDetailFromBaoKim(model.mrc_order_id);
            if (orderDetailModel.code == 0 && orderDetailModel.data != null)
            {
                if (double.Parse(orderDetailModel.data.total_amount, CultureInfo.InvariantCulture) != model.total_amount)
                {
                    isMatch = false;
                }
                else if (!orderDetailModel.data.description.ToLower().Equals(model.description.ToLower()))
                {
                    isMatch = false;
                }
                else if (!orderDetailModel.data.url_success.ToLower().Equals(model.url_success.ToLower()))
                {
                    isMatch = false;
                }
                else if (orderDetailModel.data.merchant_id != model.merchant_id)
                {
                    isMatch = false;
                }
                else if (!orderDetailModel.data.url_detail.ToLower().Equals(model.url_detail.ToLower().ToString()))
                {
                    isMatch = false;
                }
                else if (!orderDetailModel.data.lang.ToLower().Equals(model.lang.ToLower()))
                {
                    isMatch = false;
                }
                else if (Convert.ToInt32(orderDetailModel.data.accept_bank) != model.accept_bank)
                {
                    isMatch = false;
                }
                else if (Convert.ToInt32(orderDetailModel.data.accept_cc) != model.accept_cc)
                {
                    isMatch = false;
                }
                else if (Convert.ToInt32(orderDetailModel.data.accept_qrpay) != model.accept_qrpay)
                {
                    isMatch = false;
                }
                else if (Convert.ToInt32(orderDetailModel.data.accept_e_wallet) != model.accept_e_wallet)
                {
                    isMatch = false;
                }
                else if (int.Parse(orderDetailModel.data.bpm_id) != model.bpm_id)
                {
                    isMatch = false;
                }
                else if (!orderDetailModel.data.webhooks.ToLower().Equals(model.webhooks.ToLower()))
                {
                    isMatch = false;
                }
                else if (!orderDetailModel.data.customer_email.ToLower().Equals(model.customer_email.ToLower()))
                {
                    isMatch = false;
                }
                else if (!orderDetailModel.data.customer_name.ToLower().Equals(model.customer_name.ToLower()))
                {
                    isMatch = false;
                }
                else if (!orderDetailModel.data.customer_address.ToLower().Equals(model.customer_address.ToLower()))
                {
                    isMatch = false;
                }
                else
                {
                    return Json(new { Success = true, data = orderDetailModel.data });
                }
                await CancelOrder(model.mrc_order_id);
                //model.mrc_order_id = baokim_vm.orderParamModel.mrc_order_id;
            }
            if (!isMatch)
            {
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
                            var message = string.Empty;
                            if (responseString != null)
                            {
                                try
                                {
                                    SendOrderResponseModel<SendOrderDataModel> sendOrderResponseModel = null;
                                    sendOrderResponseModel = JsonConvert.DeserializeObject<SendOrderResponseModel<SendOrderDataModel>>(responseString);
                                    if (sendOrderResponseModel.message != null) //Lỗi validate dữ liệu/tham số
                                    {
                                        if (!string.IsNullOrEmpty(FunctionHelpers.GenerateErrorMsg(sendOrderResponseModel.message.total_amount)))
                                        {
                                            message = FunctionHelpers.GenerateErrorMsg("Tổng số tiền");
                                        }
                                        else if (!string.IsNullOrEmpty(FunctionHelpers.GenerateErrorMsg(sendOrderResponseModel.message.customer_phone)))
                                        {
                                            message = FunctionHelpers.GenerateErrorMsg("Số điện thoại");
                                        }
                                        else if (!string.IsNullOrEmpty(FunctionHelpers.GenerateErrorMsg(sendOrderResponseModel.message.customer_email)))
                                        {
                                            message = FunctionHelpers.GenerateErrorMsg("Email");
                                        }
                                        else if (!string.IsNullOrEmpty(FunctionHelpers.GenerateErrorMsg(sendOrderResponseModel.message.mrc_order_id)))
                                        {
                                            message = FunctionHelpers.GenerateErrorMsg("Mã đơn hàng");
                                        }
                                        else
                                        {
                                            message += FunctionHelpers.GenerateErrorMsg("");
                                        }
                                    }
                                    if (!string.IsNullOrEmpty(message))
                                    {
                                        return Json(new { Success = false, Message = message });
                                    }
                                }
                                catch
                                {
                                    SendOrderResponseModelv2<SendOrderDataModel> sendOrderResponseModel = null;
                                    sendOrderResponseModel = JsonConvert.DeserializeObject<SendOrderResponseModelv2<SendOrderDataModel>>(responseString);
                                    if (sendOrderResponseModel.data != null)
                                    {
                                        return Json(new { Success = true, data = sendOrderResponseModel.data });
                                    }
                                    return Json(new
                                    {
                                        Success = false,
                                        Message = "Số tiền / total_amount vượt quá giới hạn 30.000 VNĐ cho mỗi lần xác thực đối với website chưa được xác thực"
                                    });
                                }
                            }
                        }
                    }
                    return Json(new { Success = false, Message = "Có lỗi xẩy ra vui lòng thử lại." });
                }
                catch
                {
                    return Json(new { Success = false, Message = "Có lỗi xẩy ra vui lòng thử lại." });
                }
            }
            return Json(new { Success = false, Message = "Có lỗi xẩy ra vui lòng thử lại." });
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