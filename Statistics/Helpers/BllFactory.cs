using LMIS.Bll.Managers;
using LMIS.Dal.Repositories;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Infrastructure.Interfaces.Repositories;
using Ninject;

namespace Statistics.Helpers
{
    public sealed class BllFactory
    {
        private static readonly BllFactory Instance = new BllFactory();
        private static readonly IKernel Kernel = new StandardKernel();
        private static readonly IGeneralCodeManager GeneralCodeMgr;
        private static readonly ISubCodeManager SubCodeMgr;
        private static readonly IOpportunityManager OpportunityMgr;
        private static readonly IindividualDetailsManager IndividualDetailsMgr;
        private static readonly IEventManager EventMgr;
        private static readonly IJobManager JobMgr;
        private static readonly IDimThemesManager DimThemesMgr;
        private static readonly ITrainingManager TrainingMgr;
        private static readonly IDataServiceManager DataServiceMgr;
        private static readonly IThemesVariablesManager ThemesVariablesMgr;
        private static readonly IReportsManager ReportsMgr;
        private static readonly IFactStatisticalDataManager FactStatisticalDataMgr;
        private static readonly IAspNetUsersManager AspNetUsersMgr;
        private static readonly IPagesActionsManager PagesActionsMgr;
        private static readonly IPagesManager PagesMgr;
        private static readonly IConfigCenterManager ConfigCenterMgr;
        private static readonly IOrganizationContactInfoManager OrganizationContactInfoMgr;
        private static readonly IOrganizationManager OrganizationMgr;
        private static readonly IRequestLogManager RequestLogMgr;
        private static readonly IFeedbackManager FeedbackMgr;
        private static readonly ITestimonialsManager TestimonialsMgr;
        private static readonly IPortalUsersManager PortalUsersMgr;
        private static readonly INewsManager NewsMgr;
        private static readonly IIndividualManager IndividualMgr;
        private static readonly IPartnerManager PartnersMgr;
        private static readonly IIndManager IndMgr;
        private static readonly IOrgManager OrgMgr;
        private static readonly IChatLogManager ChatLogMgr;
        private static readonly IConceptNonFormalTrainingManager ConceptNonFormalTrainingMgr;
        private static readonly ILmisReportsRepository LmisReportsRepo;
        private static readonly IObsceneWordsManager ObsceneWordsRepo;
        private static readonly IListOfEmailsManager ListOfEmailsRepo;
        private static readonly IHelpfulLinkManager HelpfulLinkRepo;
        private static readonly IQualificationsManager QualificationsRepo;
        private static readonly IOfficeManager OfficeMgr;
        private static readonly IUnionManager UnionMgr;
        private static readonly IFaqManager FaqMgr;
        private static readonly IConceptsDefinitionsManager ConceptsDefinitionsMgr;
        private static readonly IEmployersTrainingProvidersManager EmployersTrainingProvidersMgr;
        private static readonly IRecruitmentAgenciesManager RecruitmentAgenciesMgr;

