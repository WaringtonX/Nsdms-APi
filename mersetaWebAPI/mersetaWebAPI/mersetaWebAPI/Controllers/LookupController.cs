using mersetaWebAPI.Data;
using mersetaWebAPI.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace mersetaWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LookupController : Controller
    {
        private readonly mersetaContext MmersetaContext;
        // MySqlConnection conn = new MySqlConnection("server=156.38.134.211;user id=merseta;password=M3rseta@345;port=3306;database=merseta;T");
        private ILookups lookups;
        public LookupController(mersetaContext MmersetaContext, ILookups _lookups)
        {
            this.MmersetaContext = MmersetaContext;
            lookups = _lookups;
        }
        //get all provinces
        [HttpGet("Province")]
        //[Authorize]
        public async Task<ActionResult> GetAllProvinces()
        {
            var _lookup = lookups.GetAllProvince();
            if (_lookup.Succeeded)
            {
                return Ok(_lookup.Data);
            }
            return NotFound("Provinces not found");
        }
        [HttpGet("Countries")]
        //[Authorize]
        public async Task<ActionResult> GetAllCountries()
        {
            var _lookup =await lookups.GetAllCountries();
            if (_lookup.Succeeded)
            {
                return Ok(_lookup.Data);
            }
            return NotFound("Coutries not found");
        }
        [HttpGet("Genders")]
        //[Authorize]
        public async Task<ActionResult> GetAllGenders()
        {
            var _lookup = await lookups.GetAllGender();
            if (_lookup.Succeeded)
            {
                return Ok(_lookup.Data);
            }
            return NotFound("Coutries not found");
        }
        [HttpGet("OrganizationTypes")]
        //[Authorize]
        public async Task<ActionResult> GetAllOrganizationTypes()
        {
            var _lookup = await lookups.GetAllOrganizationTypes();
            if (_lookup.Succeeded)
            {
                return Ok(_lookup.Data);
            }
            return NotFound("Coutries not found");
        }

        [HttpGet("Setas")]
        //[Authorize]
        public async Task<ActionResult> GetAllSetas()
        {
            var _lookup = await lookups.GetAllSetas();
            if (_lookup.Succeeded)
            {
                return Ok(_lookup.Data);
            }
            return NotFound("Coutries not found");
        }
        [HttpGet("LearningPrograms")]
        //[Authorize]
        public async Task<ActionResult> GetAllLearningPrograms()
        {
            var _lookup = await lookups.GetAllLearningProgramTypes();
            if (_lookup.Succeeded)
            {
                return Ok(_lookup.Data);
            }
            return NotFound("Coutries not found");
        }
        //get all addresses
        [HttpGet("Addresses")]
        public async Task<ActionResult> GetAllAddress(int CompanyId)
        {
            var _address = lookups.GetAllAddress(CompanyId);
            if (_address.Succeeded)
            {
                return Ok(_address.Data);
            }
            return NotFound("Address not found");
        }
        //get all Towns
        [HttpGet("Towns")]
        public async Task<ActionResult> GetAllTowns()
        {
            var _towns = lookups.GetAllTown();
            if (_towns.Succeeded)
            {
                return Ok(_towns.Data);
            }
            return NotFound("Towns not found");
        }

        //get all Municipalities
        [HttpGet("Municipalities")]
        public async Task<ActionResult> GetAllMunicipality()
        {
            var _municipality =await lookups.GetAllMunicipality();
            if (_municipality.Succeeded)
            {
                return Ok(_municipality.Data);
            }
            return NotFound("Municipality not found");
        }
        //get all Disabilty Rating
        [HttpGet("DisabiltyRating")]
        public async Task<ActionResult> GetAllDisabiltyRating()
        {
            var _disabiltyRating = lookups.GetAllDisabilityRating();
            if (_disabiltyRating.Succeeded)
            {
                return Ok(_disabiltyRating.Data);
            }
            return NotFound("Disabilty Rating not found");
        }
        //get all Disabilty Status
        [HttpGet("DisabiltyStatus")]
        public async Task<ActionResult> GetAllDisabiltyStatus()
        {
            var _disabiltyStatus = lookups.GetAllDisabilityStatus();
            if (_disabiltyStatus.Succeeded)
            {
                return Ok(_disabiltyStatus.Data);
            }
            return NotFound("Disabilty Status  not found");
        }

        //get all Tvet Fet Qualification
        [HttpGet("TvetFetQualification")]
        public async Task<ActionResult> GetAllTvetFetQualification()
        {
            var _tvetFetQualification = lookups.GetAllTvetFetQualification();
            if (_tvetFetQualification.Succeeded)
            {
                return Ok(_tvetFetQualification.Data);
            }
            return NotFound("Tvet Fet Qualification  not found");
        }

        //get all OfoCode
        [HttpGet("OfoCode")]
        
        public async Task<ActionResult> GetAllOfoCode()
        {
            var _getAllOfoCode = lookups.GetAllOfoCode();
            if (_getAllOfoCode.Succeeded)
            {
                return Ok(JsonSerializer.Serialize(_getAllOfoCode.Data));
            }
            return NotFound("OfoCode  not found");
        }

        //get all GetAllHighestQualificationRequired
        [HttpGet("HighestQualificationRequired")]
        public async Task<ActionResult> GetAllHighestQualificationRequired()
        {
            var _highestQualificationRequired = lookups.GetAllHighestQualificationRequired();
            if (_highestQualificationRequired.Succeeded)
            {
                return Ok(_highestQualificationRequired.Data);
            }
            return NotFound("_highestQualificationRequired  not found");
        }
        //get all GetAllHighestQualificationRequired
        [HttpGet("PreviousSchools")]
        public async Task<ActionResult> GetAllPreviousSchools()
        {
            var _previousSchools = lookups.GetAllPreviousSchools();
            if (_previousSchools.Succeeded)
            {
                return Ok(_previousSchools.Data);
            }
            return NotFound("_highestQualificationRequired  not found");
        }
        //get all Region
        [HttpGet("Region")]
        public async Task<ActionResult> GetAllRegions()
        {
            var _region = lookups.GetAllRegions();
            if (_region.Succeeded)
            {
                return Ok(_region.Data);
            }
            return NotFound("Region  not found");
        }

        //get all Region Town
        [HttpGet("RegionTown")]
        public async Task<ActionResult> GetAllRegionTowns()
        {
            var _regionTowns = lookups.GetAllRegionTowns();
            if (_regionTowns.Succeeded)
            {
                return Ok(_regionTowns.Data);
            }
            return NotFound("Region Town  not found");
        }
        //get all highest Education
        [HttpGet("HighestEducation")]
        public async Task<ActionResult> GetAllHighestEducation()
        {
            var _highestEducation = lookups.GetAllHighestEducation();
            if (_highestEducation.Succeeded)
            {
                return Ok(_highestEducation.Data);
            }
            return NotFound("Highest Education  not found");
        }
        //get all highest Marital status
        [HttpGet("MaritalStatus")]
        public async Task<ActionResult> GetAllMaritalStatus()
        {
            var _maritalstatus =await lookups.GetAllMaritalStatus();
            if (_maritalstatus.Succeeded)
            {
                return Ok(_maritalstatus.Data);
            }
            return NotFound("Marital Status  not found");
        }
        //get all Roles
        [HttpGet("Role")]
        public async Task<ActionResult> GetAllUserRoles()
        {
            var _role = lookups.GetAllUserRoles();
            if (_role.Succeeded)
            {
                return Ok(_role.Data);
            }
            return NotFound("User Roles not found");
        }

        //get all Languages
        [HttpGet("Languages")]
        public async Task<ActionResult> GetAllLanguages()
        {
            var _languages = lookups.GetAllLanguages();
            if (_languages.Succeeded)
            {
                return Ok(_languages.Data);
            }
            return NotFound("Region Town  not found");
        }

        //get all Learnership
        [HttpGet("Learnership")]
        public async Task<ActionResult> GetAllLearnership()
        {
            var _learn = lookups.GetAllLearnership();
            if (_learn.Succeeded)
            {
                return Ok(_learn.Data);
            }
            return NotFound("Region Town  not found");
        }

        //get all Learnership
        [HttpGet("StatssaAreaCode")]
        public async Task<ActionResult> GetAllStatssaAreaCode()
        {
            var _seta = lookups.GetAllStatssaAreaCode();
            if (_seta.Succeeded)
            {
                return Ok(_seta.Data);
            }
            return NotFound("Region Town  not found");
        }

        //get all user address
        [HttpGet("UserAddress")]
        public async Task<ActionResult> GetAllUserAddress(int useraddressid)
        {
            var _seta = lookups.GetAllUserAddress(useraddressid);
            if (_seta.Succeeded)
            {
                return Ok(_seta.Data);
            }
            return NotFound("Region Town  not found");
        }

        //get all user address
        [HttpGet("SingleCompany")]
        public async Task<ActionResult> GetSingleCompany(int id)
        {
            var _seta = lookups.GetSingleCompanies(id);
            if (_seta.Succeeded)
            {
                return Ok(_seta.Data);
            }
            return NotFound("Region Town  not found");
        }

        //get all user address
        [HttpGet("SdfCompany")]
        public async Task<ActionResult> GetSdfCompany(int companyId)
        {
            var _seta = lookups.GetSdfCompany(companyId);
            if (_seta.Succeeded)
            {
                return Ok(_seta.Data);
            }
            return NotFound("Region Town  not found");
        }

        //get all user address
        [HttpGet("SicCode")]
        public async Task<ActionResult> GetSicCode(int scid)
        {
            var _seta = lookups.GetSicCode(scid);
            if (_seta.Succeeded)
            {
                return Ok(_seta.Data);
            }
            return NotFound("Region Town  not found");
        }

        //get all user address
        [HttpGet("UserTaks")]
        public async Task<ActionResult> GetAllUserTaks(int targetkey)
        {
            var _task = lookups.GetAllTasks(targetkey);
            if (_task.Succeeded)
            {
                return Ok(_task.Data);
            }
            return NotFound("Region Town  not found");
        }
        //get approval comments
        [HttpGet("LearnerApplicationLifecycle")]
        public async Task<ActionResult> GetRecommendationComments(int apcomID)
        {
            var _com = lookups.GetRecommendationComments(apcomID);
            if (_com.Succeeded)
            {
                return Ok(_com.Data);
            }
            return NotFound("Region Town  not found");
        }
        //get all user address
        [HttpGet("ApplicationComments")]
        public async Task<ActionResult> GetAllApplicationComments(int compid)
        {
            var _com = lookups.GetApplicationComents(compid);
            if (_com.Succeeded)
            {
                return Ok(_com.Data);
            }
            return NotFound("Region Town  not found");
        }
        //get all approval address
        [HttpPost("ApprovalComments")]
        public async Task<ActionResult> ApprovalComments(int comID, DateTime Status_Date, int notify, string commentnote, ulong isdeleted, DateTime deletedon,
            int ActionId, int companylearnerId)
        {//(int apcomlearnID, DateTime statusdate, int snetNotification, string apcomment, ulong apisdeleted, DateTime apdeletedon, int actionuser)
            var _com = lookups.ApprovalComments(comID, Status_Date, notify, commentnote, isdeleted, deletedon, ActionId, companylearnerId);

            return NotFound("Success");
        }
        //get all user address
        [HttpPost("Addcomments")]
        public async Task<ActionResult> Addcomments(int comlearnID, DateTime date, int createid, string comment, ulong isdeleted, DateTime deletedon)
        {
            var _com = lookups.InsertChatComments(comlearnID, date, createid, comment, isdeleted, deletedon);

            return NotFound("Success");
        }

        //get all user address
        [HttpGet("GetallConfigDoc")]
        public async Task<ActionResult> GetAllconfigDoc()
        {
            var _task = lookups.GetAllconfigDoc();
            if (_task.Succeeded)
            {
                return Ok(_task.Data);
            }
            return NotFound("Region Town  not found");
        }

        //get all user address
        [HttpGet("GetAllLearnerDocs")]
        public async Task<ActionResult> GetAllLearnerconfigDoc(int user_id, int target_key)
        {
            var _task = lookups.GetAllLearnerconfigDoc(user_id, target_key);
            if (_task.Succeeded)
            {
                return Ok(_task.Data);
            }
            return NotFound("Region Town  not found");
        }

        //get all user address
        [HttpGet("GetAllUserDocuments")]
        public async Task<ActionResult> GetAllUserDocuments(int doc_id)
        {
            var _task = lookups.GetAllUserDocuments(doc_id);
            if (_task.Succeeded)
            {
                return Ok(_task.Data);
            }
            return NotFound("Region Town  not found");
        }

        //get all users
        [HttpGet]
        [HttpGet("GetAllCompanyforview")]
        public async Task<ActionResult> GetAllCompanyforview()
        {
            var company = lookups.GetAllCompaniesfoview();
            if (company.Succeeded)
            {
                return Ok(company.Data);
            }
            return NotFound("User not found");
        }

        //get all Titles
        
        [HttpGet("GetAllTitles")]
        public async Task<ActionResult> GetAllTitle()
        {
            var titles =await lookups.GetAllTitles();
            if (titles.Succeeded)
            {
                return Ok(titles.Data);
            }
            return NotFound("User not found");
        }

        //get all Titles

        [HttpGet("GetOccupations")]
        public async Task<ActionResult> GetOccupations()
        {
            var titles = await lookups.GetOccupations();
            if (titles.Succeeded)
            {
                return Ok(titles.Data);
            }
            return NotFound("Occupations not found");
        }

        //get all LearnerAchievementStatus
        [HttpGet("Learner_achievement_status")]
        public async Task<ActionResult> GetAllLearner_achievement_status()
        {
            var LearnerAchievementStatus =await lookups.GetAllLearnerAchievementStatus();
            if (LearnerAchievementStatus.Succeeded)
            {
                return Ok(LearnerAchievementStatus.Data);
            }
            return NotFound("Learner_achievement_status not found");
        }

        [HttpGet("GetSdpCompany")]
        public async Task<ActionResult> GetSdpCompany(int sdpid)
        {
            var _seta = await lookups.getSdpCompany(sdpid);
            if (_seta.Succeeded)
            {
                return Ok(_seta.Data);
            }
            return NotFound("Sdp not found");
        }


        [HttpGet("GetAllRaces")]
        public async Task<ActionResult> GetAllRaces()
        {
            var _seta = await lookups.GetAllEquityRace();
            if (_seta.Succeeded)
            {
                return Ok(_seta.Data);
            }
            return NotFound("Races not found");
        }

        [HttpGet("GetModeratorconfigDoc")]
        public async Task<ActionResult> GetModeratorconfigDoc(int user_id, int target_key)
        {
            var _seta = await lookups.GetModeratorconfigDoc(user_id, target_key);
            if (_seta.Succeeded)
            {
                return Ok(_seta.Data);
            }
            return NotFound("Sdp not found");
        }

        [HttpGet("GetAlternativeIdType")]
        public async Task<ActionResult> GetLternativeIdType()
        {
            var _seta = await lookups.GetAllAlternativeIdType();
            if (_seta.Succeeded)
            {
                return Ok(_seta.Data);
            }
            return NotFound("Sdp not found");
        }

        [HttpGet("GetAllSocioEconomics")]
        public async Task<ActionResult> GetAllSocioEconomics()
        {
            var _seta = await lookups.GetAllSocioEconomicStatus();
            if (_seta.Succeeded)
            {
                return Ok(_seta.Data);
            }
            return NotFound("Sdp not found");
        }
        [HttpGet("GetAllCitizenResidentStatus")]
        public async Task<ActionResult> GetAllCitizenResidentStatus()
        {
            var _seta = await lookups.GetAllCitizenResidentStatus();
            if (_seta.Succeeded)
            {
                return Ok(_seta.Data);
            }
            return NotFound("Sdp not found");
        }

        [HttpGet("GetAllAccreditationStatus")]
        public async Task<ActionResult> GetAllAccreditationStatus()
        {
            var _seta = await lookups.GetAllAccreditionStatuses();
            if (_seta.Succeeded)
            {
                return Ok(_seta.Data);
            }
            return NotFound("Sdp not found");
        }
    }
}
