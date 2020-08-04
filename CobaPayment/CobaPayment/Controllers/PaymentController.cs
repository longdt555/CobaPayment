using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CobastockPayment;
using CobastockPayment.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CobaPayment.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private const string URL = "https://api.baokim.vn/payment/";
        private string baokim_url = "https://www.baokim.vn/payment/customize_payment/order";
        //Mã merchant site
        private string merchant_id = "100001";    //Thay bằng mã merchant site bạn đã đăng ký trên BaoKim.vn

        //Mật khẩu bảo mật
        private static string secure_pass = "aTfL6YZSOWO68KltB8ardUfYZTAzC9g3";    //Thay bằng mật khẩu giao tiếp giữa website của bạn với BaoKim.vn


        [HttpGet]
        public OrderInfoModel Get()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URL);
                client.DefaultRequestHeaders.Add("jwt", FunctionHelpers.ZoomToken());
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Order detail
                var values = new OrderInfoModel();
                values.mrc_order_id = "vhKAByfq54SfrQWO";
                values.total_amount = 6;
                values.description = "0BnT3BExltFwUTkm";
                values.url_success = "i0Q8bd9BCkimqopn";
                values.merchant_id = 13;
                values.url_detail = "zjb2yg3JWUMEpg9o";
                values.lang = "RlVgjhJyGpJ3oURy";
                values.bpm_id = 12;
                values.accept_bank = true;
                values.accept_cc = true;
                values.accept_qrpay = true;
                values.accept_e_wallet = true;
                values.webhooks = "9sduSJffCxfSOIwT";
                values.customer_email = "vHSVGy4ZSOhuzSX7";
                values.customer_phone = "ilxb6faR0hCheg8g";
                values.customer_name = "08unBjKqQ094wJq8";
                values.customer_address = "6MQOmAgJ9DX6XXOe";
                HttpContent content = new StringContent(values.ToString(), Encoding.UTF8, "application/json");
                var response = client.PostAsJsonAsync("api/v4/txn/detail", "mA0q6otRngVTIUZi");
                
                //var token = FunctionHelpers.GenerateJwtToken();

                //var token1 = FunctionHelpers.ZoomToken();
            }
            return new OrderInfoModel();
        }
    }
}