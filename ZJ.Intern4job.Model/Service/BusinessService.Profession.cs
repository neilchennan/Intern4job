using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZJ.Intern4job.Model.DAL;
using ZJ.Intern4job.Model.Query;
using ZJ.Intern4job.Model.Repository;
using ZJ.Intern4job.Utility.Common;
using ZJ.Intern4job.Utility.Helper;
using ZJ.Intern4job.Utility.Result;

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

        public static BaseActionResult CreateProfession(Profession obj4create)
        {
            string msg;
            if (obj4create == null)
            {
                msg = string.Format(Intern4jobResources.MSG_CREATE_SUCCESS, Intern4jobResources.STR_PROFESSION) + string.Format(Intern4jobResources.STR_FAIL_RESAON, Intern4jobResources.MSG_OBJECT_IS_NULL);
                return new BaseActionResult(false, msg);
            }

            try
            {
                using (var context = new Intern4jobEntities())
                {
                    var repository = new ProfessionRepository(context);
                    string newId = Guid.NewGuid().ToString();
                    obj4create.Id = newId;
                    repository.Create(obj4create);
                    context.SaveChanges();
                    msg = string.Format(Intern4jobResources.MSG_CREATE_SUCCESS, obj4create.Name);
                    return new BaseActionResult(true, msg);
                }
            }
            catch (Exception e)
            {
                msg = string.Format(Intern4jobResources.MSG_CREATE_FAIL, obj4create.Name) + string.Format(Intern4jobResources.STR_FAIL_RESAON, ExceptionHelper.GetInnerExceptionInfo(e));
                return new BaseActionResult(false, msg);
            }
        }

        public static BaseActionResult UpdateProfession(Profession obj4update)
        {
            string msg;
            if (obj4update == null)
            {
                msg = string.Format(Intern4jobResources.MSG_UPDATE_SUCCESS, Intern4jobResources.STR_PROFESSION) + string.Format(Intern4jobResources.STR_FAIL_RESAON, Intern4jobResources.MSG_OBJECT_IS_NULL);
                return new BaseActionResult(false, msg);
            }

            try
            {
                using (var context = new Intern4jobEntities())
                {
                    var repository = new ProfessionRepository(context);
                    repository.Update(obj4update);
                    context.SaveChanges();
                    msg = string.Format(Intern4jobResources.MSG_UPDATE_SUCCESS, obj4update.Name);
                    return new BaseActionResult(true, msg);
                }
            }
            catch (Exception e)
            {
                msg = string.Format(Intern4jobResources.MSG_UPDATE_FAIL, obj4update.Name) + string.Format(Intern4jobResources.STR_FAIL_RESAON, ExceptionHelper.GetInnerExceptionInfo(e));
                return new BaseActionResult(false, msg);
            }
        }

        public static BaseActionResult DeleteProfession(Profession obj4delete)
        {
            using (var context = new Intern4jobEntities())
            {
                string msg;
                var repository = new ProfessionRepository(context);

                if (obj4delete == null)
                {
                    msg = string.Format(Intern4jobResources.MSG_DELETE_SUCCESS, Intern4jobResources.STR_PROFESSION) + string.Format(Intern4jobResources.STR_FAIL_RESAON, Intern4jobResources.MSG_OBJECT_IS_NULL);
                    return new BaseActionResult(false, msg);
                }
                repository.Delete(obj4delete);
                context.SaveChanges();
                msg = string.Format(Intern4jobResources.MSG_UPDATE_SUCCESS, obj4delete.Name);
                return new BaseActionResult(true, msg);
            }
        }

        public static Profession GetProfessionById(string id4query)
        {
            ProfessionQuery query = new ProfessionQuery() { IdEqual = id4query };
            Profession objInDb = GetProfessionListByQuery(query).FirstOrDefault();
            return objInDb;
        }

        public static BaseActionResult BulkDeleteProfessionByIds(string idsStr)
        {
            string msg;
            string[] idArr = idsStr.Split(',');
            if (idArr.Length == 0)
            {
                msg = Intern4jobResources.ERR_MSG_NO_RECORD_FOR_ACTION;
                return new BaseActionResult(false, msg);
            }
            try
            {
                List<Profession> list4delete = new List<Profession>();
                foreach (string id in idArr)
                {
                    var obj4delete = GetProfessionById(id);
                    list4delete.Add(obj4delete);
                }

                using (var context = new Intern4jobEntities())
                {
                    var repository = new ProfessionRepository(context);
                    repository.BulkDelete(list4delete);
                    context.SaveChanges();
                    msg = string.Format(Intern4jobResources.MSG_BULK_ACTION_SUCCESS, Intern4jobResources.STR_DELETE, idArr.Length);
                    return new BaseActionResult(true, msg);
                }
            }
            catch (Exception e)
            {
                msg = string.Format(Intern4jobResources.MSG_BULK_ACTION_FAIL, Intern4jobResources.STR_DELETE, idArr.Length) + string.Format(Intern4jobResources.STR_FAIL_RESAON, ExceptionHelper.GetInnerExceptionInfo(e));
                return new BaseActionResult(false, msg, e);
            }
        }
    }
}
