using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZJ.Intern4job.Model.DAL;
using ZJ.Intern4job.Model.Query;
using ZJ.Intern4job.Model.Service;
using ZJ.Intern4job.Utility.Common;
using ZJ.Intern4job.Utility.Helper;
using ZJ.Intern4job.Utility.Result;

namespace ZJ.Intern4job.Webapi.Controllers
{
    public class ProfessionsController : ApiController
    {
        // GET api/professions
        public Object Get(ProfessionQuery query)
        {
            string msg;
            try
            {
                if (query == null)
                    query = new ProfessionQuery();
                List<Profession> dbList = BusinessService.GetProfessionListByQuery(query);
                //List<UserAPIModel> vmList = UserAPIModel.FromUserList(dbList);

                msg = string.Format(Intern4jobResources.MSG_SINGLE_ACTION_SUCCESS, "获取", "专业列表");
                return new { IsSuccess = true, Message = msg, Obj = dbList };
            }
            catch (Exception e)
            {
                msg = string.Format(Intern4jobResources.MSG_SINGLE_ACTION_FAIL, "获取", "专业列表") + string.Format(Intern4jobResources.STR_FAIL_RESAON, ExceptionHelper.GetInnerExceptionInfo(e));
                return new { IsSuccess = false, Message = msg };
            }
        }

        // GET api/professions/5
        public Object Get(string id)
        {
            string msg;
            Profession objinDb = BusinessService.GetProfessionById(id);
            if (objinDb == null)
            {
                msg = string.Format(Intern4jobResources.MSG_OBJECT_NOT_FOUND_WITH_ID, id);
                return new { IsSuccess = false, Message = msg };
            }
            msg = string.Format(Intern4jobResources.MSG_SINGLE_ACTION_SUCCESS, "获取", "专业");
            return new { IsSuccess = true, Message = msg, Obj = objinDb };
        }

        // POST api/professions
        public Object Post(Profession obj)
        {
            BaseActionResult result = BusinessService.CreateProfession(obj);
            return new { IsSuccess = result.IsSuccess, Message = result.Message };
        }

        // PUT api/professions/5
        public Object Put(string id, Profession obj)
        {
            string msg;
            Profession objinDb = BusinessService.GetProfessionById(id);
            if (objinDb == null)
            {
                msg = string.Format(Intern4jobResources.MSG_OBJECT_NOT_FOUND_WITH_ID, id);
                return new { IsSuccess = false, Message = msg };
            }
            objinDb.Name = obj.Name;
            objinDb.Description = obj.Description;
            objinDb.Catalog = obj.Catalog;
            objinDb.Class = obj.Class;
            BaseActionResult result = BusinessService.UpdateProfession(objinDb);
            return new { IsSuccess = result.IsSuccess, Message = result.Message };
        }

        // DELETE api/professions/5
        public Object Delete(string id)
        {
            BaseActionResult result = BusinessService.BulkDeleteProfessionByIds(id);
            return new { IsSuccess = result.IsSuccess, Message = result.Message };
        }
    }
}
