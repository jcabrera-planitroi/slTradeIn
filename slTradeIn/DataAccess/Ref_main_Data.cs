using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using slTradeIn.DAL;
using slTradeIn.Data;

namespace slTradeIn.DataAccess
{
    public class Ref_main_Data
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public Ref_main_Data(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public string GetCatalog(int iRefMainID = 0)
        {
            string Category = string.Empty;

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandText = "select vDescription from ref_main where iRefMainID = " + iRefMainID.ToString();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Category = reader["vDescription"] == DBNull.Value ? string.Empty : reader["vDescription"].ToString();
                }
            }

            return Category;
        }

        public Dictionary<int, string> GetMany(List<int> ids)
        {
            var result = new Dictionary<int, string>();

            foreach (var entry in _unitOfWork.RefMainRepo.Get(x => ids.Contains(x.iRefMainID)))
            {
                result.Add(entry.iRefMainID, entry.vDescription);
            }

            return result;
        }

        public List<SelectListItem> GetList(string vReferenceID)
        {
            List<SelectListItem> List = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandText = "select iRefMainID, vDescription from ref_main where vReferenceID = '" + vReferenceID + "'";
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    List.Add(new SelectListItem
                    {
                        Value = reader["iRefMainID"] == DBNull.Value ? string.Empty : reader["iRefMainID"].ToString(),
                        Text = reader["vDescription"] == DBNull.Value ? string.Empty : reader["vDescription"].ToString()
                    });
                }
            }
            return List;
        }

        public List<Ref_Main> GetListReference(string vReferenceID)
        {
            return _unitOfWork.RefMainRepo.Get(q => q.vReferenceID == vReferenceID).ToList();
        }
    }
}