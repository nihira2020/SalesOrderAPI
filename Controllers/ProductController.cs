using Microsoft.AspNetCore.Mvc;
using SalesOrderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace SalesOrderAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductContainer _container;
    private readonly IWebHostEnvironment _environment;
    public ProductController(IProductContainer container, IWebHostEnvironment environment)
    {
        this._container = container;
        this._environment = environment;
    }

    [HttpGet("GetAll")]
    public async Task<List<ProductEntity>> GetAll()
    {
        var productlist = await this._container.Getall();
        if (productlist != null && productlist.Count > 0)
        {
            productlist.ForEach(item =>
            {
                item.productImage = GetImagebyProduct(item.Code);
            });
        }
        else
        {
            return new List<ProductEntity>();
        }
        return productlist;

    }
    [HttpGet("GetByCode")]
    public async Task<ProductEntity> GetByCode(string Code)
    {
        return await this._container.Getbycode(Code);

    }

    [HttpGet("Getbycategory")]
    public async Task<List<ProductEntity>> Getbycategory(int Code)
    {
        return await this._container.Getbycategory(Code);

    }

    [HttpPost("UploadImage")]
    public async Task<ActionResult> UploadImage()
    {
        bool Results = false;
        try
        {
            var _uploadedfiles = Request.Form.Files;
            foreach (IFormFile source in _uploadedfiles)
            {
                string Filename = source.FileName;
                string Filepath = GetFilePath(Filename);

                if (!System.IO.Directory.Exists(Filepath))
                {
                    System.IO.Directory.CreateDirectory(Filepath);
                }

                string imagepath = Filepath + "\\image.png";

                if (System.IO.File.Exists(imagepath))
                {
                    System.IO.File.Delete(imagepath);
                }
                using (FileStream stream = System.IO.File.Create(imagepath))
                {
                    await source.CopyToAsync(stream);
                    Results = true;
                }


            }
        }
        catch (Exception ex)
        {

        }
        return Ok(Results);
    }

    [HttpGet("RemoveImage/{code}")]
    public ResponseType RemoveImage(string code)
    {
        string Filepath = GetFilePath(code);
        string Imagepath = Filepath + "\\image.png";
        try
        {
            if (System.IO.File.Exists(Imagepath))
            {
                System.IO.File.Delete(Imagepath);
            }
            return new ResponseType { Result = "pass", KyValue = code };
        }
        catch (Exception ext)
        {
            throw ext;
        }
    }

    [HttpPost("SaveProduct")]
    public async Task<ResponseType> SaveProduct([FromBody] ProductEntity _product)
    {
        return await this._container.SaveProduct(_product);
    }


    [NonAction]
    private string GetFilePath(string ProductCode)
    {
        return this._environment.WebRootPath + "\\Uploads\\Product\\" + ProductCode;
    }
    [NonAction]
    private string GetImagebyProduct(string productcode)
    {
        string ImageUrl = string.Empty;
        string HostUrl = "https://localhost:7118/";
        string Filepath = GetFilePath(productcode);
        string Imagepath = Filepath + "\\image.png";
        if (!System.IO.File.Exists(Imagepath))
        {
            ImageUrl = HostUrl + "/uploads/common/noimage.png";
        }
        else
        {
            ImageUrl = HostUrl + "/uploads/Product/" + productcode + "/image.png";
        }
        return ImageUrl;

    }

}
