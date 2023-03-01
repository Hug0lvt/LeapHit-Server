using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Factory
{
    public class ApiResponse<T>
    {
        public string Message { get; set; }
        public T Data { get; set; }
        public List<ApiLink> Links { get;  set; }
        private List<ApiLink> links = new();


        public ApiResponse(string message, T data = default)
        {
            Message = message;
            Data = data;
        }
    }
}
