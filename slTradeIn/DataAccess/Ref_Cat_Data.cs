using Microsoft.Data.SqlClient;
using slTradeIn.DAL;
using slTradeIn.Data;

namespace slTradeIn.DataAccess
{
    public class Ref_Cat_Data
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public Ref_Cat_Data(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public string GetCategory(int iCategoryID = 0)
        {
            string Category = string.Empty;

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandText = "select vCatDescription from Ref_Cat with (nolock) where iCatID = " + iCategoryID.ToString();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Category = reader["vCatDescription"] == DBNull.Value ? string.Empty : reader["vCatDescription"].ToString();
                }
            }

            return Category;
        }

        public List<Ref_Cat> GetMany(List<int> ids)
        {
            return _unitOfWork.RefCatRepo.Get(x => ids.Contains(x.iCatID)).ToList();
        }
    }
}