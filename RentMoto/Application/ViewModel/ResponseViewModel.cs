using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModel
{
    public class ResponseViewModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public ResponseViewModel(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}
