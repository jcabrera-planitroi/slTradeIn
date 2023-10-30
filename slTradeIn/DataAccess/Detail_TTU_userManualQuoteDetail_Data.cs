using slTradeIn.DAL;
using slTradeIn.Data;

namespace slTradeIn.DataAccess
{
    public class Detail_TTU_userManualQuoteDetail_Data
    {
        private readonly IUnitOfWork _unitOfWork;

        public Detail_TTU_userManualQuoteDetail_Data(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Detail_TTU_userManualQuoteDetail> GetList(int iManualQuoteID)
        {
            return _unitOfWork.DetailTTUUserManualQuoteDetailRepo.Get(q => q.iManualQuoteID == iManualQuoteID).ToList();

        }

        public Detail_TTU_userManualQuoteDetail GetInfo(int iManualQuoteDetID)
        {

            return _unitOfWork.DetailTTUUserManualQuoteDetailRepo.Get(q => q.iManualQuoteDetID == iManualQuoteDetID).FirstOrDefault();

        }

        public bool SaveDetail(Detail_TTU_userManualQuoteDetail det)
        {
            var Entity = _unitOfWork.DetailTTUUserManualQuoteDetailRepo.Get(q => q.iManualQuoteDetID == det.iManualQuoteDetID).FirstOrDefault();
            if (Entity == null)
            {
                Entity = new Detail_TTU_userManualQuoteDetail
                {
                    iManualQuoteID = det.iManualQuoteID,
                    iQuantity = det.iQuantity,
                    vHDD = det.vHDD,
                    vManufacturer = det.vManufacturer,
                    vModelNumber = det.vModelNumber,
                    vProcessor = det.vProcessor,
                    vRam = det.vRam
                };
                _unitOfWork.DetailTTUUserManualQuoteDetailRepo.Insert(Entity);
            }
            else
            {
                Entity.iQuantity = det.iQuantity;
                Entity.vHDD = det.vHDD;
                Entity.vManufacturer = det.vManufacturer;
                Entity.vModelNumber = det.vModelNumber;
                Entity.vProcessor = det.vProcessor;
                Entity.vRam = det.vRam;
                _unitOfWork.DetailTTUUserManualQuoteDetailRepo.Update(Entity);
            }

            _unitOfWork.Save();
            det.iManualQuoteDetID = Entity.iManualQuoteDetID;

            return true;
        }

        public bool DeleteDetail(int iManualQuoteDetID)
        {

            _unitOfWork.DetailTTUUserManualQuoteDetailRepo.Delete(_unitOfWork.DetailTTUUserManualQuoteDetailRepo.Get(q => q.iManualQuoteDetID == iManualQuoteDetID).FirstOrDefault());
            _unitOfWork.Save();
            return true;

        }


    }
}