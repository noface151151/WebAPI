using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MODEL.DAL;
using MODEL.DTO;
using MODEL;

namespace ShopAPI.Controllers
{
    public class KhachHangController : ApiController
    {
        KhachHangDAL khachhangDAL = new KhachHangDAL();
        [ActionName("DefaultAction")]
        [HttpGet]
        public HttpResponseMessage GetAll()
        {
            HttpResponseMessage response = null;
            var khachhang = khachhangDAL.DanhSachKhachHang();
            if (khachhang.Count == 0)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Không có dữ liệu"))
                };
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.OK, khachhang);
            }
            return response;
        }

        [ActionName("DefaultAction")]
        [HttpGet]
        public HttpResponseMessage GetUserByUserName(string id)
        {
            HttpResponseMessage response = null;
            var khachhang = khachhangDAL.LayKhachHangTheoUserName(id);
            if (khachhang == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Không có dữ liệu"))
                };
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.OK, khachhang);
            }
            return response;
        }

        [HttpPost]
        [ActionName("DefaultAction")]
        public HttpResponseMessage Register([FromBody]KhachHang khachhang)
        {
            HttpResponseMessage response = null;
            if (khachhangDAL.ThemKhachHang(khachhang))
            {
                response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(string.Format("Đăng ký thành công")),
                };
            }
            else
            {
                response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(string.Format("Đăng ký thất bại")),
                    ReasonPhrase = "Invalid data, please check again"
                };
            }
            return response;
        }

        [HttpPost]
        [Route("login")]
        public HttpResponseMessage Login([FromBody]KhachHang khachhang)
        {
            HttpResponseMessage response = null;
            var khachhangdangnhap = khachhangDAL.DangNhap(khachhang);
            if (khachhangdangnhap == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Đăng nhập thất bại")),
                };
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.OK, khachhangdangnhap);
            }
            return response;
        }

        [HttpPut]
        [ActionName("DefaultAction")]
        public HttpResponseMessage ChangePass([FromBody]KhachHang khachhang)
        {
            HttpResponseMessage response = null;
            if (khachhangDAL.SuaThongTin(khachhang))
            {
                response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(string.Format("Đổi mật khẩu thành công")),
                };
            }
            else
            {
                response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(string.Format("Đổi mật khẩu thất bại")),
                    ReasonPhrase = "Invalid data, please check again"
                };
            }
            return response;
        }
    }
}
