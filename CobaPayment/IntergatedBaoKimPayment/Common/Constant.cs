using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common
{
    public static class Constant
    {
        // APi_KEY + API_SECRET
        //Development
        public const string PRO_API_KEY = "JRCqv5kLw82Hz515RqbwaLEpi96ufrRR";
        public const string PRO_API_SECRET = "aTfL6YZSOWO68KltB8ardUfYZTAzC9g3";
        //Production
        public const string PRO_API_KEY_EG = "STR9VqmTxZ2A90NX4Hy3XfrMHQMilqWX";
        public const string PRO_API_SECRET_EG = "6kl61H9jSIL8JKzb5qkBokIG9fwEdMWt";
        // request host
        public const string devHost = "https://sandbox-api.baokim.vn/payment/";
        public const string proHost = "https://api.baokim.vn/payment/";
        // get bank api
        public const string bankApi = "api/v4/bank/list";
        // get bank pay api
        public const string bankPayApi = "api/v4/bpm/list";
        // send order api
        public const string sendOrderApi = "api/v4/order/send";
        // order detail api
        public const string orderDetailApi = "api/v4/order/detail";
        // order list detail api
        public const string orderListDetailApi = "api/v4/order/list";
        // cancel order api
        public const string cancelOrderApi = "api/v4/order/cancel";
        // account detail
        public const string accountDetailApi = "api/v4/account/detail";


    }
    public static class BankPaymentMethod
    {
        public static int BaoKim = 0;
        public static int ATM = 1;
        public static int VISA_Master = 2;
        public static int QRCode = 14;
        public static int QREWallet = 15;
    }
    public static class Currency
    {
        public const int USD = 1;
        public const int AUSTRALIAN_DOLLAR = 2;
        public const int British_Pound = 3;
        public const int CANADIAN_DOLLAR = 4;
        public const int CHINESEYUAN_REMINBI = 5;
        public const int EURO = 6;
        public const int HONGKONG_DOLLAR = 7;
        public const int JAPANESE_YEN = 8;
        public const int RUSSIAN_ROUBLE = 9;
        public const int SWEDISH_KRONA = 10;
        public const int ROMANDIAN_LEU = 11;
        public const int INDIAN_RUPEE = 12;
        public const int VIETNAM_DONG = 13;
    }
}