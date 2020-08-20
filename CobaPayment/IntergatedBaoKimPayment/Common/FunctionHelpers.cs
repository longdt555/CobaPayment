using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CobastockPayment.Common
{
    public static class FunctionHelpers
    {
        // APi_KEY + API_SECRET
        //Production
        public const string PRO_API_KEY = "JRCqv5kLw82Hz515RqbwaLEpi96ufrRR";
        public const string PRO_API_SECRET = "aTfL6YZSOWO68KltB8ardUfYZTAzC9g3";
        //devlopment
        public const string DEV_API_KEY = "a18ff78e7a9e44f38de372e093d87ca1";
        public const string DEV_API_SECRET = "9623ac03057e433f95d86cf4f3bef5cc";

        public static string GenerateJwtToken(int expireMinutes = 1)
        {
            var symmetricKey = Encoding.ASCII.GetBytes(PRO_API_SECRET);
            var tokenHandler = new JwtSecurityTokenHandler();
            var generator = new Random();
            Byte[] b = new Byte[32];
            generator.NextBytes(b);
            var tokenId = Convert.ToBase64String(b);

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]{
                    new Claim("iss", PRO_API_KEY),
                    new Claim("jti", tokenId)
                }),

                Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);
            return token;
        }
        public static string ZoomToken(OrderParamModel model = null)
        {
            // Token will be good for 20 minutes
            DateTime Expiry = DateTime.UtcNow.AddMinutes(20);

            int ts = (int)(Expiry - new DateTime(1970, 1, 1)).TotalSeconds;

            var generator = new Random();
            Byte[] b = new Byte[32];
            generator.NextBytes(b);
            var tokenId = Convert.ToBase64String(b);

            // Create Security key  using public key above:
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(PRO_API_SECRET));

            // length should be >256b
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            //Finally create a Token
            var header = new JwtHeader(credentials);
            var payload = new JwtPayload();
            if (model != null)
            {
                //Zoom Required Payload
                payload = new JwtPayload
                {
                    { "iss", PRO_API_KEY},
                    { "exp", ts },
                    { "jti", tokenId },
                    { "form_params", new OrderParamModel {
                    mrc_order_id = model.mrc_order_id,
                    total_amount = model.total_amount,
                    description = model.description,
                    url_success = model.url_success,
                    merchant_id = model.merchant_id,
                    url_detail = model.url_detail,
                    lang = model.lang,
                    bpm_id = model.bpm_id,
                    accept_bank = model.accept_bank,
                    accept_cc = model.accept_cc,
                    accept_qrpay = model.accept_qrpay,
                    accept_e_wallet = model.accept_e_wallet,
                    webhooks = model.webhooks,
                    customer_email = model.customer_email,
                    customer_phone = model.customer_phone,
                    customer_name = model.customer_name,
                    customer_address = model.customer_address
                       }
                    }
                };
            }
            else
            {
                payload = new JwtPayload
                {
                    { "iss", PRO_API_KEY},
                    { "exp", ts },
                    { "jti", tokenId }
                };
            }

            var secToken = new JwtSecurityToken(header, payload);
            var handler = new JwtSecurityTokenHandler();

            // Token to String so you can use it in your client
            var tokenString = handler.WriteToken(secToken);

            return tokenString;
        }
        public static string GetMD5Hash(String input)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();

            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }

            String md5String = s.ToString();
            return md5String;
        }

        public static String GetErrorMessage(string _ErrorCode)
        {
            String _Message = "";
            switch (_ErrorCode)
            {
                case "00":
                    _Message = "Giao dịch thành công";
                    break;
                case "01":
                    _Message = "Lỗi hệ thống";
                    break;
                case "02":
                    _Message = "Lỗi validate dữ liệu/tham số";
                    break;
                case "03":
                    _Message = "Lỗi không tìm thấy đối tượng (tài khoản/giao dịch/đơn hàng...)";
                    break;
                case "04":
                    _Message = "Lỗi tài khoản bị khóa";
                    break;
                case "05":
                    _Message = "Lỗi không được phép thực hiện giao dịch (đăng nhập, xác thực 2FA lỗi)";
                    break;
                case "06":
                    _Message = "Lỗi số tiền giao dịch không chính xác";
                    break;
                case "07":
                    _Message = "Lỗi giao dịch lặp (vd thanh toán 2 lần...)";
                    break;
                case "08":
                    _Message = "Lỗi hệ thống nội bộ";
                    break;
                case "09":
                    _Message = "Lỗi số dư tài khoản không đủ thực hiện giao dịch";
                    break;
                case "10":
                    _Message = "Lỗi số tiền giao dịch vượt quá hạn mức ngày";
                    break;
                case "11":
                    _Message = "Lỗi xác minh giao dịch";
                    break;
                case "12":
                    _Message = "Lỗi cấu hình tính phí";
                    break;
                case "13":
                    _Message = "Lỗi không tìm thấy tài khoản giao dịch";
                    break;
                case "14":
                    _Message = "Lỗi số tiền quá nhỏ so với hạn mức";
                    break;
                case "15":
                    _Message = "Lỗi số tiền quá lớn so với hạn mức";
                    break;
                case "16":
                    _Message = "Lỗi user chưa xác thực tài khoản";
                    break;
                case "17":
                    _Message = "Lỗi trạng thái giao dịch chưa hoàn thành";
                    break;
                case "18":
                    _Message = "Lỗi hoàn tiền lặp(khi thực hiện hoàn tiền)";
                    break;
                case "19":
                    _Message = "Tài khoản Ngân hàng đã tồn tại trên hệ thống";
                    break;
                case "20":
                    _Message = "Không tìm thấy thẻ Ngân hàng";
                    break;
                case "21":
                    _Message = "Lỗi chuyển tiền sang Thẻ ngân hàng";
                    break;
                case "22":
                    _Message = "Tên Tài khoản ngân hàng không trùng với tên Ví";
                    break;
                case "23":
                    _Message = "Không tìm thấy Tài khoản Ngân hàng";
                    break;
                case "24":
                    _Message = "Lỗi khác (không xác định)";
                    break;
                case "25":
                    _Message = "Loại giao dịch không được hoàn tiền";
                    break;
                case "27":
                    _Message = "Thẻ Ngân hàng đã tồn tại trên hệ thống";
                    break;
                case "47":
                    _Message = "Liên kết ví đã tồn tại";
                    break;
                case "48":
                    _Message = "Không tìm thấy Merchant";
                    break;
                case "49":
                    _Message = "Mã xác thực liên kết không chính xác";
                    break;
                case "51":
                    _Message = "Ticket sai định dạng";
                    break;

                case "52":
                    _Message = "Token liên kết không hợp lệ";
                    break;
                case "53":
                    _Message = "Liên kết đã được xác thực. Không thể thực hiện lại";
                    break;
                case "61":
                    _Message = "Ticket đã hết hạn. Vui lòng thực hiện lại";
                    break;
                case "62":
                    _Message = "Linked code đã hết hạn. Vui lòng thực hiện lại";
                    break;
                case "63":
                    _Message = "Token Unauthorization";
                    break;
                case "180":
                    _Message = "Lỗi xác thực thẻ";
                    break;
            }
            return _Message;
        }
    }
}
