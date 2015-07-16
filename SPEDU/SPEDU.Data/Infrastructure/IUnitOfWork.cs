using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPEDU.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        int Commit();
    }
}
