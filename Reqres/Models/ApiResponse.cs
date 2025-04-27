using System;
using System.Collections.Generic;
using System.Text;

namespace Reqres.Models
{
    public class ApiResponse<T>
    {
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public List<T> Data { get; set; }
    }
}
