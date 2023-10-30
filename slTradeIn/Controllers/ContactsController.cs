using System.Net.Http.Headers;
using Google.Apis.Auth.OAuth2;
using Google.Apis.PeopleService.v1;
using Google.Apis.PeopleService.v1.Data;
using Google.Apis.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Identity.Client;
using static Google.Apis.Auth.OAuth2.Flows.GoogleAuthorizationCodeFlow;
using static Google.Apis.PeopleService.v1.PeopleResource.ConnectionsResource;
using Person = Google.Apis.PeopleService.v1.Data.Person;

namespace slTradeIn.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IConfiguration _configuration;
        
        public ContactsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: Contacts
        public async Task<ActionResult> Index()
        {
            return View("Index", new List<Person>());
        }

        [HttpPost]
        public async Task<ActionResult> SaveGoogleContacts(ICollection<Person> people)
        {
            Console.WriteLine("Contacts");
            return RedirectToAction("Index","Landing");
        }
        
        [HttpPost]
        public async Task<ActionResult> SaveOutlookContacts(ICollection<Contact> people)
        {
            Console.WriteLine("Contacts");
            return RedirectToAction("Index","Landing");
        }

        [HttpPost]
        public async Task<ActionResult> GetGoogleContacts(string accessToken)
        {
            var credential = GoogleCredential.FromAccessToken(accessToken);

            var service = new PeopleServiceService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "TechTradeUp",
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
            ListRequest listRequest = new ListRequest(service, "people/me")
            {
                PersonFields = "names,emailAddresses,organizations,phoneNumbers,locations",
            };

            return Task.Run(() => listRequest.ExecuteAsync());
        }
        
        private Task<ListConnectionsResponse> ListContactsOutlook(PeopleServiceService service)
        {
            ListRequest listRequest = new ListRequest(service, "people/me")
            {
                PersonFields = "names,emailAddresses,organizations,phoneNumbers,locations",
            };

            return Task.Run(() => listRequest.ExecuteAsync());
        }
    }
}