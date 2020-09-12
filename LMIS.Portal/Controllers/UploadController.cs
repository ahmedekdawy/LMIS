using LMIS.Portal.Helpers;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using LMIS.Infrastructure.Data.DTOs;

namespace LMIS.Portal.Controllers
{
    public class UploadController : ApiController
    {
        private const int MaxFileCount = 15;
        private readonly string _uploadFolder = ConfigurationManager.AppSettings["VirtualUploadFolder"];

        [HttpPost]
        [Route("api/upload/image")]
        public async Task<HttpResponseMessage> UploadImage()
        {
            return await Upload("image", ".gif,.jpg,.jpeg,.png");
        }

        [HttpPost]
        [Route("api/upload/imageWithPath")]
        public async Task<HttpResponseMessage> UploadImageWithPath()
        {
            return await Upload("image", ".gif,.jpg,.jpeg,.png",_uploadFolder+ HttpContext.Current.Request.QueryString["path"]);
        }
      

        [HttpPost]
        [Route("api/upload/Video")]
        public async Task<HttpResponseMessage> UploadVideo()
        {
            return await Upload("video", ".mp4,.flv,.avi,.wmv,.3gp,.mpg,.mpeg,.asf",_uploadFolder+ HttpContext.Current.Request.QueryString["path"]);
        }

        [HttpPost]
        [Route("api/upload/doc")]
        public async Task<HttpResponseMessage> UploadDoc()
        {
            return await Upload("application/msword,application/vnd.ms-powerpoint,application/vnd.openxmlformats,application/pdf", ".doc,.docx,.ppt,.pptx,.pdf");
        }
      

        [HttpPost]
        [Route("api/upload/UploadDocWithPath")]
        public async Task<HttpResponseMessage> UploadDocWithPath()

