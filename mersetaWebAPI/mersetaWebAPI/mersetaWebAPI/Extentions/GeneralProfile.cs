using AutoMapper;
using mersetaWebAPI.CustomModels.Assessments;
using mersetaWebAPI.CustomModels.Task;
using mersetaWebAPI.CustomModels.UserModels;
using mersetaWebAPI.Models;

namespace mersetaWebAPI.Extentions
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {

            CreateMap<UserUpdate, User>();
            CreateMap<User, UserUpdate>().ReverseMap();
            CreateMap<SummativeAssessmentReport, SummativeAssessmentRequest>();
            CreateMap<SummativeAssessmentRequest, SummativeAssessmentReport>().ReverseMap();
            CreateMap<SummativeAssessmentReportUnitStandardResponse, SummativeAssessmentReportUnitStandard>();
            CreateMap<SummativeAssessmentReportUnitStandard, SummativeAssessmentReportUnitStandardResponse>().ReverseMap();
            CreateMap<mersetaWebAPI.Models.Task, TaskResponse>().ReverseMap();

            CreateMap<ExternalModerationRequest, ExternalModerationValidation>();
            CreateMap<ExternalModerationValidation, ExternalModerationRequest>().ReverseMap();
        }
    }
}
