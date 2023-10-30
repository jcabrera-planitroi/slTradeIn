using slTradeIn.DAL;
using slTradeIn.Data;

namespace slTradeIn.DataAccess
{
    public class Detail_TTU_userLocation_Data
    {
        private readonly IUnitOfWork _unitOfWork;

        public Detail_TTU_userLocation_Data(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Detail_TTU_userLocation> GetLocations(int iUserID)
        {
            var model = new List<Detail_TTU_userLocation>();

            model = _unitOfWork.DetailTTUUserLocationRepo.Get(q => q.iUserID == iUserID && q.bIsActive == true).ToList();

            return model;
        }

        public Detail_TTU_userLocation GetLocation(int iLogisticID)
        {

            return _unitOfWork.DetailTTUUserLocationRepo.Get(q => q.iLogisticID == iLogisticID).FirstOrDefault();

        }

        public bool DeleteLocation(int iLogisticID)
        {
            var Entity = _unitOfWork.DetailTTUUserLocationRepo.Get(q => q.iLogisticID == iLogisticID).FirstOrDefault();
            if (Entity != null)
            {
                Entity.bIsActive = false;
                _unitOfWork.DetailTTUUserLocationRepo.Update(Entity);
                _unitOfWork.Save();
            }

            return true;
        }

        public bool SaveLocation(Detail_TTU_userLocation model)
        {
            if (model.iLogisticID == 0)
            {
                _unitOfWork.DetailTTUUserLocationRepo.Insert(new Detail_TTU_userLocation
                {
                    iUserID = model.iUserID,
                    vLocationLabel = model.vLocationLabel,
                    vLocationEmail = model.vLocationEmail,
                    vLocationContactPerson = model.vLocationContactPerson,
                    vLocationStreetAddress = model.vLocationStreetAddress,
                    vSuiteAppt = model.vSuiteAppt,
                    vCity = model.vCity,
                    vState = model.vState,
                    vPostalCode = model.vPostalCode,
                    bIsActive = true,
                    vIPCreated = model.vIPCreated,
                    dDateCreated = DateTime.Now
                });
            }
            else
            {
                var Entity = _unitOfWork.DetailTTUUserLocationRepo.Get(q => q.iLogisticID == model.iLogisticID && q.bIsActive == true).FirstOrDefault();
                if (Entity != null)
                {
                    Entity.vLocationLabel = model.vLocationLabel;
                    Entity.vLocationEmail = model.vLocationEmail;
                    Entity.vLocationContactPerson = model.vLocationContactPerson;
                    Entity.vLocationStreetAddress = model.vLocationStreetAddress;
                    Entity.vSuiteAppt = model.vSuiteAppt;
                    Entity.vCity = model.vCity;
                    Entity.vState = model.vState;
                    Entity.vPostalCode = model.vPostalCode;
                    _unitOfWork.DetailTTUUserLocationRepo.Update(Entity);
                }
            }

            _unitOfWork.Save();

            return true;
        }
    }
}