        {
            if (HttpContext.Current.Request.QueryString["path"].Contains("DescriptiveJob"))
            {
                if (Utils.CheckPermission(2, 20, Utils.LoggedUser.Roles) < 1)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest,Resources.MessagesResource.X_NotAuthorized);
                   
                }
            }
            return await Upload("application/msword,application/vnd.ms-powerpoint,application/vnd.openxmlformats,application/pdf", ".doc,.docx,.ppt,.pptx,.pdf",_uploadFolder+ HttpContext.Current.Request.QueryString["path"]);
        }

        [HttpPost]
        [Route("api/upload/UploadDelete")]
        public int  UploadDelete()
        {
            if (HttpContext.Current.Request.QueryString["path"].Contains("DescriptiveJob"))
            {
                if (Utils.CheckPermission(4, 20, Utils.LoggedUser.Roles) < 1)
                {
                  return   -4;

                }
            }
            var uploadPath = HttpContext.Current.Server.MapPath(_uploadFolder + HttpContext.Current.Request.QueryString["path"]);
            if (File.Exists(uploadPath))
            {
                File.Delete(uploadPath);
                return 0;
            }
            return -1;

        }

        [HttpPost]
        [Route("api/upload/customImage")]
        public async Task<HttpResponseMessage> CustomUploadImage()
        {
            return await CustomUpload("image", ".gif,.jpg,.jpeg,.png");
        }
       
    
        private async Task<HttpResponseMessage> Upload(string mediaTypes, string extensions, long maxFileSize = 1048576, int maxFileCount = MaxFileCount)
        {
            try
            {
                if (!Request.Content.IsMimeMultipartContent())
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Resources.MessagesResource.X_UnsupportedMediaType);

                var uploadPath = HttpContext.Current.Server.MapPath(_uploadFolder);
                if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);

                var provider = new CustomMultipartFormDataStreamProvider(uploadPath, mediaTypes, extensions, maxFileSize, maxFileCount);

                await Request.Content.ReadAsMultipartAsync(provider);

                var filePaths = provider.FileData
                    .Select(file => file.LocalFileName)
                    .TakeWhile(file => (new FileInfo(file)).Length <= maxFileSize)
                    .Select(Path.GetFileName).ToList();

                if (filePaths.Count == provider.OriginalCount)
                    return Request.CreateResponse(HttpStatusCode.OK, ReduceReturnValue(filePaths, provider.FileMap, provider.IdIncluded));

                foreach (var file in provider.FileData.Select(file => file.LocalFileName))
                    File.Delete(file);

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Resources.MessagesResource.X_InvalidFile);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        private async Task<HttpResponseMessage> Upload(string mediaTypes, string extensions, string uploadpath, long maxFileSize = 100048576, int maxFileCount = MaxFileCount)
        {
            try
            {
                if (!Request.Content.IsMimeMultipartContent())
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Resources.MessagesResource.X_UnsupportedMediaType);

                var uploadPath = HttpContext.Current.Server.MapPath(uploadpath);
                if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);

                var provider = new CustomMultipartFormDataStreamProvider(uploadPath, mediaTypes, extensions, maxFileSize, maxFileCount);
               
                await Request.Content.ReadAsMultipartAsync(provider);
              

                var filePaths = provider.FileData
                    .Select(file => file.LocalFileName)
                    .TakeWhile(file => (new FileInfo(file)).Length <= maxFileSize)
                    .Select(Path.GetFileName).ToList();

                if (filePaths.Count == provider.OriginalCount)
                    return Request.CreateResponse(HttpStatusCode.OK, ReduceReturnValue(filePaths, provider.FileMap, provider.IdIncluded));

                foreach (var file in provider.FileData.Select(file => file.LocalFileName))
                    File.Delete(file);

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Resources.MessagesResource.X_InvalidFile);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
         private async Task<HttpResponseMessage> CustomUpload(string mediaTypes, string extensions, long maxFileSize = 1048576, int maxFileCount = MaxFileCount)
        {
            try
            {
                if (!Request.Content.IsMimeMultipartContent())
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Resources.MessagesResource.X_UnsupportedMediaType);

                var uploadPath = HttpContext.Current.Server.MapPath(_uploadFolder);
                if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);

                var provider = new CustomMultipartFormDataStreamProvider(uploadPath, mediaTypes, extensions, maxFileSize, maxFileCount);

                #region Custom stream
                Stream reqStream = Request.Content.ReadAsStreamAsync().Result;
                MemoryStream tempStream = new MemoryStream();
                reqStream.CopyTo(tempStream);

                tempStream.Seek(0, SeekOrigin.End);
                StreamWriter writer = new StreamWriter(tempStream);
                writer.WriteLine();
                writer.Flush();
                tempStream.Position = 0;


                StreamContent streamContent = new StreamContent(tempStream);
                foreach (var header in Request.Content.Headers)
                {
                    streamContent.Headers.Add(header.Key, header.Value);
                }

                #endregion
                // Read the form data and return an async task.
                await streamContent.ReadAsMultipartAsync(provider);

                // await Request.Content.ReadAsMultipartAsync(provider);

                var filePaths = provider.FileData
                    .Select(file => file.LocalFileName)
                    .TakeWhile(file => (new FileInfo(file)).Length <= maxFileSize)
                    .Select(Path.GetFileName).ToList();

                if (filePaths.Count == provider.OriginalCount)
                    return Request.CreateResponse(HttpStatusCode.OK, ReduceReturnValue(filePaths, provider.FileMap, provider.IdIncluded));

                foreach (var file in provider.FileData.Select(file => file.LocalFileName))
                    File.Delete(file);

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Resources.MessagesResource.X_InvalidFile);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        private static object ReduceReturnValue(IReadOnlyList<string> acceptedFiles, IReadOnlyDictionary<string, string> fileMap, bool includeId)
        {
            if (!includeId) return acceptedFiles[0];

            var ret = new List<object>();

            foreach (var f in acceptedFiles)
                ret.Add(new { id = fileMap[f], serverFileName = f });

            return ret;
        }
    }
}