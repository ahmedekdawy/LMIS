using LMIS.Dal.Repositories;
using LMIS.Infrastructure.Interfaces.Repositories;

using Ninject;

namespace LMIS.Bll.Helpers
{
    public sealed class DalFactory
    {
        private static readonly DalFactory Instance = new DalFactory();
        private static readonly IKernel Kernel = new StandardKernel();
        private static readonly IGeneralCodeRepository GeneralCodeRepo;
        private static readonly ISubCodeRepository SubCodeRepo;
        private static readonly IOpportunityRepository OpportunityRepo;
        private static readonly IindividualDetailsRepository IndividualDetailsRepo;
        private static readonly IEventRepository EventRepo;
        private static readonly IJobRepository JobRepo;
        private static readonly ITrainingRepository TrainingRepo;
        private static readonly IDataServiceRepository DataServiceRepo;
        private static readonly IOrganizationContactInfoRepository OrganizationContactInfoRepo;
        private static readonly IOrganizationRepository OrganizationRepo;
        private static readonly IRequestLogRepository RequestLogRepo;
        private static readonly IPortalUsersRepository PortalUsersRepo;
        private static readonly INewsRepository NewsRepo;
        private static readonly IIndividualRepository IndividualRepo;
        private static readonly IConfigCenterRepository ConfigRepo;
        private static readonly IIndRepository IndRepo;
        private static readonly IOrgRepository OrgRepo;
        private static readonly IChatLogRepository ChatLogRepo;
        private static readonly IConceptNonFormalTrainingRepository ConceptNonFormalTrainingRepo;
        private static readonly ILmisReportsRepository LmisReportsRepo;
        private static readonly IObsceneWordsRepository ObsceneWordsRepo;
        private static readonly IListOfEmailsRepository ListOfEmailsRepo;
        private static readonly IHelpfulLinkRepository HelpfulLinkRepo;
        private static readonly IQualificationsRepository QualificationsRepo;
        private static readonly IOfficeRepository OfficeRepo;
        private static readonly IUnionRepository UnionRepo;
        private static readonly IFaqRepository FaqRepo;
        private static readonly IConceptsDefinitionsRepository ConceptsDefinitionsRepo;
        private static readonly IEmployersTrainingProvidersRepository EmployersTrainingProvidersRepo;
        private static readonly IRecruitmentAgenciesRepository RecruitmentAgenciesRepo;

        public static DalFactory Singleton { get { return Instance; } }
        public IGeneralCodeRepository GeneralCode { get { return GeneralCodeRepo; } }
        public ISubCodeRepository SubCode { get { return SubCodeRepo; } }
        public IOpportunityRepository Opportunity { get { return OpportunityRepo; } }
        public IindividualDetailsRepository IndividualDetails { get { return IndividualDetailsRepo; } }
        public IEventRepository Event { get { return EventRepo; } }
        public IJobRepository Job { get { return JobRepo; } }
        public ITrainingRepository Training { get { return TrainingRepo; } }
        public IDataServiceRepository DataService { get { return DataServiceRepo; } }
        public IOrganizationContactInfoRepository OrganizationContactInfo { get { return OrganizationContactInfoRepo; } }
        public IOrganizationRepository Organization { get { return OrganizationRepo; } }
        public IRequestLogRepository RequestLog { get { return RequestLogRepo; } }
        public IPortalUsersRepository PortalUsers { get { return PortalUsersRepo; } }
        public INewsRepository News { get { return NewsRepo; } }
        public IConfigCenterRepository ConfigCenter { get { return ConfigRepo; } }
        public IIndividualRepository Individual { get { return IndividualRepo; } }
        public IIndRepository Ind { get { return IndRepo; } }
        public IOrgRepository Org { get { return OrgRepo; } }
        public IChatLogRepository ChatLog { get { return ChatLogRepo; } }
        public IConceptNonFormalTrainingRepository ConceptNonFormalTraining { get { return ConceptNonFormalTrainingRepo; } }
        public ILmisReportsRepository LmisReports { get { return LmisReportsRepo; } }
        public IObsceneWordsRepository ObsceneWords { get { return ObsceneWordsRepo; } }
        public IListOfEmailsRepository ListOfEmails { get { return ListOfEmailsRepo; } }
        public IHelpfulLinkRepository HelpfulLink { get { return HelpfulLinkRepo; } }
        public IQualificationsRepository Qualifications { get { return QualificationsRepo; } }
        public IOfficeRepository Office { get { return OfficeRepo; } }
        public IUnionRepository Union { get { return UnionRepo; } }
        public IFaqRepository Faq { get { return FaqRepo; } }
        public IConceptsDefinitionsRepository ConceptsDefinitions { get { return ConceptsDefinitionsRepo; } }
        public IEmployersTrainingProvidersRepository EmployersTrainingProviders { get { return EmployersTrainingProvidersRepo; } }
        public IRecruitmentAgenciesRepository RecruitmentAgencies { get { return RecruitmentAgenciesRepo; } }

