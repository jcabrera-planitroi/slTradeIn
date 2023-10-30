using slTradeIn.DAL;
using slTradeIn.Data;
using slTradeIn.Models;

namespace slTradeIn.DataAccess
{
    public class Detail_TTU_userEmailCampaign_Data
    {
        private readonly IUnitOfWork _unitOfWork;

        public Detail_TTU_userEmailCampaign_Data(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<EmailCampaignModel> GetAll()
        {
            List<EmailCampaignModel> emailCampaign = new List<EmailCampaignModel>();
            int iUserID = Convert.ToInt32(slTradeIn.Help.SessionTradeIn.iUserID);
            var query = (from et in _unitOfWork.DetailTTUUserEmailCampaignRepo.Get(et => et.iUserID == iUserID)
                         select et).ToList();

            foreach (var item in query)
            {
                emailCampaign.Add(new EmailCampaignModel
                {
                    iEmailCampaignID = item.iEmailCampaingID,
                    iUserID = item.iUserID,
                    iEmailTemplateID = item.iEmailTemplateID,
                    vSubject = item.vSubject,
                    vHTMLBody = item.vHTMLBody,
                    dDateCreated = item.dDateCreated,
                    vName = item.vName
                });
            }

            return emailCampaign;

        }

        public List<EmailCampaignModel> GetEmailGroups(int id)
        {
            List<EmailCampaignModel> emailGroup = new List<EmailCampaignModel>();
            var query = (from dc in _unitOfWork.DetailTTUUserEmailCampaignDetailRepo.Get()
                         where dc.iEmailCampaingID == id
                         select dc.vEmailGroup).ToList();

            foreach (var item in query)
            {
                emailGroup.Add(new EmailCampaignModel
                {
                    emailGroup = item
                });
            }

            return emailGroup;

        }
        public CreateCampaignModel GetCreateView()
        {
            CreateCampaignModel cm = new CreateCampaignModel();
            cm.Templates = new List<Detail_TTU_emailTemplate>();
            cm.EmailGroups = new List<EmailGroupModel>();

            var query = (from et in _unitOfWork.DetailTTUEmailTemplateRepo.Get()
                         select et).ToList();

            foreach (var item in query)
            {
                cm.Templates.Add(item);
            }

            var emailGroups = (from ue in _unitOfWork.DetailTTUUserEmailRepo.Get()
                               select ue.vEmailGroup).Distinct().ToList();


            foreach (var item in emailGroups)
            {
                cm.EmailGroups.Add(new EmailGroupModel { emailGroup = item.ToString() });
            }

            return cm;
        }
        public CreateCampaignModel GetCreateViewWithID(int id)
        {
            CreateCampaignModel cm = new CreateCampaignModel();
            cm.Templates = new List<Detail_TTU_emailTemplate>();
            cm.EmailGroups = new List<EmailGroupModel>();
            cm.EmailGroupsSelected = new List<EmailGroupModel>();

            var query = (from et in _unitOfWork.DetailTTUEmailTemplateRepo.Get()
                         select et).ToList();

            foreach (var item in query)
            {
                cm.Templates.Add(item);
            }


            var emailGroups = (from ue in _unitOfWork.DetailTTUUserEmailRepo.Get()
                               select ue.vEmailGroup).Distinct().ToList();


            foreach (var item in emailGroups)
            {
                cm.EmailGroups.Add(new EmailGroupModel { emailGroup = item.ToString() });
            }

            var emailCampaign = (from et in _unitOfWork.DetailTTUUserEmailCampaignRepo.Get()
                                 where et.iEmailCampaingID == id
                                 select et).FirstOrDefault();

            var campaignDetail = (from etd in _unitOfWork.DetailTTUUserEmailCampaignDetailRepo.Get()
                                  where etd.iEmailCampaingID == id
                                  select etd).ToList();

            foreach (var item in campaignDetail)
            {
                cm.EmailGroupsSelected.Add(new EmailGroupModel { emailGroup = item.vEmailGroup });
            }

            foreach (var item in cm.EmailGroupsSelected)
            {
                cm.EmailGroups.RemoveAll(x => x.emailGroup == item.emailGroup);
            }

            cm.iEmailTemplateID = emailCampaign.iEmailTemplateID;
            cm.vSubject = emailCampaign.vSubject;
            cm.vHTMLBody = emailCampaign.vHTMLBody;
            cm.selectedTemplate = (from et in _unitOfWork.DetailTTUEmailTemplateRepo.Get()
                                   where et.iEmailTemplateID == cm.iEmailTemplateID
                                   select et).FirstOrDefault();

            return cm;
        }

        public void SaveEmailCampaign(CreateCampaignModel model)
        {
            var ent = new Detail_TTU_userEmailCampaign
            {
                iUserID = model.iUserID,
                iEmailTemplateID = model.iEmailTemplateID,
                vSubject = model.vSubject,
                vHTMLBody = model.vHTMLBody,
                dDateCreated = DateTime.Now,
                vName = model.vName
            };

            _unitOfWork.DetailTTUUserEmailCampaignRepo.Insert(ent);
            _unitOfWork.Save();

            var ecRecord = (from ec in _unitOfWork.DetailTTUUserEmailCampaignRepo.Get()
                            orderby ec.iEmailCampaingID descending
                            select ec.iEmailCampaingID).Take(1).FirstOrDefault();

            foreach (var item in model.EmailGroups)
            {
                var detail = new Detail_TTU_userEmailCampaignDetail
                {
                    iEmailCampaingID = Convert.ToInt32(ecRecord.ToString()),
                    vEmailGroup = item.emailGroup.ToString()

                };
                _unitOfWork.DetailTTUUserEmailCampaignDetailRepo.Insert(detail);
                _unitOfWork.Save();
            }
        }
    }
}