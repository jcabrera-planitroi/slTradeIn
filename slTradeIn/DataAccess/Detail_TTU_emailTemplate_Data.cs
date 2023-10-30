using slTradeIn.DAL;
using slTradeIn.Data;
using slTradeIn.Models;

namespace slTradeIn.DataAccess
{
    public class Detail_TTU_emailTemplate_Data
    {
        private readonly IUnitOfWork _unitOfWork;

        public Detail_TTU_emailTemplate_Data(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<EmailTemplateModel> GetAll()
        {
            List<EmailTemplateModel> emailTemplate = new List<EmailTemplateModel>();

            var query = (from et in _unitOfWork.DetailTTUEmailTemplateRepo.Get()
                         where et.bActive == true
                         select et).ToList();

            foreach (var item in query)
            {
                emailTemplate.Add(new EmailTemplateModel
                {
                    iEmailTemplateID = item.iEmailTemplateID,
                    vTemplateName = item.vTemplateName,
                    vSubject = item.vSubject,
                    vHTMLBody = item.vHTMLBody,
                    bActive = item.bActive

                });
            }

            return emailTemplate;

        }
        public EmailTemplateModel GetWithId(int id)
        {
            EmailTemplateModel emailTemplate = new EmailTemplateModel();
            var query = (from et in _unitOfWork.DetailTTUEmailTemplateRepo.Get()
                         where et.iEmailTemplateID == id
                         select et).FirstOrDefault();

            if (query != null)
            {
                emailTemplate = new EmailTemplateModel
                {
                    iEmailTemplateID = query.iEmailTemplateID,
                    vTemplateName = query.vTemplateName,
                    vSubject = query.vSubject,
                    vHTMLBody = query.vHTMLBody,
                    bActive = query.bActive
                };
            }

            return emailTemplate;
        }

        public void SaveEmailTemplate(EmailTemplateModel model)
        {
            var ent = new Detail_TTU_emailTemplate
            {
                vTemplateName = model.vTemplateName,
                vSubject = model.vSubject,
                vHTMLBody = model.vHTMLBody,
                bActive = model.bActive,
                dCreatedDate = DateTime.Now
            };

            _unitOfWork.DetailTTUEmailTemplateRepo.Insert(ent);
            _unitOfWork.Save();
        }

        public void UpdateEmailTemplate(EmailTemplateModel model)
        {
            var query = (from et in _unitOfWork.DetailTTUEmailTemplateRepo.Get()
                         where et.iEmailTemplateID == model.iEmailTemplateID
                         select et).FirstOrDefault();

            if (query != null)
            {
                query.vTemplateName = model.vTemplateName;
                query.vSubject = model.vSubject;
                query.vHTMLBody = model.vHTMLBody;
                query.bActive = model.bActive;
                if (model.dInactiveDate > new DateTime(1, 1, 1))
                {
                    query.dInactiveDate = model.dInactiveDate;
                }
                query.dUpdatedDate = model.dUpdatedDate;
                _unitOfWork.Save();
            }

        }

        public void DeleteEmailTemplate(int id)
        {
            var query = (from et in _unitOfWork.DetailTTUEmailTemplateRepo.Get()
                         where et.iEmailTemplateID == id
                         select et).FirstOrDefault();

            if (query != null)
            {
                query.dInactiveDate = DateTime.Now;
                _unitOfWork.DetailTTUEmailTemplateRepo.Delete(query);
                _unitOfWork.Save();
            }
        }
    }
}