        public static BllFactory Singleton { get { return Instance; } }
        public IGeneralCodeManager GeneralCode { get { return GeneralCodeMgr; } }
        public ISubCodeManager SubCode { get { return SubCodeMgr; } }
        public IOpportunityManager Opportunity { get { return OpportunityMgr; } }
        public IindividualDetailsManager IndividualDetails { get { return IndividualDetailsMgr; } }
        public IEventManager Event { get { return EventMgr; } }
        public IJobManager Job { get { return JobMgr; } }
        public IDimThemesManager DimThemes { get { return DimThemesMgr; } }
        public ITrainingManager Training { get { return TrainingMgr; } }
        public IDataServiceManager DataService { get { return DataServiceMgr; } }
        public IThemesVariablesManager ThemesVariables { get { return ThemesVariablesMgr; } }
        public IReportsManager Reports { get { return ReportsMgr; } }
        public IFactStatisticalDataManager FactStatisticalDataManager { get { return FactStatisticalDataMgr; } }
        public IAspNetUsersManager AspNetUsersManager { get { return AspNetUsersMgr; } }
        public IPagesActionsManager PagesActionsManager { get { return PagesActionsMgr; } }
        public IPagesManager PagesManager { get { return PagesMgr; } }
        public IConfigCenterManager ConfigCenterManager { get { return ConfigCenterMgr; } }
        public IOrganizationContactInfoManager OrganizationContactInfo { get { return OrganizationContactInfoMgr; } }
        public IOrganizationManager Organization { get { return OrganizationMgr; } }
        public IRequestLogManager RequestLog { get { return RequestLogMgr; } }
        public IFeedbackManager FeedbackManager { get { return FeedbackMgr; } }
        public ITestimonialsManager TestimonialsManager { get { return TestimonialsMgr; } }
        public IPortalUsersManager PortalUsersManager { get { return PortalUsersMgr; } }
        public INewsManager NewsManager { get { return NewsMgr; } }
        public IIndividualManager IndividualManager{ get { return IndividualMgr; } }
        public IPartnerManager PartnersManager { get { return PartnersMgr; } }
        public IIndManager Ind { get { return IndMgr; } }
        public IOrgManager Org { get { return OrgMgr; } }
        public IChatLogManager ChatLog { get { return ChatLogMgr; } }
        public IConceptNonFormalTrainingManager ConceptNonFormalTraining { get { return ConceptNonFormalTrainingMgr; } }
        public ILmisReportsRepository LmisReports { get { return LmisReportsRepo; } }
        public IObsceneWordsManager ObsceneWords { get { return ObsceneWordsRepo; } }
        public IListOfEmailsManager ListOfEmails { get { return ListOfEmailsRepo; } }
        public IHelpfulLinkManager HelpfulLink { get { return HelpfulLinkRepo; } }
        public IQualificationsManager Qualifications { get { return QualificationsRepo; } }
        public IOfficeManager Office { get { return OfficeMgr; } }
        public IUnionManager Union { get { return UnionMgr; } }
        public IFaqManager Faq { get { return FaqMgr; } }
        public IConceptsDefinitionsManager ConceptsDefinitions { get { return ConceptsDefinitionsMgr; } }
        public IEmployersTrainingProvidersManager EmployersTrainingProviders { get { return EmployersTrainingProvidersMgr; } }
        public IRecruitmentAgenciesManager RecruitmentAgencies { get { return RecruitmentAgenciesMgr; } }

