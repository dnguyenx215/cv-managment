using AutoMapper;
using CVManagementAPI.DTO;
using CVManagementAPI.FluentValidation;
using CVManagementAPI.Helpers;
using CVManagementAPI.Helpers.Page;
using CVManagementAPI.Middleware;
using CVManagementAPI.Models;
using CVManagementAPI.Repository;
using Hangfire;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace CVManagementAPI.Services
{
    public class CVService : ControllerBase, IBaseService<CV, CvDTO>
    {
        private readonly CVRepository _cvRepository;
        private readonly PositionRepository _poisitionRepository;

        private readonly IMapper _mapper;

        public CVService(IMapper mapper, IServiceProvider serviceProvider)
        {
            var _repositoryFactory = serviceProvider.GetService<RepositoryFactory>();
            _cvRepository = _repositoryFactory.CVRepository;
            _poisitionRepository = _repositoryFactory.PositionRepository;
            _mapper = mapper;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var existingEntity = await _cvRepository.FindAsync(id);
            if (existingEntity == null)
            {
                throw new BadHttpRequestException("Entity not found.");
            }

            return await _cvRepository.DeleteAsync(existingEntity);
        }

        public async Task<CvDTO> FindAsync(int id)
        {
            var entity = await _cvRepository.FindAsync(id);
            if (entity == null)
            {
                return new CvDTO();
            }

            var cvDto = _mapper.Map<CvDTO>(entity);

            if (cvDto == null)
            {
                return new CvDTO();
            }

            return cvDto;
        }

        public async Task<int> InsertAsync(CvDTO obj)
        {
            var map = _mapper.Map<CV>(obj);
            return await _cvRepository.InsertAsync(map);
        }

        public async Task<List<CvDTO>> SearchAsync(Expression<Func<CV, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new BadHttpRequestException("Predicate is null!");
            }

            var searchResult = await _cvRepository.SearchAsync(predicate);
            var resultDTO = _mapper.Map<List<CvDTO>>(searchResult);
            return resultDTO;
        }

        public async Task<List<CvDTO>> ToListAsync()
        {
            var entities = await _cvRepository.ToListAsync();
            var cvListDTO = _mapper.Map<List<CvDTO>>(entities).ToList();
            return cvListDTO;
        }

        public async Task<int> UpdateAsync(int id, CvDTO obj)
        {
            var existingEntity = await _cvRepository.SelectAsync(x => x.Id == id);
            if (!existingEntity)
            {
                return -1;
            }

            if (id != obj.Id)
            {
                return -1;
            }

            var updateEntity = _mapper.Map<CV>(obj);
            return await _cvRepository.UpdateAsync(updateEntity);
        }

        public async Task<string> UploadPDF([FromForm] IFormFile pdfFile)
        {
            string url = await Upload.Upfile(pdfFile);
            return url;
        }

        public async Task<Object> PDFToText([FromForm] IFormFile pdfFile)
        {
            using var stream = new MemoryStream();
            await pdfFile.CopyToAsync(stream);
            stream.Position = 0;

            string pdfText = ExtractTextFromPdf(stream);
            string strim = Regex.Replace(pdfText, @"\s+", "");
            string name = ExtractInfo(pdfText, @$"(?:{Constants.Surname.StringRegex()})\s+([^\n]+)");
            string school = ExtractInfo(pdfText, @"(?:(Đại học|Cao đẳng)\s+([^.\n]+)|([^\n]+)\s+(University|college)\s+([^.\n]+))");
            string position = ExtractInfo(pdfText, @"\b(?:C#|NodeJS|Unity|PHP|Java(?!\S))");
            string phone = ExtractInfo(strim, @"(0|\+84)\d{9}");
            string email = ExtractInfo(pdfText, @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}");
            string birthYear = ExtractInfo(pdfText, @"\b(19[7-9][0-9]|200[0-9])\b");
            
            var result = new
            {
                Name = name,
                School = school,
                Position = position,
                Phone = phone,
                Email = email,
                BirthYear = birthYear,
                PdfText = pdfText
            };
            return result;
        }

        private string ExtractTextFromPdf(Stream pdfStream)
        {
            using var pdfReader = new PdfReader(pdfStream);
            StringBuilder textBuilder = new StringBuilder();

            for (int i = 1; i <= pdfReader.NumberOfPages; i++)
            {
                string pageText = PdfTextExtractor.GetTextFromPage(pdfReader, i);
                textBuilder.Append(pageText);
            }

            return textBuilder.ToString();
        }

        private string ExtractInfo(string text, string pattern)
        {
            var match = Regex.Match(text, pattern, RegexOptions.IgnoreCase);
            return match.Success ? match.Value.Trim() : string.Empty;
        }

        public async Task<FileStreamResult> ExcelExport(List<CvDTO> list)
        {
            var listCV = await _cvRepository.ToListAsync();
            if (listCV != null)
            {
                listCV = _mapper.Map<List<CV>>(list);
            }
            var result = await Helpers.ExcelExport.ExportToExcelHelper(listCV);

            var contentType = "application/octet-stream";
            var fileName = "dataCV.xlsx";

            return File(result, contentType, fileName);
        }
        public async Task<PageResult<CV>> GetAllPagination(Pagination? pagination)
        {
            var entities = await _cvRepository.ToListAsync();
            var result = PageResult<CV>.ToPageResult(pagination, entities);
            pagination.TotalCount = entities.Count();
            return new PageResult<CV>(pagination, result);
        }

        public async Task<CvDTO> UpdateCVStatus(string StatusCv, int idCvNeedUpdate, CvDTO request, int columnId)
        {
            //Validate putCvRequest business logic only when want to send mail
            //PutCVRequestValidation validationRules = new PutCVRequestValidation(StatusCv);
            CV entity = _mapper.Map<CV>(request);
            //var validationResult = validationRules.Validate(entity);
            //if (!validationResult.IsValid && StatusCv != Constants.StatusCv.REVIEW_CV_FAIL)
            //{
            //    List<ErrorObject> errorList = new List<ErrorObject>();

            //    foreach (var error in validationResult.Errors)
            //    {
            //        var objectError = new ErrorObject
            //        {
            //            Field = error.PropertyName,
            //            Message = error.ErrorMessage
            //        };
            //        errorList.Add(objectError);
            //    }
            //    var dictionary = new Dictionary<string, List<ErrorObject>>
            //    {
            //        { "errors", errorList }
            //    };
            //    throw new HttpStatusCodeException(HttpStatusCode.BadRequest,
            //       JsonConvert.SerializeObject(errorList));
            //}

            //Validate business logic
            //if (!SubjectBodyMail.CheckIfAllowedUpdateStatus(request.Status, StatusCv))
            //{
            //    List<ErrorObject> errorList = new List<ErrorObject>();

            //    var objectError = new ErrorObject
            //    {
            //        Field = "Status",
            //        Message = $"Trạng thái không hợp lệ, {SubjectBodyMail.GetVietNameseText(request.Status)} không thể thành {SubjectBodyMail.GetVietNameseText(StatusCv)}"
            //    };
            //    errorList.Add(objectError);
            //    throw new HttpStatusCodeException(HttpStatusCode.BadRequest,
            //       JsonConvert.SerializeObject(errorList));

            //}

            //get name position
            List<Position> positions = await _poisitionRepository.ToListAsync();
            string positionName = positions.Where(position => position.Id == request.PositionId).First().PositionName;
            request.PositionName = positionName;

            ////update send mail status into not send
            request.Status = StatusCv;
            request.ColumnId = columnId;
            request.IsSendMail = Constants.EmailStatus.NOT_SENT;
            var cvReturn = _mapper.Map<CV>(request);

            //cv.IsSendMail = Constants.EmailStatus.SENDING;
            int resultUpdateSendMailStatus = await _cvRepository.UpdateAsync(cvReturn);

            var dto = _mapper.Map<CvDTO>(cvReturn);
            return dto;

        }


        public async Task<CvDTO> SendMailBaseOnStatus(int idCvNeedUpdate, CvDTO request)
        {
            // Validate putCvRequest business logic only when want to send mail
            PutCVRequestValidation validationRules = new PutCVRequestValidation(request.Status);
            var cv = _mapper.Map<CV>(request);
            var validationResult = validationRules.Validate(cv);

            if (!validationResult.IsValid)
            {
                List<ErrorObject> errorList = new List<ErrorObject>();

                foreach (var error in validationResult.Errors)
                {
                    var objectError = new ErrorObject
                    {
                        Field = error.PropertyName,
                        Message = error.ErrorMessage
                    };
                    errorList.Add(objectError);
                }
                //throw new HttpStatusCodeException(HttpStatusCode.BadRequest,
                //   JsonConvert.SerializeObject(errorList));
                //return BadRequest(errorList);
            }

            //update send mail status
            cv.IsSendMail = Constants.EmailStatus.SENDING;
            await _cvRepository.UpdateAsync(cv);

            //schedual mail sending
            cv.IsSendMail = Constants.EmailStatus.SENT;
            var scheduleDateTime = DateTime.UtcNow.AddSeconds(2);
            var dateTimeOffset = new DateTimeOffset(scheduleDateTime);

            //delete previous sending mail
            SendMail.DeleteExistingJobs(idCvNeedUpdate);

            //schedual job send mail with explicit time
            var jobSendMail = BackgroundJob.Schedule(() => SendMail.Send(new MailContent
            {
                IdCvNeedToSendMail = idCvNeedUpdate,
                MailTo = request.MailContent.MailTo,
                Subject = request.MailContent.Subject,
                Content = request.MailContent.Content,
                EmailsCC = request.MailContent.EmailsCC,
                EmailsBCC = request.MailContent.EmailsBCC
            }), dateTimeOffset);

            //After send mail job done, update CV again
            var jobUpdateCvStatus = BackgroundJob.ContinueJobWith(jobSendMail, () => _cvRepository.UpdateAsync(cv), JobContinuationOptions.OnlyOnSucceededState);
            return request;
        }

        public async Task<MailContent> GetEmailTemplate(string StatusCv, CvDTO request)
        {
            var filePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), @"Resources\EmailTemplates\EmailTemplate.html");
            var content = System.IO.File.ReadAllText(filePath);

            //get positionName base on positionId
            List<Position> positions = await _poisitionRepository.ToListAsync();
            string positionName = positions.Where(position => position.Id == request.PositionId).First().PositionName;
            request.PositionName = positionName;

            // Get subject and body base on statusCv received
            Dictionary<string, string> subjectAndBodyEmail = SubjectBodyMail.GetBodyByStatus(StatusCv, request);
            MailContent emailMessage = new MailContent
            {
                IdCvNeedToSendMail = (int)request.Id,
                MailTo = request.Email,
                Content = subjectAndBodyEmail["body"],
                Subject = subjectAndBodyEmail["subject"]
            };
            return emailMessage;
        }

        public async Task<int> UpdateManyAsync(List<CvDTO> entitiesToUpdate)
        {
            List<CV> Cvs = new List<CV>();
            if (entitiesToUpdate != null)
            {
                Cvs = _mapper.Map<List<CV>>(entitiesToUpdate);
            }
            var res = await _cvRepository.UpdateManyAsync(Cvs);
            return res;
        }
    }
}
