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
    public class DonHangController : ApiController
    {
        DonHangDAL donhangDAL = new DonHangDAL();

        [ActionName("DefaultAction")]
        [HttpGet]
        public HttpResponseMessage GetAll()
        {
            HttpResponseMessage response = null;
            var donhang = donhangDAL.DanhSachDonHang();
            if (donhang.Count == 0)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Không có dữ liệu"))
                };
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.OK, donhang);
            }
            return response;
        }
        [HttpPost]
        [ActionName("DefaultAction")]
        public HttpResponseMessage Order([FromBody]DonHangDTO donhang)
        {
            HttpResponseMessage response = null;
            if (donhangDAL.ThemDonHang(donhang))
            {
                response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(string.Format("Đặt hàng thành công")),
                };
            }
            else
            {
                response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(string.Format("Đặt hàng thất bại")),
                    ReasonPhrase = "Invalid data, please check again"
                };
            }
            return response;
        }

        [HttpPut]
        [ActionName("DefaultAction")]
        public HttpResponseMessage ChangeStatus([FromBody]DonHangDTO donhang)
        {
            HttpResponseMessage response = null;
            if (donhangDAL.DoiTrangThai(donhang))
            {
                response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(string.Format("Thay đổi trạng thái thành công")),
                };
            }
            else
            {
                response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(string.Format("Thay đổi trạng thái thất bại")),
                    ReasonPhrase = "Invalid data, please check again"
                };
            }
            return response;
        }
        [HttpGet]
        public HttpResponseMessage OrderByCustomer(int id)
        {
            HttpResponseMessage response = null;
            var donhang = donhangDAL.DanhSachDonHangKhachHang(id);
            if (donhang.Count == 0)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Không có dữ liệu"))
                };
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.OK, donhang);
            }
            return response;
        }
        [HttpGet]
        public HttpResponseMessage OrderDetails(int id)
        {
            HttpResponseMessage response = null;
            var donhang = donhangDAL.DanhSachCTDonHang(id);
            if (donhang.Count == 0)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Không có dữ liệu"))
                };
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.OK, donhang);
            }
            return response;
        }
    }
}
