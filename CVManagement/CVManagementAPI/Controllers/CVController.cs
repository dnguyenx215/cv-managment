using CVManagementAPI.DTO;
using CVManagementAPI.Extention;
using CVManagementAPI.Helpers;
using CVManagementAPI.Models;
using CVManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Net;
namespace CVManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CVController : BaseControllerGeneric<CV, CvDTO>
    {
        private readonly CVService _cvService;
        //  private readonly ICacheService _cacheService;
        public CVController(ServiceFactory service) : base(service.CVService)
        {
            _cvService = service.CVService;
            // _cacheService = cacheService;
        }

        /// <summary>
        /// Search CV by status, name, phone number, email
        /// </summary>
        /// <param name="status"></param>
        /// <param name="keyword"></param>
        /// <returns>List<CV></returns>
        [HttpPost("Search")]
        public async Task<ResponseFormat> Search([FromForm] string? status, [FromForm] string? isSendMail,
            [FromForm] string? name, [FromForm] string? phone, [FromForm] string? email)
        {
            var filter = SearchCV(status, isSendMail, name, phone, email);
            var result = await _service.SearchAsync(filter);
            return new ResponseFormat(HttpStatusCode.NoContent, "Search  Success!", result);
        }

        private Expression<Func<CV, bool>> SearchCV(string? status, string? isSendMail, string? name, string? phone,
            string? email)
        {
            Expression<Func<CV, bool>> filter = x => true;
            if (!string.IsNullOrEmpty(status))
                filter = filter.And(x => x.Status == status);
            if (!string.IsNullOrEmpty(isSendMail))
                filter = filter.And(x => x.IsSendMail == isSendMail);
            if (!string.IsNullOrEmpty(name))
                filter = filter.And(x => x.NameCandidate != null && x.NameCandidate.ToLower().Contains(name.ToLower()));
            if (!string.IsNullOrEmpty(phone))
                filter = filter.And(x => x.PhoneNumber != null && x.PhoneNumber.ToLower().Contains(phone.ToLower()));
            if (!string.IsNullOrEmpty(email))
                filter = filter.And(x => x.Email != null && x.Email.ToLower().Contains(email.ToLower()));
            return filter;
        }
        [HttpPost("UploadPDF")]
        public async Task<IActionResult> UploadPDF(IFormFile pdfFile)
        {
            if (pdfFile == null || pdfFile.Length == 0)
            {
                return BadRequest("Invalid file.");
            }

            return Ok(await _cvService.UploadPDF(pdfFile));
        }

        [HttpPost("PDFToText")]
        public async Task<IActionResult> PDFToText(IFormFile pdfFile)
        {
            if (pdfFile == null || pdfFile.Length == 0)
            {
                return BadRequest("Invalid file.");
            }

            return Ok(await _cvService.PDFToText(pdfFile));
        }

        [HttpGet("ExcelExport")]
        public async Task<FileStreamResult> ExcelExport([FromQuery] string? status, [FromQuery] string? isSendMail,
            [FromQuery] string? name, [FromQuery] string? phone, [FromQuery] string? email)
        {
            var filter = SearchCV(status, isSendMail, name, phone, email);
            var list = await _service.SearchAsync(filter);
            return await _cvService.ExcelExport(list);
        }

        ///// <summary>
        ///// Update StatusCv khi kéo thả vào các ô tương ứng trên kanban
        ///// </summary>
        ///// <param name="idStatusUpdate">0 -> 5</param>
        ///// <param name="cvNeedupdate">Model CV</param>
        ///// <param name="idCvNeedUpdate">Int, là khoá chính của bảng CV</param>
        ///// <returns></returns>
        [HttpPut("update-status")]
        public async Task<ActionResult<ResponseFormat>> UpdateCVStatus(string StatusCv, int idCvNeedUpdate, CvDTO request, int columnId)
        {
            var response = await _cvService.UpdateCVStatus(StatusCv, idCvNeedUpdate, request, columnId);

            return new ResponseFormat(HttpStatusCode.OK, "Update status success", response);
        }

        /// <summary>
        /// Update một loạt trạng thái và columnId của các Cvs
        /// </summary>
        /// <param name="StatusCv"></param>
        /// <param name="requests"></param>
        /// <param name="columnId"></param>
        /// <returns></returns>
        [HttpPut("update-list-status")]
        public async Task<ActionResult<ResponseFormat>> UpdateCVsStatus(List<CvDTO> requests)
        {
            var response = await _cvService.UpdateManyAsync(requests);
            return new ResponseFormat(HttpStatusCode.OK, "Update status success", response);
        }

        [HttpPost("get-email-template")]
        public async Task<ContentResult> GetEmailTemplate(string StatusCv, CvDTO request)
        {
            //var res = await _cvService.GetEmailTemplate(StatusCv, request);
            //string mailContent = await _cacheService.GetAsync<string>($"mail_{request.Id.ToString()}");
            var expiryTime = DateTimeOffset.Now.AddMinutes(Constants.ExpiredTime);


            //Lưu entities role vào cache
            //var res = await _cacheService.SetAsync<string>($"mail_{request.Id.ToString()}", mailContent, expiryTime); ;
            var res = await _cvService.GetEmailTemplate(StatusCv, request);

            //Lưu entities role vào cache
            // var resSaveCasche = await _cacheService.SetAsync<string>($"mail_{request.Id.ToString()}", res.ToString(), expiryTime);
            return new ContentResult
            {
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content = res.ToString()
            };

            //else
            //{
            //    Dictionary<string, string> subjectAndBodyEmail = SubjectBodyMail.GetBodyByStatus(StatusCv, request);
            //    var res = await _cacheService.GetAsync<string>($"mail_{request.Id.ToString()}");
            //    res = res.Replace("{{body_email}}", subjectAndBodyEmail["body"]);
            //    return new ContentResult
            //    {
            //        ContentType = "text/html",
            //        StatusCode = (int)HttpStatusCode.OK,
            //        Content = res
            //    };
            //}

        }

        /// <summary>
        /// Hàm send mail, chú ý CvDTO có MailContent bao gồm EmailsCC và EmailsBCC
        /// </summary>
        /// <param name="idCvNeedUpdate"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("sendmail")]
        public async Task<ActionResult<ResponseFormat>> SendMail(int idCvNeedUpdate, CvDTO request)
        {
            var response = await _cvService.SendMailBaseOnStatus(idCvNeedUpdate, request);
            return new ResponseFormat(HttpStatusCode.OK, "Send mail success", response);
        }

        /// <summary>
        /// Hàm send mail, chú ý CvDTO có MailContent bao gồm EmailsCC và EmailsBCC
        /// </summary>
        /// <param name="idCvNeedUpdate"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("send-list-mail")]
        public async Task<ActionResult<ResponseFormat>> SendMails(List<CvDTO> requests)
        {
            foreach (CvDTO req in requests)
            {
                var response = await _cvService.SendMailBaseOnStatus((int)req.Id, req);
            }

            return new ResponseFormat(HttpStatusCode.OK, "Send list mail success", requests);
        }
    }

    /// <summary>
    /// Update list StatusCv khi kéo thả vào các ô tương ứng trên kanban
    /// </summary>
    /// <param name="idStatusUpdate">0 -> 5</param>
    /// <param name="cvNeedupdate">Model CV</param>
    /// <param name="idCvNeedUpdate">Int, là khoá chính của bảng CV</param>
    /// <returns></returns>
    //[HttpPut("update-status-list")]
    //public async Task<ActionResult<ResponseFormat>> UpdateListCVStatus(string StatusCv, int idCvNeedUpdate, List<PutCVRequest> request)
    //{
    //    for (int i = 0; i < request.Count; i++)
    //    {
    //        //Validate putCvRequest business logic only when want to send mail
    //        PutCVRequestValidation validationRules = new PutCVRequestValidation();
    //        var validationResult = validationRules.Validate(request[i]);

    //        if (!validationResult.IsValid)
    //        {
    //            List<ErrorObject> errorList = new List<ErrorObject>();

    //            foreach (var error in validationResult.Errors)
    //            {
    //                var objectError = new ErrorObject
    //                {
    //                    Field = error.PropertyName,
    //                    Message = error.ErrorMessage
    //                };
    //                errorList.Add(objectError);
    //            }
    //            //throw new HttpStatusCodeException(HttpStatusCode.BadRequest,
    //            //   JsonConvert.SerializeObject(errorList));
    //            return BadRequest(errorList);
    //        }

    //        //update send mail status
    //        request[i].Status = StatusCv;
    //        var cv = _mapper.Map<CvDTO>(request[i]);
    //        cv.IsSendMail = Constants.EmailStatus.SENDING;
    //        await _baseController.UpdateAsync(idCvNeedUpdate, cv);

    //        //schedual mail sending
    //        cv.IsSendMail = Constants.EmailStatus.SENT;
    //        var scheduleDateTime = DateTime.UtcNow.AddSeconds(2);
    //        var dateTimeOffset = new DateTimeOffset(scheduleDateTime);

    //        //Get subject and body base on statusCv received
    //        Dictionary<string, string> subjectAndBodyEmail = SubjectBodyMail.GetBodyByStatus(StatusCv, request[i]);

    //        //delete previous sending mail
    //        SendMail.DeleteExistingJobs(idCvNeedUpdate);

    //        //schedual job send mail with explicit time
    //        var jobSendMail = BackgroundJob.Schedule(() => SendMail.Send(new MailContent
    //        {
    //            IdCvNeedToSendMail = idCvNeedUpdate,
    //            MailTo = "lehuyhaianh0808@gmail.com",
    //            Subject = subjectAndBodyEmail["subject"],
    //            Content = subjectAndBodyEmail["body"]
    //        }), dateTimeOffset);

    //        //After send mail job done, update CV again
    //        var jobUpdateCvStatus = BackgroundJob.ContinueJobWith(jobSendMail, () => _baseController.UpdateAsync(idCvNeedUpdate, cv), JobContinuationOptions.OnlyOnSucceededState);
    //    }

    //    return new ResponseFormat(HttpStatusCode.OK, "Update and sending mail for list CV success ", 1);
    //}


}