        static DalFactory()
        {
            //Define Ninject Dependency Injection Bindings
            Kernel.Bind<IGeneralCodeRepository>().To<GeneralCodeRepository>();
            Kernel.Bind<ISubCodeRepository>().To<SubCodeRepository>();
            Kernel.Bind<IOpportunityRepository>().To<OpportunityRepository>();
            Kernel.Bind<IindividualDetailsRepository>().To<IndividualDetailsRepository>();
            Kernel.Bind<IEventRepository>().To<EventRepository>();
            Kernel.Bind<IJobRepository>().To<JobRepository>();
            Kernel.Bind<ITrainingRepository>().To<TrainingRepository>();
            Kernel.Bind<IDataServiceRepository>().To<DataServiceRepository>();
            Kernel.Bind<IOrganizationContactInfoRepository>().To<OrganizationContactInfoRepository>();
            Kernel.Bind<IOrganizationRepository>().To<OrganizationRepository>();
            Kernel.Bind<IRequestLogRepository>().To<RequestLogRepository>();
            Kernel.Bind<IPortalUsersRepository>().To<PortalUsersRepository>();
            Kernel.Bind<INewsRepository>().To<NewsRepository>();
            Kernel.Bind<IConfigCenterRepository>().To<ConfigCenterRepository>();
            Kernel.Bind<IIndividualRepository>().To<IndividualRepository>();
            Kernel.Bind<IIndRepository>().To<IndRepository>();
            Kernel.Bind<IOrgRepository>().To<OrgRepository>();
            Kernel.Bind<IChatLogRepository>().To<ChatLogRepository>();
            Kernel.Bind<IConceptNonFormalTrainingRepository>().To<ConceptNonFormalTrainingRepository>();
            Kernel.Bind<ILmisReportsRepository>().To<LmisReportsRepository>();
            Kernel.Bind<IObsceneWordsRepository>().To<ObsceneWordsRepository>();
            Kernel.Bind<IListOfEmailsRepository>().To<ListOfEmailsRepository>();
            Kernel.Bind<IHelpfulLinkRepository>().To<HelpfulLinkRepository>();
            Kernel.Bind<IQualificationsRepository>().To<QualificationsRepository>();
            Kernel.Bind<IOfficeRepository>().To<OfficeRepository>();
            Kernel.Bind<IUnionRepository>().To<UnionRepository>();
            Kernel.Bind<IFaqRepository>().To<FaqRepository>();
            Kernel.Bind<IConceptsDefinitionsRepository>().To<ConceptsDefinitionsRepository>();
            Kernel.Bind<IEmployersTrainingProvidersRepository>().To<EmployersTrainingProvidersRepository>();
            Kernel.Bind<IRecruitmentAgenciesRepository>().To<RecruitmentAgenciesRepository>();

            //Serve BLL Objects as Singletons
            GeneralCodeRepo = Kernel.Get<IGeneralCodeRepository>();
            SubCodeRepo = Kernel.Get<ISubCodeRepository>();
            OpportunityRepo = Kernel.Get<IOpportunityRepository>();
            IndividualDetailsRepo = Kernel.Get<IindividualDetailsRepository>();
            EventRepo = Kernel.Get<IEventRepository>();
            JobRepo = Kernel.Get<IJobRepository>();
            TrainingRepo = Kernel.Get<ITrainingRepository>();
            DataServiceRepo = Kernel.Get<IDataServiceRepository>();
            OrganizationContactInfoRepo = Kernel.Get<IOrganizationContactInfoRepository>();
            OrganizationRepo = Kernel.Get<IOrganizationRepository>();
            RequestLogRepo = Kernel.Get<IRequestLogRepository>();
            PortalUsersRepo = Kernel.Get<IPortalUsersRepository>();
            NewsRepo = Kernel.Get<INewsRepository>();
            IndividualRepo = Kernel.Get<IIndividualRepository>();
            ConfigRepo = Kernel.Get<IConfigCenterRepository>();
            IndividualRepo = Kernel.Get<IIndividualRepository>();
            IndRepo = Kernel.Get<IIndRepository>();
            OrgRepo = Kernel.Get<IOrgRepository>();
            ChatLogRepo = Kernel.Get<IChatLogRepository>();
            ConceptNonFormalTrainingRepo = Kernel.Get<IConceptNonFormalTrainingRepository>();
            LmisReportsRepo = Kernel.Get<ILmisReportsRepository>();
            ObsceneWordsRepo = Kernel.Get<IObsceneWordsRepository>();
            ListOfEmailsRepo = Kernel.Get<IListOfEmailsRepository>();
            HelpfulLinkRepo = Kernel.Get<IHelpfulLinkRepository>();
            QualificationsRepo = Kernel.Get<IQualificationsRepository>();
            OfficeRepo = Kernel.Get<IOfficeRepository>();
            UnionRepo = Kernel.Get<IUnionRepository>();
            FaqRepo = Kernel.Get<IFaqRepository>();
            ConceptsDefinitionsRepo = Kernel.Get<IConceptsDefinitionsRepository>();
            EmployersTrainingProvidersRepo = Kernel.Get<IEmployersTrainingProvidersRepository>();
            RecruitmentAgenciesRepo = Kernel.Get<IRecruitmentAgenciesRepository>();
        }

        private DalFactory() { }
    }
}