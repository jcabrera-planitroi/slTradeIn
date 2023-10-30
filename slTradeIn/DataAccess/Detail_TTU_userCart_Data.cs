using slTradeIn.DAL;
using slTradeIn.Data;
using slTradeIn.Models;
using slTradeIn.Shared;

namespace slTradeIn.DataAccess
{
    public class Detail_TTU_userCart_Data
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Ref_main_Data _ref_Main_Data;

        public Detail_TTU_userCart_Data(IUnitOfWork unitOfWork, Ref_main_Data ref_Main_Data)
        {
            this._unitOfWork = unitOfWork;
            _ref_Main_Data = ref_Main_Data;
        }

        public Detail_TTU_userCart GetInfo(int iCartID, int iUserID, int iLogisticsID)
        {
            var Entity = _unitOfWork.DetailTTUUserCartRepo.Get(q => q.iCartID == iCartID && q.iBoxStatusID == 14964).FirstOrDefault();
            if (Entity == null)
            {
                Entity = _unitOfWork.DetailTTUUserCartRepo.Get(q => q.iUserID == iUserID && q.iBoxStatusID == 14964).FirstOrDefault();
                if (Entity == null)
                {
                    
                }
            }
            return Entity;
        }

        public void UpdateQuoteSignatureDate(int iCartID)
        {

            var cart = _unitOfWork.DetailTTUUserCartRepo.GetByID(iCartID);
            cart.bDateSigned = DateTime.Now;
            cart.iBoxStatusID = _unitOfWork.RefMainRepo.Get(q => q.vReferenceID == Constants.IBOX_STATUS_ID && q.iRefOrder == Constants.REFODER_SIGNED).FirstOrDefault()?.iRefMainID;
            _unitOfWork.Save();
        }

        public void UpdateQuoteSignature(SignatureModel model)
        {
            var cart = _unitOfWork.DetailTTUUserCartRepo.GetByID(model.iCartID);
            cart.vImageName = model.vImageName;
            _unitOfWork.Save();
        }

        public void UpdateExpiredQuote(int iCartID)
        {
            var cart = _unitOfWork.DetailTTUUserCartRepo.GetByID(iCartID);
            cart.bDateSigned = null;
            cart.dDateCreated = DateTime.Now;
            cart.iBoxStatusID = Constants.QUOTE_CREATED;
            cart.vImageName = null;
            _unitOfWork.Save();

        }

        public Detail_TTU_userCart GetCart(int iCartID)
        {

            return _unitOfWork.DetailTTUUserCartRepo.GetByID(iCartID);
        }

        public bool SaveCart(Detail_TTU_userCart model)
        {
            var lst = _ref_Main_Data.GetListReference("iBoxStatusID");
            int? iBoxStatusID = null;
            if (lst.Count > 0)
                iBoxStatusID = lst.OrderBy(q => q.iRefOrder).First().iRefMainID;


            var Entity = _unitOfWork.DetailTTUUserCartRepo.Get(q => q.iCartID == model.iCartID).FirstOrDefault();
            if (Entity == null)
            {
                var count = _unitOfWork.DetailTTUUserCartRepo.Get(x => x.iUserID == model.iUserID).Count();
                var EntityLog = _unitOfWork.DetailTTUUserLocationRepo.Get(q => q.iUserID == model.iUserID).FirstOrDefault();

                Entity = new Detail_TTU_userCart
                {
                    iUserID = model.iUserID,
                    bSerializedReport = model.bSerializedReport,
                    vShippingType = model.vShippingType,
                    iLogisticID = model.iLogisticID == 0 ? EntityLog.iLogisticID : model.iLogisticID,
                    iBoxStatusID = iBoxStatusID,
                    dDateCreated = DateTime.Now,
                    iQuoteNumber = count + 1
                };
                _unitOfWork.DetailTTUUserCartRepo.Insert(Entity);
                _unitOfWork.Save();
                model.iCartID = Entity.iCartID;
            }
            else
            {
                Entity.bSerializedReport = model.bSerializedReport;
                Entity.vShippingType = model.vShippingType;
                Entity.iLogisticID = model.iLogisticID;
                Entity.dDateCreated = DateTime.Now;
                _unitOfWork.Save();
            }
            return true;
        }

        public void UpdateTotal(int cartId)
        {
            var cart = _unitOfWork.DetailTTUUserCartRepo.GetByID(cartId);
            var details = _unitOfWork.DetailTTUUserCartDetailRepo.Get(x => x.iCartID == cartId).ToList();

            cart.mTotal = details.Sum(x => x.mTotal);
            cart.mTotalSerializedReport = cart.bSerializedReport ?
                details.Sum(x => x.iQuantity) * 15 : 0;
            cart.mTotalShipping = 56;
            cart.mTotalDOD = details.Where(x => x.bDOD).Sum(x => x.iQuantity) * 10;

            cart.mTotalPayout =
                cart.mTotal -
                cart.mTotalSerializedReport -
                cart.mTotalShipping -
                cart.mTotalDOD;
        }

        public List<BoxListModel> GetList(int iUserID)
        {
            return _unitOfWork.VwTTUBoxListRepo.Get(q => q.iUserID == iUserID).Select(q => new BoxListModel
            {
                iCartID = q.iCartID,
                vLocationLabel = q.vLocationLabel,
                vLocationContactPerson = q.vLocationContactPerson,
                DateCreated = q.dDateCreated,
                QuoteStatus = q.QuoteStatus,
                IsSigned = q.QuoteStatus == "Quote Signed" ? true : false
            }).ToList();
        }

        public void UserHasOnlyOneCart(int iUserID, ref bool HasOneCart, ref bool HastDetailInCart, ref int iCartID)
        {
            var a = _unitOfWork.DetailTTUUserCartRepo.Get(q => q.iUserID == iUserID).ToList();
            HasOneCart = a.Count == 1 || a.Count == 0 ? true : false;
            if (!HasOneCart)
            {
                iCartID = a.OrderByDescending(q => q.iCartID).Select(q => q.iCartID).FirstOrDefault();
                return;
            }

            int o = 0;
            foreach (var item in a)
            {
                var d = _unitOfWork.DetailTTUUserCartDetailRepo.Get(q => q.iCartID == item.iCartID).Count();
                if (d > 0)
                {
                    o++;
                    if (o > 1)
                    {
                        HastDetailInCart = true;
                        return;
                    }
                }
            }
        }
    }
}