using EntityFramework.BulkInsert.Extensions;
using ICAD.Data.DataContext;
using ICAD.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICAD.Data.DataAccess
{
    public class IcadDataAccess
    {
        private readonly IcadDataContext _context;
        public IcadDataAccess()
        {
            _context = new IcadDataContext();
        }

        public List<AccountInfo> GetAll()
        {
            var model = _context.AccountRecords.ToList();
            return model;
        }

        public AccountInfo GetById(int id)
        {
            var model = _context.AccountRecords.Find(id);
            return model;
        }

        public void Create(AccountInfo model)
        {
            if (model != null)
            {
                model.Date = DateTime.Now;
                model.bvns = null;
                _context.AccountRecords.Add(model);
                _context.SaveChanges();
            }
        }

        public void BulkInsert(IEnumerable<AccountInfo> list)
        {
            if (list.Count() > 0)
            {
                //remove bvns from list
                //list = list.Select(x =>
                //{
                //    x.bvns = null;
                //    return x;
                //});
                _context.BulkInsert(list);
                _context.SaveChanges();
            }
        }

        public void Edit(int id, AccountInfo model)
        {
            var record = GetById(id);
            if (record != null)
            {
                //model.bvns = null;
                _context.Entry(record).CurrentValues.SetValues(model);
                _context.SaveChanges();
            }
        }

        public void UpdateServiceResponse(AccountInfo model, DateTime? requestTime, DateTime? responseTime, string responseMsg, bool isSuccess = false)
        {
            if (model != null && model.Id > 0)
            {
                model.ServiceRequestTime = requestTime;
                model.ServiceResponseTime = responseTime;
                model.ServiceResponseMessage = responseMsg;
                model.IsSuccessful = isSuccess;
                _context.Entry(model).CurrentValues.SetValues(model);
                _context.SaveChanges();
            }
        }
    }
}
