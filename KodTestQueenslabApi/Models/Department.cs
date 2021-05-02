using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KodTestQueenslabApi.Models
{
    // This model is for the API to track every department, and the details.
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
    }
}
