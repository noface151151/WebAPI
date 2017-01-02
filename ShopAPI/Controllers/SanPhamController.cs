using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MODEL.DTO;
using MODEL.DAL;
using MODEL;
using System.Web.Hosting;
using System.IO;
using System.Net.Http.Headers;
using System.Web;

namespace ShopAPI.Controllers
{
    public class SanPhamController : ApiController
    {
        SanPhamDAL sanphamDAL = new SanPhamDAL();
        [HttpGet]
        [ActionName("DefaultAction")]
        public HttpResponseMessage GetAll()
        {
            HttpResponseMessage response = null;
            var sanpham = sanphamDAL.DanhSachSanPham();
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

         [Route("api/SanPham/HotProduct")]
        public HttpResponseMessage GetHotProducts()
        {
            HttpResponseMessage response = null;
            var sanpham = sanphamDAL.DanhSachSanPhamHot();
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
        [Route("api/SanPham/OtherProduct")]
        public HttpResponseMessage OtherProduct()
        {
            HttpResponseMessage response = null;
            var sanpham = sanphamDAL.SanPhamKhac();
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
        [ActionName("DefaultAction")]
        public HttpResponseMessage ProductById(int id)
        {
            HttpResponseMessage response = null;
            var sanpham = sanphamDAL.LaySanPhamTheoId(id);
            if (sanpham == null)
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
        public HttpResponseMessage ProductByCat(int id)
        {
            HttpResponseMessage response = null;


            var sanpham = sanphamDAL.LaySanPhamTheoTheLoai(id);
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
        public HttpResponseMessage ProductBySubCat(int id)
        {
            HttpResponseMessage response = null;


            var sanpham = sanphamDAL.LaySanPhamTheoSubTheLoai(id);
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
        public HttpResponseMessage RelaredProducts(int id) 
        {
            HttpResponseMessage response = null;


            var sanpham = sanphamDAL.LaySanPhamlienquanSubTheLoai(id);
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
        public HttpResponseMessage ThemSanPham([FromBody]SanPhamDTO sanpham)
        {
            HttpResponseMessage response = null;
            if (sanphamDAL.ThemSanPham(sanpham))
            {
                response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(string.Format("Thêm sản phẩm thành công")),
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
        public HttpResponseMessage IncreaseView(int id)
        {
            HttpResponseMessage response = null;
            if (sanphamDAL.TangLuotXem(id))
            {
                response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(string.Format("Thành công")),
                };
            }
            else
            {
                response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(string.Format("Thất bại")),
                    ReasonPhrase = "Invalid data, please check again"
                };
            }
            return response;
        }

        [HttpPut]
        [ActionName("DefaultAction")]
        public HttpResponseMessage SuaSanPham([FromBody]SanPhamDTO sanpham)
        {
            HttpResponseMessage response = null;
            if (sanphamDAL.SuaSanPham(sanpham))
            {
                response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(string.Format("Sửa sản phẩm thành công")),
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
        [Route("image/get")]
        public HttpResponseMessage ImageGet(string imageName)
        {
            var resp = Request.CreateResponse(HttpStatusCode.OK);

            var path = "~/HinhAnh/" + imageName;
            path = HostingEnvironment.MapPath(path);
            var ext = Path.GetExtension(path);

            var content = File.ReadAllBytes(path);

            MemoryStream ms = new MemoryStream(content);

            resp.Content = new StreamContent(ms);
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("image/" + ext);

            return resp;
        }
        [Route("image/upload")]
        public HttpResponseMessage Post()
        {
            HttpResponseMessage response = null;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/HinhAnh/" + postedFile.FileName);
                    if (File.Exists(filePath))
                    {
                        response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                        {
                            Content = new StringContent(string.Format("Trùng tên file hình ảnh, xin hãy chọn tên khác"))
                        };
                    }
                    else
                    {
                        postedFile.SaveAs(filePath);
                        response = new HttpResponseMessage(HttpStatusCode.OK)
                        {
                            Content = new StringContent(string.Format("Đã lưu thành công hình ảnh"))
                        };
                    }
                }

            }
            else
            {
                response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(string.Format("Chưa chọn hình ảnh"))
                };
            }
            return response;
        }
        [Route("image/check")]
        [HttpPost]
        public HttpResponseMessage CheckHinhAnh()
        {
            HttpResponseMessage response = null;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/HinhAnh/" + postedFile.FileName);
                    if (File.Exists(filePath))
                    {
                        response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                        {
                            Content = new StringContent(string.Format("Trùng tên file hình ảnh, xin hãy chọn tên khác"))
                        };
                    }
                    else
                    {
                        response = new HttpResponseMessage(HttpStatusCode.OK)
                        {
                            Content = new StringContent(string.Format("Hình ảnh hợp lệ"))
                        };
                    }
                }

            }
            else
            {
                response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(string.Format("Chưa chọn hình ảnh"))
                };
            }
            return response;
        }




        //dành cho time based
       [Route("api/SanPham/GetProductsTime")]
        [HttpGet]
        public HttpResponseMessage GetProductsTime()
        {
            HttpResponseMessage response = null;
            var sanpham = sanphamDAL.GetSPTimeBase();
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
       [Route("api/SanPham/OtherProductTime")]
       public HttpResponseMessage OtherProductTime()
       {
           HttpResponseMessage response = null;
           var sanpham = sanphamDAL.SanPhamKhacTimeBase();
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
       public HttpResponseMessage ProductByCatTime(int id)
       {
           HttpResponseMessage response = null;


           var sanpham = sanphamDAL.LaySanPhamTheoTheLoaiTimebase(id);
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
       public HttpResponseMessage ProductBySubCatTime(int id)
       {
           HttpResponseMessage response = null;


           var sanpham = sanphamDAL.LaySanPhamTheoSubTheLoaiTimebase(id);
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
       public HttpResponseMessage RelaredProductsTime(int id)
       {
           HttpResponseMessage response = null;


           var sanpham = sanphamDAL.LaySanPhamlienquanSubTheLoaiTimebase(id);
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
    }
}
