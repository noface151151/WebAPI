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
    public class SubTheLoaiController : ApiController
    {
        SubTheLoaiDAL subtheloaiDAL = new SubTheLoaiDAL();
        [ActionName("DefaultAction")]
        [HttpGet]
        public HttpResponseMessage GetAll()
        {
            HttpResponseMessage response = null;
            var Subtheloai = subtheloaiDAL.DanhSachSubTheLoai();
            if (Subtheloai.Count == 0)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Không có dữ liệu"))
                };
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.OK, Subtheloai);
            }
            return response;
        }


        [ActionName("DefaultAction")]
        [HttpGet]
        public HttpResponseMessage GetSubCatByID(int id)
        {
            HttpResponseMessage response = null;
            var Subtheloai = subtheloaiDAL.LaySubTheLoaiTheoID(id);
            if (Subtheloai == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Không có dữ liệu"))
                };
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.OK, Subtheloai);
            }
            return response;
        }

        [HttpPost]
        [ActionName("DefaultAction")]
        public HttpResponseMessage ThemSubTheLoai([FromBody]SubTheLoai Subtheloai)
        {
            HttpResponseMessage response = null;
            if (subtheloaiDAL.ThemSubTheLoai(Subtheloai))
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
        public HttpResponseMessage SuaTheLoai([FromBody]SubTheLoai Subtheloai)
        {
            HttpResponseMessage response = null;
            if (subtheloaiDAL.SuaSubTheLoai(Subtheloai))
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
        public HttpResponseMessage SubCategoryByCategory(int id)
        {
            HttpResponseMessage response = null;
            var Subtheloai = subtheloaiDAL.SubTheLoaiTheoTheLoai(id);
            if (Subtheloai.Count == 0)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Không có dữ liệu"))
                };
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.OK, Subtheloai);
            }
            return response;
        }
        [HttpGet]
        [Route("api/SubTheLoai/OtherSubCategory")]
        public HttpResponseMessage OtherSubCategory()
        {
            HttpResponseMessage response = null;
            var Subtheloai = subtheloaiDAL.SubTheLoaiKhac();
            if (Subtheloai.Count == 0)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Không có dữ liệu"))
                };
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.OK, Subtheloai);
            }
            return response;
        }
        [HttpGet]
        public HttpResponseMessage SubCatbyProduct(int id)
        {
            HttpResponseMessage response = null;
            int idSubtheloai = subtheloaiDAL.LaySubTheLoaiTheoSP1(id);
            if (idSubtheloai != 0)
            {
                response = Request.CreateResponse(HttpStatusCode.OK, idSubtheloai);
            }
            else
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Không tồn tại dữ liệu")),
                };
            }
            return response;
        }
        [HttpGet]
        public HttpResponseMessage GetSubCatBySP(int id)
        {
            HttpResponseMessage response = null;
            var Subtheloai = subtheloaiDAL.LaySubTheLoaiTheoSP(id);
            if (Subtheloai == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Không có dữ liệu"))
                };
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.OK, Subtheloai);
            }
            return response;
        }
    }
}
