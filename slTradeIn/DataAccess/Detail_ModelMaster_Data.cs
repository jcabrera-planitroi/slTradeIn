using Microsoft.Data.SqlClient;
using slTradeIn.DAL;
using slTradeIn.Data;
using slTradeIn.Models;

namespace slTradeIn.DataAccess
{
    public class Detail_ModelMaster_Data
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        public Detail_ModelMaster_Data(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public List<ModelModel> GetListModel(int iCategoryID, int iManufacturer, string FilterWord)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandText = "declare @iCategory int = " + iCategoryID.ToString() + ", "
                                + " @vFilterWord varchar(max) = '" + FilterWord + "', "
                                + " @iManufacturer int = " + iManufacturer.ToString()

                                + " select distinct dm.iModelID, rc.vCatDescription Category, dm.vModelNumber, rm.vDescription Manufacturer, rm.iRefMainID "
                                + " from Detail_ModelMaster as dm with(nolock) left join "
                                + " Detail_ModelMasterPricing as dmp with(nolock) on dm.iModelID = dmp.iModelID left join "
                                + " Ref_Cat as rc with(nolock) on dm.iCategoryID = rc.iCatID left join "
                                + " ref_main as rm with(nolock) on rm.iRefMainID = dm.iMfg "
                                + " WHERE DM.iCategoryID = @iCategory "
                                + " and dm.vModelNumber like '%' + @vFilterWord + '%' "
                                + " and dmp.iModelPricingID is not null "
                                + " and isnull(dm.iMfg, @iManufacturer) = @iManufacturer";

                List<ModelModel> ModelList = new List<ModelModel>();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    ModelList.Add(new ModelModel
                    {
                        iModelID = reader["iModelID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["iModelID"]),
                        Category = reader["Category"] == DBNull.Value ? "" : Convert.ToString(reader["Category"]),
                        Model = reader["vModelNumber"] == DBNull.Value ? "" : Convert.ToString(reader["vModelNumber"]),
                        Manufacturer = reader["Manufacturer"] == DBNull.Value ? "" : Convert.ToString(reader["Manufacturer"]),
                        iMfg = reader["iRefMainID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["iRefMainID"])
                    });
                }
                return ModelList;
            }
        }

        public ModelModel GetModel(int iModelID = 0)
        {
            ModelModel model = new ModelModel();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                con.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandText = "select dmm.iModelID, dmm.vModelNumber, rc.vCatDescription, rm.vDescription  "
                                + " from Detail_ModelMaster as dmm with(nolock) left join "
                                + " ref_main as rm with(nolock) on rm.iRefMainID = dmm.iMfg left join "
                                + " Ref_Cat as rc with(nolock) on rc.iCatID = dmm.iCategoryID "
                                + " where iModelID = " + iModelID.ToString();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    model = new ModelModel
                    {
                        iModelID = reader["iModelID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["iModelID"]),
                        Category = reader["vCatDescription"] == DBNull.Value ? "" : Convert.ToString(reader["vCatDescription"]),
                        Model = reader["vModelNumber"] == DBNull.Value ? "" : Convert.ToString(reader["vModelNumber"]),
                        Manufacturer = reader["vDescription"] == DBNull.Value ? "" : Convert.ToString(reader["vDescription"])
                    };
                }
            }

            return model;
        }

        public List<Detail_ModelMaster> GetMany(List<int> ids)
        {
            return _unitOfWork.DetailModelMasterRepo.Get(x => ids.Contains(x.iModelID)).ToList();
        }
    }
}