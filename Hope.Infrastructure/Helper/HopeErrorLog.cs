using Hope.Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Infrastructure.Helper
{
    public class HopeErrorLog
    {
        ErrorLogRepository errorLog = new ErrorLogRepository();
        public void AddErrorLog(Exception ex,string UserId,string ModuleName)
        {
            Hope.DomainEntities.DBEntities.ErrorLog obj =new DomainEntities.DBEntities.ErrorLog();
            obj.ErrorException = ex.InnerException != null ? ex.InnerException.ToString():"";
            obj.ErrorMessage = ex.Message != null ? ex.Message.ToString() : "";
            obj.ModuleName = ModuleName;
            obj.UserId =7;
            obj.Trasnaction = DateTime.Now;
            obj.StackTrace = ex.StackTrace != null ? ex.StackTrace.ToString() : "";
            errorLog.Add(obj);
        }
    }

}
