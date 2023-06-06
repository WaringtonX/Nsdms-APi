using AutoMapper;
using mersetaWebAPI.CustomModels.Task;
using mersetaWebAPI.Data;
using mersetaWebAPI.Interface;
using mersetaWebAPI.Models;
using mersetaWebAPI.Wrappers;
using Microsoft.EntityFrameworkCore;

namespace mersetaWebAPI.Implementation
{
    public class SiteService : ISite
    {
        private IConfiguration configuration;
        private mersetaContext MmersetaContext;
        private readonly IMapper _mapper;

        public SiteService(IConfiguration configuration, mersetaContext MmersetaContext, IMapper mapper)
        {
            this.configuration = configuration;
            this.MmersetaContext = MmersetaContext;
            this._mapper = mapper;
        }

        public async Task<Response<string>> CreateSiteVisit(SiteVisit sitevisit)
        {
            sitevisit.CreateDate = DateTime.Now;
            sitevisit.VisitDate = DateTime.Now;
;
            await MmersetaContext.SiteVisits.AddAsync(sitevisit);
            await MmersetaContext.SaveChangesAsync();
            return new Response<string>(sitevisit.Id.ToString(), "Added successfully");
        }

        public async Task<Response<List<SiteResponse>>> getCompanySite(long companyid)
        {
            var sites = await MmersetaContext.SiteResponses.Where(x => x.CompanyId == companyid).ToListAsync();
            return new Response<List<SiteResponse>>(sites);
        }

        public async Task<Response<SiteVisit>> GetSiteVisit(long assessmentbacthid)
        {
            var sitevisit = await MmersetaContext.SiteVisits.Where(x => x.AssessmentProcessBatchId == assessmentbacthid).FirstOrDefaultAsync();
            return new Response<SiteVisit>(sitevisit);
        }
    }
}
