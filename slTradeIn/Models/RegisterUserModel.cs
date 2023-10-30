using slTradeIn.Data;

namespace slTradeIn.Models;

public class RegisterUserModel
{
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Title { get; set; }
    public string CompanyName { get; set; }
    public string OfficePhone { get; set; }
    public string? Extension { get; set; }
    public string Cellphone { get; set; }

    public string LocationLabel { get; set; }
    public string StreetAddress { get; set; }
    public string SuiteApt { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
    public string ContactPerson { get; set; }
    public string LocationEmail { get; set; }
    public string? IPAddress { get; set; }
    public int iUserID { get; set; }
    public int iLogisticID { get; set; }
    public int iTTUTechPartner { get; set; }
    public bool bIsAdmin { get; set; }
    public List<Detail_TTU_userLocation>? LocationDetail { get; set; }
}

public class SignatureModel
{
    public int iCartID { get; set; }
    public string vImageName { get; set; }
    public string file { get; set; }
    public int iUserID { get; set; }
}