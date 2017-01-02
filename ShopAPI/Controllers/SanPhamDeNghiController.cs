using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MODEL.DTO;
using MODEL.DAL;
using MODEL;

namespace ShopAPI.Controllers
{
    public class SanPhamDeNghiController : ApiController
    {
        SanPhamDeNghiDAL denghiDAL = new SanPhamDeNghiDAL();

        [HttpGet]
        [Route("api/SanPhamDeNghi/coSPDN")]
        public HttpResponseMessage coSPDN()//cái này là lấy sản phẩm đã có sản phẩm đề nghị
        {
            HttpResponseMessage response = null;
            var sanpham = denghiDAL.LaySPDeNghi1();
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

        [HttpGet]
        [Route("api/SanPhamDeNghi/KhongcoSPDN")]
        public HttpResponseMessage KhongcoSPDN()//cái này là lấy sản phẩm chưa có sản phẩm đề nghị
        {
            HttpResponseMessage response = null;
            var sanpham = denghiDAL.LaySPChuaDeNghi1();
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

        [HttpGet]
        public HttpResponseMessage ProductsRecommendation(int id)//cái này là lấy sản phẩm đã được đề nghị cho 1 sản phẩm nào đó
        {
            HttpResponseMessage response = null;
            var sanpham = denghiDAL.LaySPDeNghi(id);
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

        [HttpGet]
        public HttpResponseMessage ProductsNotRecommendation(int id)// cái này là lấy sản phẩm chưa được đề nghị cho 1 sản phẩm nào đó
        {
            HttpResponseMessage response = null;
            var sanpham = denghiDAL.LaySPChuaDeNghi(id);
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
        public HttpResponseMessage ThemSanPhamDeNghi([FromBody]SanPhamDeNghiDTO sanphamdenghi)
        {
            HttpResponseMessage response = null;
            if (denghiDAL.ThemSanPhamDeNghi(sanphamdenghi))
            {
                response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(string.Format("Thêm sản phẩm đề nghị thành công")),
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
        public HttpResponseMessage SuaSanPhamDeNghi([FromBody]SanPhamDeNghiDTO sanphamdenghi)
        {
            HttpResponseMessage response = null;
            if (denghiDAL.SuaSanPhamDeNghi(sanphamdenghi))
            {
                response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(string.Format("Sửa sản phẩm đề nghị thành công")),
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
