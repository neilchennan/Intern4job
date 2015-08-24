using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZJ.Intern4job.Model.DAL;

namespace ZJ.Intern4job.Model.Repository
{
    public class CandidateRepository: BaseRepository<Candidate>
    {
        public CandidateRepository(Intern4jobEntities context) : base(context) { }
    }
}
