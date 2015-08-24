using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZJ.Intern4job.Model.DAL;
using ZJ.Intern4job.Model.Query;
using ZJ.Intern4job.Model.Repository;

namespace ZJ.Intern4job.Model.Service
{
    public static partial class BusinessService
    {
        public static bool _isMatch(Profession obj, ProfessionQuery query)
        {
            if (!string.IsNullOrEmpty(query.IdEqual) && !string.Equals(obj.Id, query.IdEqual))
                return false;
            if (!string.IsNullOrEmpty(query.IdNotEqual) && string.Equals(obj.Id, query.IdNotEqual))
                return false;

            if (!string.IsNullOrEmpty(query.NameEqual) && !string.Equals(obj.Name, query.NameEqual))
                return false;
            if (!string.IsNullOrEmpty(query.NameNotEqual) && string.Equals(obj.Name, query.NameNotEqual))
                return false;
            if (!string.IsNullOrEmpty(query.NameLike) && !obj.Name.Contains(query.NameLike))
                return false;

            return true;
        }

        private static dynamic _orderByKey(Profession obj, ProfessionQuery query)
        {
            if (string.IsNullOrEmpty(query.OrderByKey))
                return obj.Id;
            return obj.GetType().GetProperty(query.OrderByKey).GetValue(obj);
        }

        public static List<Profession> GetProfessionListByQuery(ProfessionQuery query)
        {
            using (var context = new Intern4jobEntities())
            {
                var repository = new ProfessionRepository(context);

                List<Profession> professions = repository.GetPageList(item => _isMatch(item, query), item => _orderByKey(item, query), query.OrderByValue, query.Offset, query.Limit);
                return professions;
            }
        }
    }
}
