using DynamoPackageService.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace DynamoPackageService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApiControllerBase:ControllerBase
    {
       
        [ApiExplorerSettings(IgnoreApi = true)]
        public MessageModel<T> Success<T>(T data, string message = "success")
        {
            return new MessageModel<T>(true, message, data);
        }

        
        [NonAction]
        [ApiExplorerSettings(IgnoreApi = true)]
        public MessageModel<T> Failed<T>(string message = "success", int code = 500)
        {
            return new MessageModel<T>(false, message) { StatusCode = code };
        }

        [NonAction]
        [ApiExplorerSettings(IgnoreApi = true)]
        public MessageModel<string> Failed(string message = "failed", int code = 500)
        {
            return new MessageModel<string>(false, message) { StatusCode = code };
        }
        
        [NonAction]
        [ApiExplorerSettings(IgnoreApi = true)]
        public MessageModel<PageModel<T>> SucceedPage<T>(int page, int dataCount, int pageSize, List<T> data, int pageCount, string message = "success")
        {
            var pageModel = new PageModel<T>()
            {
                Data = data,
                PageCount = pageCount,
                PageSize = pageSize,
                Page = page,
                DataCount = dataCount,
            };
            return new MessageModel<PageModel<T>>(true, message, pageModel);
        }
       
        [NonAction]
        [ApiExplorerSettings(IgnoreApi = true)]
        public MessageModel<PageModel<T>> SucceedPage<T>(PageModel<T> page, string message = "success")
        {
            var response = new PageModel<T>()
            {
                Page = page.Page,
                DataCount = page.DataCount,
                Data = page.Data,
                PageSize = page.PageSize,
                PageCount = page.PageCount,
            };
            return new MessageModel<PageModel<T>>(true, message, response);
        }
    }