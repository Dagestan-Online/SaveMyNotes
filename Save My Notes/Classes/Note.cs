using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Save_My_Notes.Classes
{
    public class Note
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public bool Status { get; set; }
    }
}
