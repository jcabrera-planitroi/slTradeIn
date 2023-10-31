using System.Data;
using Microsoft.Data.SqlClient;
using slTradeIn.DAL;
using slTradeIn.Data;

namespace slTradeIn.DataAccess;

public class Detail_TTU_userEmail_Data
{
    private readonly IConfiguration _configuration;
    private readonly IUnitOfWork _unitOfWork;

    public Detail_TTU_userEmail_Data(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }

    public int Count()
    {
        return _unitOfWork.DetailTTUUserEmailRepo.Get().Count();
    }

    public List<Detail_TTU_userEmail> List(int page)
    {
        return _unitOfWork.DetailTTUUserEmailRepo.Get().OrderBy(x => x.iEmailID).Skip(page * 10).Take(10).ToList();
    }

    public void Update(int iEmailID, Detail_TTU_userEmail item)
    {
        var persisted = _unitOfWork.DetailTTUUserEmailRepo.GetByID(iEmailID);
        persisted.vName = item.vName;
        persisted.vEmailGroup = item.vEmailGroup;
        _unitOfWork.Save();
    }

    public void Delete(int iEmailID)
    {
        _unitOfWork.DetailTTUUserEmailRepo.Delete(_unitOfWork.DetailTTUUserEmailRepo.GetByID(iEmailID));
        _unitOfWork.Save();
    }

    public bool Save(IEnumerable<Detail_TTU_userEmail> items)
    {
        var detailTtuUserEmails = items as Detail_TTU_userEmail[] ?? items.ToArray();
        var emails = detailTtuUserEmails.Select(y => y.vEmail);
        var existing = _unitOfWork.DetailTTUUserEmailRepo.Get(x => emails.Contains(x.vEmail))
            .ToList();

        try
        {
            using (var copy = new SqlBulkCopy(_configuration.GetConnectionString("DefaultConnection")))
            {
                copy.DestinationTableName = "dbo.Detail_TTU_userEmail";

                using (var table = new DataTable())
                {
                    table.Columns.Add("iEmailID", typeof(int));
                    table.Columns.Add("vEmail", typeof(string));
                    table.Columns.Add("vName", typeof(string));
                    table.Columns.Add("vEmailGroup", typeof(string));
                    table.Columns.Add("dCreatedDate", typeof(DateTime));
                    table.Columns.Add("dUpdatedDate", typeof(DateTime));
                    table.Columns.Add("bStatus", typeof(bool));
                    table.Columns.Add("UseriUserID", typeof(int));
                    table.Columns.Add("iUserID", typeof(int));
                    table.Columns.Add("vEmailProvider", typeof(string));


                    foreach (var email in detailTtuUserEmails)
                    {
                        var persisted = existing.FirstOrDefault(x => x.vEmail == email.vEmail);

                        if (persisted == null)
                            table.Rows.Add(email.iEmailID, email.vEmail, email.vName, email.vEmailGroup,
                                email.dCreatedDate,
                                email.dUpdatedDate, email.bStatus, email.iUserID, email.iUserID, email.vEmailProvider);
                    }

                    copy.WriteToServer(table);
                }
            }

            return true;
        }
        catch (Exception e)
        {
            return false;
        }


        // detailTtuUserEmails.ToList().ForEach(item =>
        // {
        //     var persisted = existing.FirstOrDefault(x => x.vEmail == item.vEmail);
        //
        //     if (persisted != null)
        //     {
        //         persisted.vName = item.vName;
        //         persisted.vEmailGroup = item.vEmailGroup;
        //     }
        //     else
        //     {
        //         _unitOfWork.DetailTTUUserEmailRepo.Insert(item);
        //     }
        // });
        //
        // _unitOfWork.Save();
    }
}