        static BllFactory()
        {
            //ASP.NET Identity

            //Define Ninject Dependency Injection Bindings
            Kernel.Bind<IGeneralCodeManager>().To<GeneraCodeManager>();
            Kernel.Bind<ISubCodeManager>().To<SubCodeManager>();
            Kernel.Bind<IOpportunityManager>().To<OpportunityManager>();
            Kernel.Bind<IindividualDetailsManager>().To<IndividualDetailsManager>();
            Kernel.Bind<IEventManager>().To<EventManager>();
            Kernel.Bind<IJobManager>().To<JobManager>();
            Kernel.Bind<IDimThemesManager>().To<DimThemesManager>();
            Kernel.Bind<ITrainingManager>().To<TrainingManager>();
            Kernel.Bind<IDataServiceManager>().To<DataServiceManager>();
            Kernel.Bind<IThemesVariablesManager>().To<ThemesVariablesManager>();
            Kernel.Bind<IReportsManager>().To<ReportsManager>();
            Kernel.Bind<IFactStatisticalDataManager>().To<FactStatisticalDataManager>();
            Kernel.Bind<IAspNetUsersManager>().To<AspNetUsersManager>();
            Kernel.Bind<IPagesActionsManager>().To<PagesActionsManager>();
            Kernel.Bind<IPagesManager>().To<PagesManager>();
            Kernel.Bind<IConfigCenterManager>().To<ConfigCenterManager>();
            Kernel.Bind<IOrganizationContactInfoManager>().To<OrganizationContactInfoManager>();
            Kernel.Bind<IOrganizationManager>().To<OrganizationManager>();
            Kernel.Bind<IRequestLogManager>().To<RequestLogManager>();
            Kernel.Bind<IFeedbackManager>().To<FeedbackManager>();
            Kernel.Bind<ITestimonialsManager>().To<TestimonialsManager>();
            Kernel.Bind<IPortalUsersManager>().To<PortalUsersManager>();
            Kernel.Bind<INewsManager>().To<NewsManager>();
            Kernel.Bind<IIndividualManager>().To<IndividualManager>();
            Kernel.Bind<IPartnerManager>().To<PartnerManager>();
            Kernel.Bind<IIndManager>().To<IndManager>();
            Kernel.Bind<IOrgManager>().To<OrgManager>();
            Kernel.Bind<IChatLogManager>().To<ChatLogManager>();
            Kernel.Bind<IConceptNonFormalTrainingManager>().To<ConceptNonFormalTrainingManager>();
            Kernel.Bind<ILmisReportsRepository>().To<LmisReportsRepository>();
            Kernel.Bind<IObsceneWordsManager>().To<ObsceneWordsManager>();
            Kernel.Bind<IListOfEmailsManager>().To<ListOfEmailsManager>();
            Kernel.Bind<IHelpfulLinkManager>().To<HelpfulLinkManager>();
            Kernel.Bind<IQualificationsManager>().To<QualificationsManager>();
            Kernel.Bind<IOfficeManager>().To<OfficeManager>();
            Kernel.Bind<IUnionManager>().To<UnionManager>();
            Kernel.Bind<IFaqManager>().To<FaqManager>();
            Kernel.Bind<IConceptsDefinitionsManager>().To<ConceptsDefinitionsManager>();
            Kernel.Bind<IEmployersTrainingProvidersManager>().To<EmployersTrainingProvidersManager>();
            Kernel.Bind<IRecruitmentAgenciesManager>().To<RecruitmentAgenciesManager>();

            //Serve BLL Objects as Singletons
            GeneralCodeMgr = Kernel.Get<IGeneralCodeManager>();
            SubCodeMgr = Kernel.Get<ISubCodeManager>();
            OpportunityMgr = Kernel.Get<IOpportunityManager>();
            IndividualDetailsMgr = Kernel.Get<IindividualDetailsManager>();
            EventMgr = Kernel.Get<IEventManager>();
            JobMgr = Kernel.Get<IJobManager>();
            DimThemesMgr = Kernel.Get<IDimThemesManager>();
            TrainingMgr = Kernel.Get<ITrainingManager>();
            DataServiceMgr = Kernel.Get<IDataServiceManager>();
            ThemesVariablesMgr = Kernel.Get<IThemesVariablesManager>();
            ReportsMgr = Kernel.Get<IReportsManager>();
            FactStatisticalDataMgr = Kernel.Get<IFactStatisticalDataManager>();
            AspNetUsersMgr = Kernel.Get<IAspNetUsersManager>();
            PagesActionsMgr = Kernel.Get<IPagesActionsManager>();
            PagesMgr = Kernel.Get<IPagesManager>();
            ConfigCenterMgr  = Kernel.Get<IConfigCenterManager>();
            OrganizationContactInfoMgr = Kernel.Get<IOrganizationContactInfoManager>();
            OrganizationMgr = Kernel.Get<IOrganizationManager>();
            RequestLogMgr = Kernel.Get<IRequestLogManager>();
            FeedbackMgr = Kernel.Get<IFeedbackManager>();
            TestimonialsMgr = Kernel.Get<ITestimonialsManager>();
            PortalUsersMgr = Kernel.Get<IPortalUsersManager>();
            NewsMgr = Kernel.Get<INewsManager>();
            IndividualMgr = Kernel.Get<IIndividualManager>();
            PartnersMgr = Kernel.Get<IPartnerManager>();
            IndMgr = Kernel.Get<IIndManager>();
            OrgMgr = Kernel.Get<IOrgManager>();
            ChatLogMgr = Kernel.Get<IChatLogManager>();
            ConceptNonFormalTrainingMgr = Kernel.Get<IConceptNonFormalTrainingManager>();
            LmisReportsRepo = Kernel.Get<ILmisReportsRepository>();
            ObsceneWordsRepo = Kernel.Get<IObsceneWordsManager>();
            ListOfEmailsRepo = Kernel.Get<IListOfEmailsManager>();
            HelpfulLinkRepo = Kernel.Get<IHelpfulLinkManager>();
            QualificationsRepo = Kernel.Get<IQualificationsManager>();
            OfficeMgr = Kernel.Get<IOfficeManager>();
            UnionMgr = Kernel.Get<IUnionManager>();
            FaqMgr = Kernel.Get<IFaqManager>();
            ConceptsDefinitionsMgr = Kernel.Get<IConceptsDefinitionsManager>();
            EmployersTrainingProvidersMgr = Kernel.Get<IEmployersTrainingProvidersManager>();
            RecruitmentAgenciesMgr = Kernel.Get<IRecruitmentAgenciesManager>();
        }

        private BllFactory() {}
    }
}