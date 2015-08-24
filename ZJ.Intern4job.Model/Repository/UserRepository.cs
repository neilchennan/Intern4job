using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZJ.Intern4job.Model.DAL;

namespace ZJ.Intern4job.Model.Repository
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(Intern4jobEntities context) : base(context) { }
    }
}
