using mersetaWebAPI.Models;
using mersetaWebAPI.Wrappers;

namespace mersetaWebAPI.Interface
{
    public interface ISite
    {
        public Task<Response<string>> CreateSiteVisit(SiteVisit sitevisit);
        public Task<Response<SiteVisit>> GetSiteVisit(long assessmentbacthid);

        public Task<Response<List<SiteResponse>>> getCompanySite(long companyid);
    }
}
