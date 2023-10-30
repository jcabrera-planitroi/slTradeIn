using Newtonsoft.Json;
using slTradeIn.Data;
using slTradeIn.Models;

namespace slTradeIn.Help
{
    public class LocationList
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        string variable = "Detail_TTU_userLocation";

        public LocationList(IHttpContextAccessor httpContextAccessor) =>
                _httpContextAccessor = httpContextAccessor;

        public List<Detail_TTU_userLocation> get_list(decimal IdTransaccionSession)
        {
            if (_httpContextAccessor.HttpContext?.Session.GetString(variable + IdTransaccionSession.ToString()) == null)
            {
                List<LocationModel> list = new List<LocationModel>();

                _httpContextAccessor.HttpContext?.Session.SetString(variable + IdTransaccionSession.ToString(), JsonConvert.SerializeObject(list));
            }

            return JsonConvert.DeserializeObject<List<Detail_TTU_userLocation>>(_httpContextAccessor.HttpContext?.Session.GetString(variable + IdTransaccionSession.ToString()) ?? "[]") ?? new List<Detail_TTU_userLocation>();
        }

        public void set_list(List<Detail_TTU_userLocation> list, decimal IdTransaccionSession)
        {
            _httpContextAccessor.HttpContext?.Session.SetString(variable + IdTransaccionSession.ToString(), JsonConvert.SerializeObject(list));
        }
    }
}