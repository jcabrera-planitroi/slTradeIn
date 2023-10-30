using slTradeIn.DAL;
using slTradeIn.Data;

namespace slTradeIn.DataAccess
{
    public class Detail_TTU_userEmail_Data
    {
        private readonly IUnitOfWork _unitOfWork;

        public Detail_TTU_userEmail_Data(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int Count()
        {
            return _unitOfWork.DetailTTUUserEmailRepo.Get().Count();
        }

        public List<Detail_TTU_userEmail> List(int page)
        {
            return _unitOfWork.DetailTTUUserEmailRepo.Get().OrderBy(x => x.iEmailID).Skip(page * 10).Take(10).ToList();
        }

        public void Update(int iEmailID, Detail_TTU_userEmail item)
        {
            var persisted = _unitOfWork.DetailTTUUserEmailRepo.GetByID(iEmailID);
            persisted.vName = item.vName;
            persisted.vEmailGroup = item.vEmailGroup;
            _unitOfWork.Save();
        }

        public void Delete(int iEmailID)
        {
            _unitOfWork.DetailTTUUserEmailRepo.Delete(_unitOfWork.DetailTTUUserEmailRepo.GetByID(iEmailID));
            _unitOfWork.Save();
        }

        public bool Save(IEnumerable<Detail_TTU_userEmail> items)
        {
            var emails = items.Select(y => y.vEmail);
            var existing = _unitOfWork.DetailTTUUserEmailRepo.Get(x => emails.Contains(x.vEmail))
                .ToList();

            items.ToList().ForEach(item =>
            {
                var persisted = existing.FirstOrDefault(x => x.vEmail == item.vEmail);

                if (persisted != null)
                {
                    persisted.vName = item.vName;
                    persisted.vEmailGroup = item.vEmailGroup;
                }
                else
                {
                    _unitOfWork.DetailTTUUserEmailRepo.Insert(item);
                }
            });

            _unitOfWork.Save();


            return true;
        }
    }
}
