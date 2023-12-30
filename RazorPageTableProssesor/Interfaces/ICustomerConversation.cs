using RazorPageTableProssesor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorPageTableProssesor.Interfaces
{
    public interface ICustomerConversation
    {
        Task<bool> OnNewActivity(ActivityEventArgs eventArgs);
    }
}
