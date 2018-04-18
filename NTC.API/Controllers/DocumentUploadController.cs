using NTC.API.Models;
using NTC.InterfaceServices;
using NTC.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace NTC.API.Controllers
{
    public class DocumentUploadController : ApiController
    {
        private readonly IEventLogService _eventLogService;

        public DocumentUploadController(IEventLogService eventLogService)
        {
            _eventLogService = eventLogService;
        }

        [HttpPost]
        public async Task<IHttpActionResult> MediaUpload()
        {
            try
            {
                string errorMessage = String.Empty;
                IList<UploadFileDataViewModel> lstUploadedFileResult = new List<UploadFileDataViewModel>();

                // Check if the request contains multipart/form-data.  
                if (Request.Content.IsMimeMultipartContent())
                {
                    var provider = await Request.Content.ReadAsMultipartAsync<InMemoryMultipartFormDataStreamProvider>(new InMemoryMultipartFormDataStreamProvider());
                    //access form data  
                    NameValueCollection formData = provider.FormData;
                    //access files  
                    IList<HttpContent> files = provider.Files;
                    

                    string FileFolder1 = formData["imageFolder"].ToString();


                    for (int i = 0; i < files.Count; i++)
                    {
                        string uploadedFileName = String.Empty;
                        HttpContent uploadedFile = files[i];

                        if (String.IsNullOrEmpty(formData["uploadedFileName"].ToString()))
                        {
                            var originalFileName = uploadedFile.Headers.ContentDisposition.FileName.Trim('\"');
                            string originalFileExtension = String.IsNullOrEmpty(formData["fileExtension"]) ? Path.GetExtension(originalFileName) : formData["fileExtension"].ToString();
                            uploadedFileName = String.Format("{0}_{1}", DateTime.UtcNow.ToString("yyyyMMddHHmmssfff"), originalFileExtension);
                        }
                        else
                        {
                            uploadedFileName = formData["uploadedFileName"].ToString();
                        }


                        string directoryName = String.Empty;
                        string filename = String.Empty;
                        Stream input = await uploadedFile.ReadAsStreamAsync();

                        directoryName = HttpContext.Current.Server.MapPath(String.Format("~/images/{0}/", FileFolder1));
                        filename = Path.Combine(directoryName, uploadedFileName);

                        //Deletion exists file  
                        if (File.Exists(filename))
                        {
                            File.Delete(filename);
                        }

                        using (Stream file = File.OpenWrite(filename))
                        {
                            input.CopyTo(file);
                            //close file  
                            file.Close();
                        }

                        UploadFileDataViewModel uplaodedFileData = new UploadFileDataViewModel();
                        uplaodedFileData.filePath = String.Format("{0}/{1}", FileFolder1, uploadedFileName);
                        lstUploadedFileResult.Add(uplaodedFileData);
                    }
                }
                else
                {
                    errorMessage = Constant.MessageFileTypeError;
                }

                var messageData = new
                {
                    code = String.IsNullOrEmpty(errorMessage) ? Constant.SuccessMessageCode : Constant.ErrorMessageCode
                    ,
                    message = String.IsNullOrEmpty(errorMessage) ? Constant.MessageSuccess : errorMessage
                };
                var returnObject = new { filesData = lstUploadedFileResult, messageCode = messageData };
                return Ok(returnObject);
            }
            catch (Exception ex)
            {
                string errorLogId = _eventLogService.WriteLogs(User.Identity.Name, ex, MethodBase.GetCurrentMethod().Name);
                var messageData = new { code = Constant.ErrorMessageCode, message = String.Format(Constant.MessageTaskmateError, errorLogId) };
                var returnObject = new { messageCode = messageData };
                return Ok(returnObject);
            }
        }
    }
}
