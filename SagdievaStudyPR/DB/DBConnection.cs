using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagdievaStudyPR.DB
{
    internal class DBConnection
    {
        public static SagdievaStudyPREntities sagdievaStudyPREntities = new SagdievaStudyPREntities();
        public static Worker loginedUser;
    }
}
