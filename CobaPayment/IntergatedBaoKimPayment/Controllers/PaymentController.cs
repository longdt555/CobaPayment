﻿using CobastockPayment;
using CobastockPayment.Common;
using IntergatedBaoKimPayment.Models;
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

        public ActionResult Cart()
        {
            return View();
        }
        private static String GetParamPost(OrderParamModel order)
        {

            String request = "";
            request += "&mrc_order_id=" + order.mrc_order_id;
            request += "&total_amount=" + order.total_amount;
            request += "&description=" + order.description;
            request += "&merchant_id=" + order.merchant_id;
            request += "&url_detail=" + order.url_detail;
            request += "&url_success=" + order.url_success;
            request += "&url_detail=" + order.url_detail;
            request += "&lang=" + order.lang;
            request += "&bpm_id=" + order.bpm_id;
            request += "&accept_bank=" + order.accept_bank;
            request += "&accept_cc=" + order.accept_cc;
            request += "&accept_qrpay=" + order.accept_qrpay;
            request += "&accept_e_wallet=" + order.accept_e_wallet;
            request += "&webhooks=" + order.webhooks;
            request += "&customer_email=" + order.customer_email;
            request += "&customer_phone=" + order.customer_phone;
            request += "&customer_name=" + order.customer_name;
            request += "&customer_address=" + order.customer_address;
            return request;
        }
        private static String GetParamPost(BankParamModel bank)
        {

            String request = "";
            request += "&lb_available=" + bank.lb_available;
            request += "&offset=" + bank.offset;
            request += "&IoHiR7zZnumiRHT9=" + bank.limit;
            return request;
        }

        public async Task<Uri> CreateProductAsync(OrderParamModel model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(proHost);
                client.DefaultRequestHeaders.Add("jwt", FunctionHelpers.ZoomToken(model));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpContent content = new StringContent(model.ToString(), Encoding.UTF8, "application/json");
                //var response = client.PostAsJsonAsync(sendOrderApi, GetParamPost(order));
                HttpResponseMessage response = await client.PostAsJsonAsync(sendOrderApi, GetParamPost(model));
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
        }
        #region GetBankList: get list of bank from BAOKIM api
        public async Task<BankModel<BankPaymentDetailModel>> GetBankList()
        {
            BankModel<BankPaymentDetailModel> bankPaymentList = null;
            using (var client = new HttpClient())
            {
                // New code:
                client.BaseAddress = new Uri(proHost);
                client.DefaultRequestHeaders.Add("jwt", FunctionHelpers.GenerateJwtToken());
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // New code:
                HttpResponseMessage response = await client.GetAsync(bankPayApi);
                if (response.IsSuccessStatusCode)
                {
                    bankPaymentList = await response.Content.ReadAsAsync<BankModel<BankPaymentDetailModel>>();
                }
            }
            return bankPaymentList;
        }
        #endregion
    }
}