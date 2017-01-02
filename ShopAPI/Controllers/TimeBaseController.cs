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
    public class TimeBaseController : ApiController
    {
        TimeBaseDAL tbDAL = new TimeBaseDAL();

        [HttpGet]
        [ActionName("DefaultAction")]
        public HttpResponseMessage GetAll()
        {
            HttpResponseMessage response = null;


            var sanpham = tbDAL.DanhSachTimeBase();
            if (sanpham.Count == 0)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Không có dữ liệu"))
                };
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.OK, sanpham);
            }


            return response;
        }

        [HttpPost]
        [ActionName("DefaultAction")]
        public HttpResponseMessage ThemTimebase([FromBody]TimeBase timebase)
        {
            HttpResponseMessage response = null;
            if (tbDAL.ThemTimeBase(timebase))
            {
                response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(string.Format("Thêm thành công")),
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
        public HttpResponseMessage SuaTimebase([FromBody]TimeBaseDTO timebase)
        {
            HttpResponseMessage response = null;
            if (tbDAL.SuaTimeBase(timebase))
            {
                response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(string.Format("Sửa thành công")),
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
        public HttpResponseMessage TimeBaseByProducts(int id)
        {
            HttpResponseMessage response = null;


            var sanpham = tbDAL.TimeBaseTheoSP(id);
            if (sanpham.Count == 0)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Không có dữ liệu"))
                };
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.OK, sanpham);
            }


            return response;
        }

        [HttpPost]
        [Route("api/TimeBase/TimebaseForProducts")]       
        public HttpResponseMessage TimebaseForProducts([FromBody]SanPhamDTO sanpham)
        {
            HttpResponseMessage response = null;
            if (tbDAL.ThemTimebaseSP(sanpham))
            {
                response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(string.Format("Thêm thành công")),
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

        [HttpPost]
        [Route("api/TimeBase/UpdateTimebaseForProducts")]
        public HttpResponseMessage UpdateTimebaseForProducts([FromBody]SanPhamDTO sanpham)
        {
            HttpResponseMessage response = null;
            if (tbDAL.SuaTimebaseSP(sanpham))
            {
                response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(string.Format("Sửa thành công")),
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
    }
}
