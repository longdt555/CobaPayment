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
        private const string PRO_API_KEY = "JRCqv5kLw82Hz515RqbwaLEpi96ufrRR";
        private const string PRO_API_SECRET = "aTfL6YZSOWO68KltB8ardUfYZTAzC9g3";

        private const string DEV_API_KEY = "a18ff78e7a9e44f38de372e093d87ca1";
        private const string DEV_API_SECRET = "9623ac03057e433f95d86cf4f3bef5cc";

        public static string GenerateJwtToken(int expireMinutes = 1)
        {
            var symmetricKey = Encoding.ASCII.GetBytes(DEV_API_SECRET);
            var tokenHandler = new JwtSecurityTokenHandler();
            var generator = new Random();
            Byte[] b = new Byte[32];
            generator.NextBytes(b);
            var tokenId = Convert.ToBase64String(b);

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]{
                    new Claim("iss", DEV_API_KEY),
                    new Claim("jti", tokenId)
                }),

                Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);

            var token = tokenHandler.WriteToken(securityToken);


            return token;
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

        private static String GetErrorMessage(string _ErrorCode)
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
                //
                case "13":
                    _Message = "Mã thẻ và số serial không khớp";
                    break;
                case "14":
                    _Message = "Thẻ không tồn tại";
                    break;
                case "15":
                    _Message = "Thẻ không sử dụng được";
                    break;
                case "16":
                    _Message = "Số lần tưử của thẻ vượt quá giới hạn cho phép";
                    break;
                case "17":
                    _Message = "Hệ thống Telco bị lỗi hoặc quá tải, thẻ chưa bị trừ";
                    break;
                case "18":
                    _Message = "Hệ thống Telco  bị lỗi hoặc quá tải, thẻ có thể bị trừ, cần phối hợp với nhà mạng để đối soát";
                    break;
                case "19":
                    _Message = "Kết nối NgânLượng với Telco bị lỗi, thẻ chưa bị trừ.";
                    break;
                case "20":
                    _Message = "Kết nối tới Telco thành công, thẻ bị trừ nhưng chưa cộng tiền trên NgânLượng.vn";
                    break;
                case "99":
                    _Message = "Lỗi tuy nhiên lỗi chưa được định nghĩa hoặc chưa xác định được nguyên nhân";
                    break;
            }
            return _Message;
        }
    }
}
