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
    public class TheLoaiController : ApiController
    {
        TheLoaiDAL theloaiDAL = new TheLoaiDAL();
        [ActionName("DefaultAction")]
        [HttpGet]
        public HttpResponseMessage GetAll()
        {
            HttpResponseMessage response = null;
            var theloai = theloaiDAL.DanhSachTheLoai();
            if (theloai.Count == 0)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Không có dữ liệu"))
                };
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.OK, theloai);
            }
            return response;
        }
        [Route("api/TheLoai/GetAllWinform")]
        [HttpGet]
        public HttpResponseMessage GetAllWinform()
        {
            HttpResponseMessage response = null;
            var theloai = theloaiDAL.DanhSachTheLoaiWinForm();
            if (theloai.Count == 0)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Không có dữ liệu"))
                };
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.OK, theloai);
            }
            return response;
        }

        [ActionName("DefaultAction")]
        [HttpGet]
        public HttpResponseMessage GetCatByID(int id)
        {
            HttpResponseMessage response = null;
            var theloai = theloaiDAL.LayTheLoaiTheoID(id);
            if (theloai == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Không có dữ liệu"))
                };
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.OK, theloai);
            }
            return response;
        }

        [HttpPost]
        [ActionName("DefaultAction")]
        public HttpResponseMessage ThemTheLoai([FromBody]TheLoai theloai)
        {
            HttpResponseMessage response = null;
            if (theloaiDAL.ThemTheLoai(theloai))
            {
                response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(string.Format("Thêm thể loại thành công")),
                };
            }
            else
            {
                response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(string.Format("Thêm thất bại")),
                    ReasonPhrase = "Invalid data, please check again"
                };
            }
            return response;
        }

        [HttpPut]
        [ActionName("DefaultAction")]
        public HttpResponseMessage SuaTheLoai([FromBody]TheLoai theloai)
        {
            HttpResponseMessage response = null;
            if (theloaiDAL.SuaTheLoai(theloai))
            {
                response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(string.Format("Sửa thể loại thành công")),
                };
            }
            else
            {
                response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(string.Format("Sửa thất bại")),
                    ReasonPhrase = "Invalid data, please check again"
                };
            }
            return response;
        }
        [HttpGet]
        public HttpResponseMessage GetCatBySP(int id)
        {
            HttpResponseMessage response = null;
            var theloai = theloaiDAL.LayTheLoaiTheoSP(id);
            if (theloai == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Không có dữ liệu"))
                };
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.OK, theloai);
            }
            return response;
        }
    }
}
