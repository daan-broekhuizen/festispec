using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel
{
    public interface IJobViewModel
    {
        int Id { get; set; }
        CustomerViewModel CustomerVM { get; set; }
    }
}
