using slTradeIn.DAL;
using slTradeIn.Data;

namespace slTradeIn.DataAccess
{
    public class Detail_TTU_userManualQuote_Data
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Ref_main_Data _mainData;

        public Detail_TTU_userManualQuote_Data(IUnitOfWork unitOfWork, Ref_main_Data mainData)
        {
            _unitOfWork = unitOfWork;
            _mainData = mainData;
        }

        public List<vw_TTU_ManualQuoteList> GetList(int iUserID)
        {

            return _unitOfWork.VwTTUManualQuoteListRepo.Get(q => q.iUserID == iUserID).ToList();

        }

        public Detail_TTU_userManualQuote GetInfo(int iManualQuoteID)
        {
            return _unitOfWork.DetailTTUUserManualQuoteRepo.Get(q => q.iManualQuoteID == iManualQuoteID).FirstOrDefault();

        }

        public int SaveManualQuote(Detail_TTU_userManualQuote info)
        {
            var Entity = _unitOfWork.DetailTTUUserManualQuoteRepo.Get(q => q.iManualQuoteID == info.iManualQuoteID).FirstOrDefault();
            if (Entity == null)
            {
                var lst = _mainData.GetListReference("iBoxStatusID");
                int? iBoxStatusID = null;
                if (lst.Count > 0)
                    iBoxStatusID = lst.OrderBy(q => q.iRefOrder).First().iRefMainID;

                Entity = new Detail_TTU_userManualQuote
                {
                    iUserID = info.iUserID,
                    vShippingType = info.vShippingType,
                    bSerializedReport = info.bSerializedReport,
                    dDateCreated = DateTime.Now,
                    iBoxStatusID = iBoxStatusID ?? 0
                };
                _unitOfWork.DetailTTUUserManualQuoteRepo.Insert(Entity);
            }
            else
            {
                Entity.vShippingType = info.vShippingType;
                Entity.bSerializedReport = info.bSerializedReport;
            }

            _unitOfWork.Save();
            info.iManualQuoteID = Entity.iManualQuoteID;
            return info.iManualQuoteID;

        }

        public void SendManualQuote(int iManualQuoteID = 0)
        {
            var lst = _mainData.GetListReference("iBoxStatusID");
            int? iBoxStatusID = null;

            var Stat = lst.Where(q => q.iRefOrder == 3).FirstOrDefault();
            if (Stat != null)
                iBoxStatusID = Stat.iRefMainID;

            var Entity = _unitOfWork.DetailTTUUserManualQuoteRepo.Get(q => q.iManualQuoteID == iManualQuoteID).FirstOrDefault();
            if (Entity != null)
            {

            }


        }
    }
}