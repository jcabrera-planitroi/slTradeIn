using slTradeIn.DAL;
using slTradeIn.Data;
using slTradeIn.Models;

namespace slTradeIn.DataAccess;

public class Detail_TTU_user_Data
{
    private readonly IUnitOfWork _unitOfWork;

    public Detail_TTU_user_Data(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public bool CreateUser(RegisterUserModel model)
    {
        var Ent = new Detail_TTU_user
        {
            vEmail = model.Email,
            vPassword = model.Password,
            vFirstName = model.FirstName,
            vLastName = model.LastName,
            vTitle = model.Title,
            vCompanyName = model.CompanyName,
            vOfficePhone = model.OfficePhone,
            vExtension = model.Extension,
            vCellphone = model.Cellphone,
            bActive = true,
            vIPCreation = model.IPAddress,
            dDateCreation = DateTime.Now,
            iTTUTechPartner = model.iTTUTechPartner == 0 ? null : model.iTTUTechPartner
        };
        _unitOfWork.DetailTTUUserRepo.Insert(Ent);

        model.iUserID = 0;

        _unitOfWork.DetailTTUUserLocationRepo.Insert(new Detail_TTU_userLocation
        {
            iUser = Ent,
            vLocationLabel = model.LocationLabel,
            vLocationEmail = model.LocationEmail,
            vLocationContactPerson = model.ContactPerson,
            vLocationStreetAddress = model.StreetAddress,
            vSuiteAppt = model.SuiteApt,
            vCity = model.City,
            vState = model.State,
            vPostalCode = model.PostalCode,
            bIsActive = true,
            vIPCreated = model.IPAddress,
            dDateCreated = DateTime.Now
        });

        _unitOfWork.Save();

        return true;
    }

    public bool CheckIfUserExists(string Email)
    {
        var Entity = _unitOfWork.DetailTTUUserRepo.Get().Where(q => q.vEmail == Email).FirstOrDefault();
        if (Entity == null)
            return false;
        return true;
    }

    public RegisterUserModel GetInfo(string Email, string Password)
    {
        var Entity = _unitOfWork.DetailTTUUserRepo.Get(user => user.vEmail == Email && user.vPassword == Password)
            .FirstOrDefault();
        if (Entity == null)
            return null;

        var model = new RegisterUserModel
        {
            iUserID = Entity.iUserID,
            Email = Entity.vEmail,
            bIsAdmin = Entity.bIsAdmin ?? false,
            iTTUTechPartner = Entity.iTTUTechPartner ?? 0,
            FirstName = Entity.vFirstName
        };
        return model;
    }

    public RegisterUserModel GetInfo(string Email)
    {
        var Entity = _unitOfWork.DetailTTUUserRepo.Get(user => user.vEmail == Email)
            .FirstOrDefault();
        if (Entity == null)
            return null;

        var model = new RegisterUserModel
        {
            iUserID = Entity.iUserID,
            Email = Entity.vEmail,
            bIsAdmin = Entity.bIsAdmin ?? false,
            iTTUTechPartner = Entity.iTTUTechPartner ?? 0,
            FirstName = Entity.vFirstName
        };
        return model;
    }

    public RegisterUserModel GetProfile(string email)
    {
        var Entity = _unitOfWork.DetailTTUUserRepo.Get(q => q.vEmail == email).FirstOrDefault();
        if (Entity == null) return null;

        var model = new RegisterUserModel
        {
            iUserID = 0,
            Email = Entity.vEmail,
            bIsAdmin = Entity.bIsAdmin ?? false,
            iTTUTechPartner = Entity.iTTUTechPartner ?? 0,

            Cellphone = Entity.vCellphone,
            CompanyName = Entity.vCompanyName,
            Extension = Entity.vExtension,
            FirstName = Entity.vFirstName,
            LastName = Entity.vLastName,
            OfficePhone = Entity.vOfficePhone,
            Title = Entity.vTitle
        };

        return model;
    }

    public void SaveProfile(RegisterUserModel model)
    {
        var Entity = _unitOfWork.DetailTTUUserRepo.Get(q => q.iUserID == model.iUserID).FirstOrDefault();
        Entity.vFirstName = model.FirstName;
        Entity.vLastName = model.LastName;
        Entity.vTitle = model.Title;
        Entity.vCompanyName = model.CompanyName;
        Entity.vOfficePhone = model.OfficePhone;
        Entity.vExtension = model.Extension;
        Entity.vCellphone = model.Cellphone;
        _unitOfWork.Save();
    }

    public void SavePassword(RegisterUserModel model)
    {
        var Entity = _unitOfWork.DetailTTUUserRepo.Get(q => q.iUserID == model.iUserID).FirstOrDefault();
        Entity.vPassword = model.Password;
        _unitOfWork.Save();
    }
}