using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KodTestQueenslabApi.Models
{
    public class Visitor
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime Time { get; set; }
    }
}
