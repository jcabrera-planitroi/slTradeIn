using slTradeIn.Data;

namespace slTradeIn.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PlanITVisionContext _context;
        public IGenericRepository<Detail_TTU_emailTemplate>? DetailTTUEmailTemplateRepo { get; }
        public IGenericRepository<Detail_TTU_user>? DetailTTUUserRepo { get; }
        public IGenericRepository<Detail_TTU_userLocation>? DetailTTUUserLocationRepo { get; }
        public IGenericRepository<Detail_TTU_userCart>? DetailTTUUserCartRepo { get; }
        public IGenericRepository<Ref_Main>? RefMainRepo { get; }
        public IGenericRepository<Ref_Cat>? RefCatRepo { get; }
        public IGenericRepository<Detail_TTU_userCartDetail>? DetailTTUUserCartDetailRepo { get; }
        public IGenericRepository<vw_TTU_BoxList>? VwTTUBoxListRepo { get; }
        public IGenericRepository<vw_TTU_ManualQuoteList> VwTTUManualQuoteListRepo { get; }
        public IGenericRepository<Detail_TTU_userEmail>? DetailTTUUserEmailRepo { get; }
        public IGenericRepository<Detail_TTU_userEmailCampaign>? DetailTTUUserEmailCampaignRepo { get; }
        public IGenericRepository<Detail_TTU_userEmailCampaignDetail> DetailTTUUserEmailCampaignDetailRepo { get; }
        public IGenericRepository<Detail_TTU_userManualQuote>? DetailTTUUserManualQuoteRepo { get; }
        public IGenericRepository<Detail_TTU_userManualQuoteDetail>? DetailTTUUserManualQuoteDetailRepo { get; }
        public IGenericRepository<Detail_TTU_userManualQuoteLocation>? DetailTTUUserManualQuoteLocationRepo { get; }
        public IGenericRepository<Detail_ModelMaster> DetailModelMasterRepo { get; }

        public UnitOfWork(PlanITVisionContext context, IGenericRepository<Detail_TTU_emailTemplate>? detailTTUEmailTemplateRepo, IGenericRepository<Detail_TTU_user>? detailTTUUserRepo, IGenericRepository<Detail_TTU_userLocation>? detailTTUUserLocationRepo, IGenericRepository<Detail_TTU_userCart>? detailTTUUserCartRepo, IGenericRepository<Ref_Main>? refMainRepo, IGenericRepository<Ref_Cat>? refCatRepo, IGenericRepository<Detail_TTU_userCartDetail>? detailTTUUserCartDetailRepo, IGenericRepository<vw_TTU_BoxList>? vwTTUBoxListRepo, IGenericRepository<vw_TTU_ManualQuoteList> vwTTUManualQuoteListRepo, IGenericRepository<Detail_TTU_userEmail>? detailTTUUserEmailRepo, IGenericRepository<Detail_TTU_userEmailCampaign>? detailTTUUserEmailCampaignRepo, IGenericRepository<Detail_TTU_userEmailCampaignDetail> detailTTUUserEmailCampaignDetailRepo, IGenericRepository<Detail_TTU_userManualQuote>? detailTTUUserManualQuoteRepo, IGenericRepository<Detail_TTU_userManualQuoteDetail>? detailTTUUserManualQuoteDetailRepo, IGenericRepository<Detail_TTU_userManualQuoteLocation>? detailTTUUserManualQuoteLocationRepo, IGenericRepository<Detail_ModelMaster> detailModelMasterRepo)
        {
            DetailTTUEmailTemplateRepo = detailTTUEmailTemplateRepo;
            DetailTTUUserRepo = detailTTUUserRepo;
            DetailTTUUserLocationRepo = detailTTUUserLocationRepo;
            DetailTTUUserCartRepo = detailTTUUserCartRepo;
            RefMainRepo = refMainRepo;
            RefCatRepo = refCatRepo;
            DetailTTUUserCartDetailRepo = detailTTUUserCartDetailRepo;
            VwTTUBoxListRepo = vwTTUBoxListRepo;
            VwTTUManualQuoteListRepo = vwTTUManualQuoteListRepo;
            DetailTTUUserEmailRepo = detailTTUUserEmailRepo;
            DetailTTUUserEmailCampaignRepo = detailTTUUserEmailCampaignRepo;
            DetailTTUUserEmailCampaignDetailRepo = detailTTUUserEmailCampaignDetailRepo;
            DetailTTUUserManualQuoteRepo = detailTTUUserManualQuoteRepo;
            DetailTTUUserManualQuoteDetailRepo = detailTTUUserManualQuoteDetailRepo;
            DetailTTUUserManualQuoteLocationRepo = detailTTUUserManualQuoteLocationRepo;
            DetailModelMasterRepo = detailModelMasterRepo;
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
