﻿using System;
using System.Collections.Generic;

namespace mersetaWebAPI.Models
{
    public partial class UsersHist
    {
        public long Id { get; set; }
        public int Rev { get; set; }
        public sbyte? Revtype { get; set; }
        public ulong? AcceptPopi { get; set; }
        public ulong? Active { get; set; }
        public ulong? Admin { get; set; }
        public int? ApprovalEnum { get; set; }
        public ulong? Approved { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string? CellNumber { get; set; }
        public DateTime? ConfirmExpireDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Email { get; set; }
        public DateTime? EmailConfirmDate { get; set; }
        public string? EmailGuid { get; set; }
        public string? FaxNumber { get; set; }
        public ulong? Finance { get; set; }
        public string? FirstName { get; set; }
        public string? IosAndroid { get; set; }
        public DateTime? LastLogin { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? PassportNumber { get; set; }
        public string? Password { get; set; }
        public string? PhoneId { get; set; }
        public ulong? RecieveEmailTaskNotification { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string? RsaIdNumber { get; set; }
        public int? Status { get; set; }
        public ulong? SuperAdmin { get; set; }
        public string? TelNumber { get; set; }
        public int? UndertakenLearningProgramBefore { get; set; }
        public ulong? UploadData { get; set; }
        public string? Username { get; set; }
        public ulong? ViewBugs { get; set; }
        public ulong? ViewExtensionRequest { get; set; }
        public ulong? ViewUploadData { get; set; }
        public string? WorkContactNumber { get; set; }
        public long? DisabilityStatus { get; set; }
        public long? DisabledId { get; set; }
        public long? EquityId { get; set; }
        public long? GenderId { get; set; }
        public long? HomeLanguage { get; set; }
        public long? LastChgUserId { get; set; }
        public long? LearnershipId { get; set; }
        public int? MunicipalityId { get; set; }
        public long? NationalityId { get; set; }
        public long? OfficeId { get; set; }
        public long? PostalAddressId { get; set; }
        public long? ProfileImageId { get; set; }
        public long? QualificationId { get; set; }
        public long? ResidentialAddressId { get; set; }
        public long? TitleId { get; set; }
        public long? LegalGaurdianId { get; set; }
        public ulong? QualWorkplaceAssignment { get; set; }
        public ulong? TempReporting { get; set; }
        public ulong? AlloocateDg { get; set; }
        public int? EmploymentStatus { get; set; }
        public int? IdType { get; set; }
        public int? PartOfUnion { get; set; }
        public ulong? ProcessAssessorModApplication { get; set; }
        public ulong? ReviewCommitteeMember { get; set; }
        public int? UrbanRuralEnum { get; set; }
        public long? OrganisedLabourUnionMembershipId { get; set; }
        public long? UnionMembershipId { get; set; }
        public ulong? MancoMember { get; set; }
        public DateTime? EmploymentStartDate { get; set; }
        public ulong? LearnerReviewMember { get; set; }
        public string? TempRasIdOrPassportNumber { get; set; }
        public long? BirthAddressId { get; set; }
        public int? Maried { get; set; }
        public ulong? ProjectCreation { get; set; }
        public ulong? ProjectTermination { get; set; }
        public long? DisabilityRatingId { get; set; }
        public long? MaritalStatusId { get; set; }
        public long? OfoCodesId { get; set; }
        public ulong? CoursewareDistroSub { get; set; }
        public ulong? MancoGrantApproval { get; set; }
        public DateTime? LastSchoolYear { get; set; }
        public ulong? DgContractingBulkApproval { get; set; }
        public int? Disability { get; set; }
        public ulong? RegisterLearner { get; set; }
        public int? RsaCitizenType { get; set; }
        public ulong? DataExtracts { get; set; }
        public ulong? WspManagment { get; set; }
        public ulong? GpTransactionsManagement { get; set; }
        public DateTime? ValidiationRanDate { get; set; }
        public string? FirstNameAltered { get; set; }
        public string? FirstNameOrginal { get; set; }
        public string? MiddleNameAltered { get; set; }
        public string? MiddleNameOrginal { get; set; }
        public long? BirthTownId { get; set; }
        public ulong? AddAssessment { get; set; }
        public ulong? GenerateCertificates { get; set; }
        public long? PreviousSchools { get; set; }
        public ulong? SarsLevyDetailCalc { get; set; }
        public ulong? SarsLevyMandInvoicesUpdate { get; set; }
        public ulong? GpBankingDetails { get; set; }
        public ulong? LevyDetailMgTransactions { get; set; }
        public ulong? AlterLegacyApplications { get; set; }
        public ulong? QmrAccess { get; set; }
        public ulong? MoaRegisterAccess { get; set; }
        public ulong? CompanyRegisterReport { get; set; }
        public ulong? DgVerificationReport { get; set; }
        public ulong? GrantTrendAnylsis { get; set; }
        public ulong? MgVerificationReport { get; set; }
        public ulong? PrioirtySkillsReport { get; set; }
        public ulong? TaskReporting { get; set; }
        public ulong? NsdpReportConfig { get; set; }
        public ulong? NsdpReportEdit { get; set; }
        public ulong? NsdpReportView { get; set; }
        public string? MaidenName { get; set; }
        public ulong? WithdrawContracts { get; set; }
        public ulong? SspReporting { get; set; }
        public ulong? FundingWindow { get; set; }
        public ulong? HoldingRoom { get; set; }
        public ulong? StrategicPriorities { get; set; }
        public DateTime? AcceptPopiDate { get; set; }
        public string? CitizenResidentStatus { get; set; }
        public DateTime? DateStamp { get; set; }
        public DateTime? LearnerSubmissionDate { get; set; }
        public string? NextOfKinName { get; set; }
        public string? NextOfKinTelephone { get; set; }
        public string? Province { get; set; }
        public string? Secondaryemailaddress { get; set; }
        public string? CitizenResidentStatus1 { get; set; }
        public DateTime? Datestamp1 { get; set; }
        public DateTime? Learnersubmissiondate1 { get; set; }
        public string? Nextofkinname1 { get; set; }
        public string? Nextofkintelephone1 { get; set; }
        public string? Primaryemailaddress { get; set; }
        public string? PrimaryEmailAddress1 { get; set; }
        public string? SecondaryEmailAddress1 { get; set; }

        public virtual Revinfo RevNavigation { get; set; } = null!;
    }
}
