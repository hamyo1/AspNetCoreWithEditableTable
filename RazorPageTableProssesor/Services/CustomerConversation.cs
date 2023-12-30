using Microsoft.Extensions.Logging;
using RazorPageTableProssesor.Interfaces;
using RazorPageTableProssesor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorPageTableProssesor.Services
{
    public class CustomerConversation : ICustomerConversation
    {
        readonly ILogger<CustomerConversation> _logger;

        public CustomerConversation(ILogger<CustomerConversation> logger)
        {
            _logger = logger;
        }


        public async Task<bool> OnNewActivity(ActivityEventArgs eventArgs)
        {
            bool isSuccess = false;

            if (eventArgs != null)
            {

                try
                {
                    //logic here
                }
                catch (Exception ex)
                {
                    _logger.LogError($"An error occurred on UploadFullConversation: {ex.Message}, {ex.StackTrace}");
                }

            }
            else
            {
                _logger.LogError($"the request to get the full Object id: is failed");
            }
            return isSuccess;
        }
    }
}
