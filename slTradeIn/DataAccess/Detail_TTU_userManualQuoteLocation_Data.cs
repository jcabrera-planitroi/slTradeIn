using slTradeIn.DAL;
using slTradeIn.Data;
using slTradeIn.Models;

namespace slTradeIn.DataAccess
{
    public class Detail_TTU_userManualQuoteLocation_Data
    {
        private readonly IUnitOfWork _unitOfWork;

        public Detail_TTU_userManualQuoteLocation_Data(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<ManualQuoteLocationModel> GetList(int iManualQuoteID, int iUserID)
        {
            var List = new List<ManualQuoteLocationModel>();
            List = _unitOfWork.DetailTTUUserManualQuoteLocationRepo.Get(q => q.iManualQuoteID == iManualQuoteID).Select(q => new
            ManualQuoteLocationModel
            {
                iLogisticID = q.iLogisticID,
                iManualQuoteID = q.iManualQuoteID,
                iManualQuoteLocID = q.iManualQuoteLocID,
                bCheck = true
            }).ToList();

            var ListLogistics = _unitOfWork.DetailTTUUserLocationRepo.Get(q => q.iUserID == iUserID).ToList();
            foreach (var item in ListLogistics)
            {
                var Loc = List.Where(q => q.iLogisticID == item.iLogisticID).FirstOrDefault();
                if (Loc == null)
                {
                    List.Add(new ManualQuoteLocationModel
                    {
                        iLogisticID = item.iLogisticID,
                        vLocationLabel = item.vLocationLabel,
                        bCheck = false
                    });
                }
                else
                {
                    Loc.vLocationLabel = item.vLocationLabel;
                }
            }

            return List;

        }

        public bool SaveLocation(List<ManualQuoteLocationModel> List, int iManualQuoteID)
        {

            var ListEntity = _unitOfWork.DetailTTUUserManualQuoteLocationRepo.Get(q => q.iManualQuoteID == iManualQuoteID).ToList();
            _unitOfWork.DetailTTUUserManualQuoteLocationRepo.RemoveRange(ListEntity);
            foreach (var item in List.Where(q => q.bCheck).ToList())
            {
                _unitOfWork.DetailTTUUserManualQuoteLocationRepo.Insert(new Detail_TTU_userManualQuoteLocation
                {
                    iManualQuoteID = iManualQuoteID,
                    iLogisticID = item.iLogisticID
                });
            }
            _unitOfWork.Save();
            return true;
        }

    }
}