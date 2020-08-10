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
                    _Message = "Lỗi, địa chỉ IP truy cập API của NgânLượng.vn bị từ chối";
                    break;
                case "02":
                    _Message = "Lỗi, tham số gửi từ merchant tới NgânLượng.vn chưa chính xác.";
                    break;
                case "03":
                    _Message = "Lỗi, mã merchant không tồn tại hoặc merchant đang bị khóa kết nối tới NgânLượng.vn";
                    break;
                case "04":
                    _Message = "Lỗi, mã checksum không chính xác";
                    break;
                case "05":
                    _Message = "Tài khoản nhận tiền nạp của merchant không tồn tại";
                    break;
                case "06":
                    _Message = "Tài khoản nhận tiền nạp của  merchant đang bị khóa hoặc bị phong tỏa, không thể thực hiện được giao dịch nạp tiền";
                    break;
                case "07":
                    _Message = "Thẻ đã được sử dụng";
                    break;
                case "08":
                    _Message = "Thẻ bị khóa";
                    break;
                case "09":
                    _Message = "Thẻ hết hạn sử dụng";
                    break;
                case "10":
                    _Message = "Thẻ chưa được kích hoạt hoặc không tồn tại";
                    break;
                case "11":
                    _Message = "Mã thẻ sai định dạng";
                    break;
                case "12":
                    _Message = "Sai số serial của thẻ";
                    break;
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
