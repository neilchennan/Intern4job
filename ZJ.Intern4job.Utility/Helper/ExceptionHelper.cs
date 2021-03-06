﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZJ.Intern4job.Utility.Common;

namespace ZJ.Intern4job.Utility.Helper
{
    public class ExceptionHelper
    {
        public static string GetInnerExceptionInfo(Exception ex)
        {
            string result = string.Empty;
            if (ex == null)
            {
                result = string.Empty;
            }
            else
            {
                if (ex.InnerException == null)
                {
                    //var stackTrace = ex.StackTrace.Split(new[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
                    var stackTrace = ex.StackTrace;
                    result = string.Format(Intern4jobResources.MSG_INNER_EXCEPTION_INFO, stackTrace == null ? string.Empty : stackTrace.Trim(), System.Environment.NewLine, ex.Message);
                }
                else
                {
                    result = GetInnerExceptionInfo(ex.InnerException);
                }
            }
            return result;
        }
    }
}
