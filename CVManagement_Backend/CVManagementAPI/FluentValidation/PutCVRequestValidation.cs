using CVManagementAPI.Helpers;
using CVManagementAPI.Models;
using FluentValidation;

namespace CVManagementAPI.FluentValidation
{
    public class PutCVRequestValidation : AbstractValidator<CV>
    {
        public PutCVRequestValidation(string statusCv)
        {
            RuleFor(x => x.DateOfInterview).NotEmpty().WithMessage("Date interview is required.").When(x => x.Status != Constants.StatusCv.REVIEW_CV_FAIL); ; // Validate Id is not empty, fail review mean no need date time
            RuleFor(x => x.TimeOfInterview).NotEmpty().WithMessage("Time interview is required.").When(x => x.Status != Constants.StatusCv.REVIEW_CV_FAIL); ; // Validate Id is not empty, fail review mean no need date time
            RuleFor(x => x.DateAcceptJob).NotEmpty().WithMessage("Date accept job is required.").When(x => statusCv == Constants.StatusCv.INTERVIEW_RES_PASS);
        }
    }

}

