using CVManagementAPI.DTO;
using static CVManagementAPI.Helpers.Constants;

namespace CVManagementAPI.Helpers
{
    public class SubjectBodyMail
    {
        public static Dictionary<string, string> GetBodyByStatus(string status, CvDTO request)
        {
            string _subject = "";
            string _body = "";
            var fileHtml = Path.Combine(Directory.GetCurrentDirectory(), @"Resources\EmailTemplate.html");
            //var emailTemplate = System.IO.File.ReadAllText(emailTemplateFilePath);
            var res = new Dictionary<string,
              string> {
          {
            "subject",
            _subject
          },
          {
            "body",
            _body
          }
        };

            var statusToContentMap = new Dictionary<string,
              string> {
          {
            StatusCv.REVIEW_CV_FAIL, ContentMail.REVIEW_CV_FAIL
          },
          {
            StatusCv.REVIEW_CV_PASS,
            ContentMail.REVIEW_CV_PASS
          },
          {
            StatusCv.INTERVIEW_RES_FAIL,
            ContentMail.INTERVIEW_RES_FAIL
          },
          {
            StatusCv.INTERVIEW_RES_PASS,
            ContentMail.INTERVIEW_RES_PASS
          },
          {
            StatusCv.INTERVIEW_RES_BACKUP,
            ContentMail.INTERVIEW_RES_BACKUP
          }
        };

            var statusToSubjectMap = new Dictionary<string,
              string> {
          {
            StatusCv.REVIEW_CV_FAIL, SubjectMail.REVIEW_CV_FAIL
          },
          {
            StatusCv.REVIEW_CV_PASS,
            SubjectMail.REVIEW_CV_PASS
          },
          {
            StatusCv.INTERVIEW_RES_FAIL,
            SubjectMail.INTERVIEW_RES_FAIL
          },
          {
            StatusCv.INTERVIEW_RES_PASS,
            SubjectMail.INTERVIEW_RES_PASS
          },
          {
            StatusCv.INTERVIEW_RES_BACKUP,
            SubjectMail.INTERVIEW_RES_BACKUP
          }
        };

            if (statusToContentMap.TryGetValue(status, out string body))
            {
                body = body.Replace("PositionName", request.PositionName);
                body = body.Replace("AddressDetail", Address.AddressDetail);
                if (status == StatusCv.REVIEW_CV_PASS)
                {
                    body = body.Replace("TimeOfInterview", request.TimeOfInterview?.ToString());
                    body = body.Replace("DateOfInterview", request.DateOfInterview?.ToString("dd-MM-yyyy"));

                    //body = body.Replace("DateOfInterview", DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));
                }

                if (status == StatusCv.INTERVIEW_RES_PASS)
                {

                    body = body.Replace(@"dateAcceptJob", request.DateAcceptJob?.ToString("dd-MM-yyyy"));
                    body = body.Replace(@"PositionName", request.PositionName);
                }
                res["body"] = body;
            }

            if (statusToSubjectMap.TryGetValue(status, out string subject))
            {
                res["subject"] = subject;
            }

            return res;
        }


        public static List<string> GetAllowedUpdateStatus(string currentStatus)
        {
            switch (currentStatus)
            {
                case StatusCv.REVIEW_CV_WAITING:
                    return [
                      StatusCv.REVIEW_CV_FAIL,
                      StatusCv.REVIEW_CV_PASS,
                    ];

                case StatusCv.REVIEW_CV_FAIL:
                    return [currentStatus];

                case StatusCv.REVIEW_CV_PASS:
                    return [
                      StatusCv.INTERVIEW_RES_FAIL,
                      StatusCv.INTERVIEW_RES_PASS
                    ];

                case StatusCv.INTERVIEW_RES_PASS:
                    return [StatusCv.INTERVIEW_RES_BACKUP];

                case StatusCv.INTERVIEW_RES_FAIL:
                    return [currentStatus];

                case StatusCv.INTERVIEW_RES_BACKUP:
                    return [
                      StatusCv.INTERVIEW_RES_FAIL,
                      StatusCv.INTERVIEW_RES_PASS
                    ];

                default:
                    return [];
            }
        }
        public static bool CheckIfAllowedUpdateStatus(string currentStatus, string status)
        {
            return GetAllowedUpdateStatus(currentStatus).Contains(status);
        }

        public static string GetVietNameseText(string constant)
        {
            var cvConstantVn = new Dictionary<string, string>
    {
        { "REVIEW_CV_WAITING", "Đang chờ xem xét hồ sơ" },
        { "REVIEW_CV_FAIL", "Không đủ điều kiện" },
        { "REVIEW_CV_PASS", "Đủ điều kiện" },
        { "REFUSED_TO_BE_INTERVIEWED", "Từ chối phỏng vấn" },
        { "INTERVIEW_RES_FAIL", "Không đạt phỏng vấn" },
        { "INTERVIEW_RES_PASS", "Đạt phỏng vấn" },
        { "REFUSE_TO_ACCEPT_JOB", "Từ chối nhận việc" },
        { "GET_JOB", "Đã nhận việc" },
        { "INTERVIEW_RES_BACKUP", "Kết quả phỏng vấn dự phòng" },
        { "NOT_SENT", "Chưa gửi" },
        { "SENDING", "Đang gửi" },
        { "SENT", "Đã gửi" }
    };

            if (cvConstantVn.TryGetValue(constant, out var translation))
            {
                return translation;
            }
            else
            {
                return constant;
            }
        }
    }
}