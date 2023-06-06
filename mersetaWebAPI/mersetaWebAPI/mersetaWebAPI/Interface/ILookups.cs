using mersetaWebAPI.CustomModels;
using mersetaWebAPI.CustomModels.LookupsModels;
using mersetaWebAPI.Models;
using mersetaWebAPI.Wrappers;
using Task = mersetaWebAPI.Models.Task;

namespace mersetaWebAPI.Interface
{
    public interface ILookups
    {
        public Response<List<Province>> GetAllProvince();
        public Response<List<Town>> GetAllTown();
        public Response<List<Address>> GetAllAddress(int CompanyId);
        public Task<Response<List<Municipality>>> GetAllMunicipality();
        public Response<List<DisabilityRating>> GetAllDisabilityRating();
        public Response<List<DisabilityStatus>> GetAllDisabilityStatus();
        public Response<List<TvetFetQualification>> GetAllTvetFetQualification();
        public Response<List<OfoCode>> GetAllOfoCode();
        public Response<List<HighestQualificationRequired>> GetAllHighestQualificationRequired();
        public Response<List<SchoolResponse>> GetAllPreviousSchools();
        public Response<List<HighestEducation>> GetAllHighestEducation();
        public Response<List<Region>> GetAllRegions();
        public Response<List<RegionTown>> GetAllRegionTowns();
        public Response<List<Role>> GetAllUserRoles();
        public Task<Response<List<Title>>> GetAllTitles();


        public Response<List<Language>> GetAllLanguages();

        public Response<List<Learnership>> GetAllLearnership();
        public Task<Response<List<Nationality>>> GetAllCountries();
        public Task<Response<List<Gender>>> GetAllGender();
        public Task<Response<List<OrganisationType>>> GetAllOrganizationTypes();
        public Task<Response<List<SetaResponse>>> GetAllSetas();
        public Task<Response<List<LearningProgramTypeResponse>>> GetAllLearningProgramTypes();

        public Response<List<StatssaAreaCode>> GetAllStatssaAreaCode();
        //public Response<List<Seta>> GetAllStatssaAreaCode();

        public Response<List<Address>> GetAllUserAddress(int useraddressid);
        public Response<List<Company>> GetSingleCompanies(int id);

        public Response<List<SdfCompany>> GetSdfCompany(int companyid);
        public Response<List<SicCode>> GetSicCode(int sicode);

        public Response<List<Task>> GetAllTasks(int targetKey);

        public Response<List<LearnerApplicationComments>> GetApplicationComents(int comlearnID);

        public Response<List<ConfigDoc>> GetAllconfigDoc();

        public Response<List<Doc>> GetAllLearnerconfigDoc(int user_id, int target_key);

        public Response<List<DocByte>> GetAllUserDocuments(int docid);

        public string InsertChatComments(int comlearnID, DateTime date, int createid, string comment, ulong isdeleted, DateTime deletedon);

        public Response<List<Company>> GetAllCompaniesfoview();

        public Response<List<LearnerApplicationLifecycle>> GetRecommendationComments(int actionuser);

        public Task<Response<List<OccupationCategory>>> GetOccupations();
        public string ApprovalComments(int comID, DateTime Status_Date, int note, string commentnote, ulong isdeleted, DateTime deletedon,
          int ActionId, int companylearnerid);

        public Task<Response<List<LearnerAchievementStatus>>> GetAllLearnerAchievementStatus();
        public Task<Response<List<MaritalStatus>>> GetAllMaritalStatus();
        public Task<Response<List<Equity>>> GetAllEquityRace();
        public Task<Response<List<SocioeconomicStatusCode>>> GetAllSocioEconomicStatus();
        public Task<Response<List<CitizenResidentStatus>>> GetAllCitizenResidentStatus();
        public Task<Response<List<AlternativeIdType>>> GetAllAlternativeIdType();
        public Task<Response<List<AccreditationStatus>>> GetAllAccreditionStatuses();
        public Task<Response<SdpCompany>> getSdpCompany(int sdp_id);

        public Task<Response<Doc>> GetModeratorconfigDoc(int user_id, int target_key);

    }
}
