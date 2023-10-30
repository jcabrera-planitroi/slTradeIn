using slTradeIn.DAL;
using slTradeIn.Data;

namespace slTradeIn.DataAccess
{
    public class Detail_TTU_userCartDetail_Data
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Detail_TTU_userCart_Data _detail_TTU_UserCart_Data;

        public Detail_TTU_userCartDetail_Data(IUnitOfWork unitOfWork, Detail_TTU_userCart_Data detail_TTU_UserCart_Data)
        {
            this._unitOfWork = unitOfWork;
            _detail_TTU_UserCart_Data = detail_TTU_UserCart_Data;
        }

        public List<Detail_TTU_userCartDetail> GetListCartDetail(int iCartID)
        {
            return _unitOfWork.DetailTTUUserCartDetailRepo.Get(q => q.iCartID == iCartID).ToList();
        }

        public void Update(int id, Action<Detail_TTU_userCartDetail> update)
        {
            update(_unitOfWork.DetailTTUUserCartDetailRepo.GetByID(id));
            _unitOfWork.Save();
        }

        public void Delete(int iEmailID)
        {
            _unitOfWork.DetailTTUUserCartDetailRepo.Delete(
                _unitOfWork.DetailTTUUserCartDetailRepo.GetByID(iEmailID)
            );
            _unitOfWork.Save();
        }

        public bool SaveCartDetail(Detail_TTU_userCartDetail model)
        {
            if (model.iCartDetailID > 0)
            {
                var Entity = _unitOfWork.DetailTTUUserCartDetailRepo.Get(q => q.iCartDetailID == model.iCartDetailID).FirstOrDefault();
                if (Entity != null)
                {
                    Entity.iCartID = model.iCartID;
                    Entity.iCategory = model.iCategory;
                    Entity.iManufacturer = model.iManufacturer;
                    Entity.iModelID = model.iModelID;
                    Entity.bIsAccessible = model.bIsAccessible;
                    Entity.iProcessorType = model.iProcessorType;
                    Entity.iProcessorGen = model.iProcessorGen;
                    Entity.iMemory = model.iMemory;
                    Entity.iHDD = model.iHDD;
                    Entity.vGrade = model.vGrade;
                    Entity.iQuantity = model.iQuantity;
                    Entity.mPrice = Math.Round(model.mPrice, 2);
                    Entity.bDOD = model.bDOD;
                    Entity.mTotal = Math.Round(model.mPrice * model.iQuantity, 2);
                }
            }
            else
            {
                _unitOfWork.DetailTTUUserCartDetailRepo.Insert(new Detail_TTU_userCartDetail
                {
                    iCartID = model.iCartID,
                    iCategory = model.iCategory,
                    iManufacturer = model.iManufacturer,
                    iModelID = model.iModelID,
                    bIsAccessible = model.bIsAccessible,
                    iProcessorType = model.iProcessorType,
                    iProcessorGen = model.iProcessorGen,
                    iMemory = model.iMemory,
                    iHDD = model.iHDD,
                    vGrade = model.vGrade,
                    iQuantity = model.iQuantity,
                    mPrice = Math.Round(model.mPrice, 2),
                    bDOD = model.bDOD,
                    mTotal = Math.Round(model.mPrice * model.iQuantity, 2),
                });
            }
            _unitOfWork.Save();

            _detail_TTU_UserCart_Data.UpdateTotal(model.iCartID);
            return true;
        }

        public List<Detail_TTU_userCartDetail> FromCarts(List<int> cartIds)
        {
            return _unitOfWork.DetailTTUUserCartDetailRepo.Get(x => cartIds.Contains(x.iCartID)).ToList();
        }
    }
}