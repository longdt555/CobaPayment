using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using System.Linq;
using System.Reflection;
using Common;

namespace Helpers
{
    public static class FunctionHelper
    {
        public static string GenerateJwtToken(int expireMinutes = 1)
        {
            var symmetricKey = Encoding.ASCII.GetBytes(Constant.PRO_API_SECRET_EG);
            var tokenHandler = new JwtSecurityTokenHandler();
            var generator = new Random();
            Byte[] b = new Byte[32];
            generator.NextBytes(b);
            var tokenId = Convert.ToBase64String(b);

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]{
                    new Claim("iss", Constant.PRO_API_KEY_EG),
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
        public static String GenerateErrorMsg(List<string> lstMsg)
        {
            var message = string.Empty;
            if (lstMsg.Count > 0)
            {
                for (var item = 0; item < lstMsg.Count; item++)
                {
                    if (string.IsNullOrEmpty(message))
                        message = lstMsg[item];
                    else
                        message = "<br/>" + lstMsg[item];
                }
            }
            return message;

        }
        public static String GenerateErrorMsg(string fieldName)
        {
            var message = string.Empty;
            if (!string.IsNullOrEmpty(fieldName))
            {
                message = fieldName + " không hợp lệ, vui lòng kiểm tra lại.";
                return message;
            }
            return "Có lỗi xẩy ra vui lòng thử lại.";

        }
        //automapper
        //use for simple object
        public static TDestination Map<TSource, TDestination>(TSource source)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>();
            });

            var mapper = config.CreateMapper();
            return mapper.Map<TDestination>(source);
        }

        //use for complex object : have child object/List<childObject> inside
        public static TDestination Map<TSource, TDestination, TProfile>(TSource source) where TProfile : Profile, new()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TProfile>();
            });

            var mapper = config.CreateMapper();
            return mapper.Map<TSource, TDestination>(source);
        }

        public static List<TDestination> MapList<TSource, TDestination>(List<TSource> source)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>();
            });

            var mapper = config.CreateMapper();
            return mapper.Map<List<TDestination>>(source);
        }

        public static T1 Map<T1, T2>(T2 data)
        {
            throw new NotImplementedException();
        }

        public static List<TDestination> MapList<TSource, TDestination, TProfile>(List<TSource> source) where TProfile : Profile, new()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TProfile>();
            });

            var mapper = config.CreateMapper();
            return mapper.Map<List<TSource>, List<TDestination>>(source);
        }
        public static void AddUnique<T>(this IList<T> self, IEnumerable<T> items, string uniqFields)
        {
            var fields = uniqFields.Split(';').ToList();
            foreach (var item in items)
                if (!self.Any(x => Compare<T>(x, item, fields)))
                    self.Add(item);
        }
        public static bool Compare<T>(T source, T destination, List<string> uniqFields)
        {
            foreach (var uniqField in uniqFields)
            {
                var sourceProp = source.GetType().GetProperty(uniqField, BindingFlags.Instance | BindingFlags.Public);
                var sourceResult = string.Empty;
                if (sourceProp != null)
                {
                    sourceResult = Convert.ToString(sourceProp.GetValue(source, null));
                }
                var destinationProp = destination.GetType().GetProperty(uniqField, BindingFlags.Instance | BindingFlags.Public);
                var destinationResult = string.Empty;
                if (destinationProp != null)
                {
                    destinationResult = Convert.ToString(destinationProp.GetValue(destination, null));
                }
                if (sourceResult != destinationResult) { return false; }
            }
            return true;
        }
    }
}
