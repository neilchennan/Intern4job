﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZJ.Intern4job.Model.Query
{
    public class UserQuery : BaseQuery
    {
        public string StatusEqual { set; get; }
        public string StatusNotEqual { set; get; }

        public string[] StatusIn { set; get; }
        public string[] StatusNotIn { set; get; }
    }
}
