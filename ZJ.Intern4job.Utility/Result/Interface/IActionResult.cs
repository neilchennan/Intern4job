using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZJ.Intern4job.Utility.Result.Interface
{
    public interface IActionResult<T, SubT>
    {
        bool IsSuccess { set; get; }

        T ResultObject { set; get; }

        string Message { set; get; }

        string Summary { get; }

        int TotalNum { get; }

        int SuccessNum { get; }

        int FailNum { get; }

        Exception Exception { set; get; }

        List<SubT> SubResultList { set; get; }

        void AddSubResult(SubT subResult);
    }
}

