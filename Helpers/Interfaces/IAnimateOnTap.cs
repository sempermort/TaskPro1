using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskPro1.Helpers.Interfaces
{
    public interface IAnimateOnTap
    {
        Task PulseAsync(object target);
    }
}
