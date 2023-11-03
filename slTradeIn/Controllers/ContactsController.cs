using System.Net.Http.Headers;
using Google.Apis.Auth.OAuth2;
using Google.Apis.PeopleService.v1;
using Google.Apis.PeopleService.v1.Data;
using Google.Apis.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using slTradeIn.Data;
using slTradeIn.DataAccess;
using slTradeIn.Help;
using static Google.Apis.PeopleService.v1.PeopleResource.ConnectionsResource;
using Person = Google.Apis.PeopleService.v1.Data.Person;

namespace slTradeIn.Controllers;

public class ContactsController : CustomController
{
    private readonly Detail_TTU_userEmail_Data _ttuUserEmailData;

    public ContactsController(Detail_TTU_userEmail_Data ttuUserEmailData)
    {
        _ttuUserEmailData = ttuUserEmailData;
    }

    // GET: Contacts
    public async Task<ActionResult> Index(int page = 0)
    {
        ViewBag.Page = page;
        ViewBag.Count = _ttuUserEmailData.Count() / 10;
        return View(_ttuUserEmailData.List(page));
    }

    [HttpPost]
    public async Task<ActionResult> SaveGoogleContacts(ICollection<Person> people)
    {
        var emailList = new List<Detail_TTU_userEmail>();

        foreach (var person in people)
            if (person.EmailAddresses?.Count > 0)
                foreach (var emailAddress in person.EmailAddresses)
                    emailList.Add(new Detail_TTU_userEmail
                    {
                        vEmail = emailAddress.Value,
                        vName = person.Names[0].DisplayName,
                        vEmailGroup = "",
                        dCreatedDate = DateTime.Now,
                        bStatus = true,
                        iUserID = Convert.ToInt32(SessionTradeIn.iUserID),
                        vEmailProvider = "GOOGLE"
                    });

        _ttuUserEmailData.Save(emailList);
        return new OkResult();
    }

    [HttpPost]
    public async Task<ActionResult> SaveOutlookContacts(ICollection<Contact> people)
    {
        var emailList = new List<Detail_TTU_userEmail>();

        foreach (var person in people)
            if (person.EmailAddresses?.Count > 0)
                foreach (var emailAddress in person.EmailAddresses)
                    emailList.Add(new Detail_TTU_userEmail
                    {
                        vEmail = emailAddress.Address,
                        vName = person.DisplayName,
                        vEmailGroup = "",
                        dCreatedDate = DateTime.Now,
                        bStatus = true,
                        iUserID = Convert.ToInt32(SessionTradeIn.iUserID),
                        vEmailProvider = "OUTLOOK"
                    });

        _ttuUserEmailData.Save(emailList);
        return new OkResult();
    }

    [HttpPost]
    public async Task<ActionResult> GetGoogleContacts(string accessToken)
    {
        var credential = GoogleCredential.FromAccessToken(accessToken);

        var service = new PeopleServiceService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credential,
            ApplicationName = "TechTradeUp"
        });

        var contactsList = await ListContactsGoogle(service);

        // List Contacts.
        return Json(contactsList.Connections);
    }

    [HttpPost]
    public async Task<ActionResult> GetOutlookContacts(string accessToken)
    {
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var graphClient = new GraphServiceClient(httpClient);

        var contacts = await ListContacts(graphClient);

        return Json(contacts);
    }

    private async Task<List<Contact>> ListContacts(GraphServiceClient client)
    {
        try
        {
            var contacts = await client.Me.Contacts.GetAsync();

            return contacts.Value;
        }
        catch (Exception ex)
        {
            // Manejar errores
            throw ex;
        }
    }

    private Task<ListConnectionsResponse> ListContactsGoogle(PeopleServiceService service)
    {
        var listRequest = new ListRequest(service, "people/me")
        {
            PersonFields = "names,emailAddresses,organizations,phoneNumbers,locations"
        };

        return Task.Run(() => listRequest.ExecuteAsync());
    }

    private Task<ListConnectionsResponse> ListContactsOutlook(PeopleServiceService service)
    {
        var listRequest = new ListRequest(service, "people/me")
        {
            PersonFields = "names,emailAddresses,organizations,phoneNumbers,locations"
        };

        return Task.Run(() => listRequest.ExecuteAsync());
    }
}