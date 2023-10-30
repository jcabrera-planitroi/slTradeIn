using slTradeIn.Data;

namespace slTradeIn.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        public IGenericRepository<Detail_TTU_emailTemplate> DetailTTUEmailTemplateRepo { get; }
        public IGenericRepository<Detail_TTU_user> DetailTTUUserRepo { get; }
        public IGenericRepository<Detail_TTU_userLocation> DetailTTUUserLocationRepo { get; }
        public IGenericRepository<Detail_TTU_userCart> DetailTTUUserCartRepo { get; }
        public IGenericRepository<Ref_Cat> RefCatRepo { get; }
        public IGenericRepository<Ref_Main> RefMainRepo { get; }
        public IGenericRepository<Detail_TTU_userCartDetail> DetailTTUUserCartDetailRepo { get; }
        public IGenericRepository<vw_TTU_BoxList> VwTTUBoxListRepo { get; }
        public IGenericRepository<vw_TTU_ManualQuoteList> VwTTUManualQuoteListRepo { get; }
        public IGenericRepository<Detail_TTU_userEmail> DetailTTUUserEmailRepo { get; }
        public IGenericRepository<Detail_TTU_userEmailCampaign> DetailTTUUserEmailCampaignRepo { get; }
        public IGenericRepository<Detail_TTU_userEmailCampaignDetail> DetailTTUUserEmailCampaignDetailRepo { get; }
        public IGenericRepository<Detail_TTU_userManualQuote> DetailTTUUserManualQuoteRepo { get; }
        public IGenericRepository<Detail_TTU_userManualQuoteDetail> DetailTTUUserManualQuoteDetailRepo { get; }
        public IGenericRepository<Detail_TTU_userManualQuoteLocation> DetailTTUUserManualQuoteLocationRepo { get; }
        public IGenericRepository<Detail_ModelMaster> DetailModelMasterRepo { get; }
        public void Save();
        public new void Dispose();
    }
}
