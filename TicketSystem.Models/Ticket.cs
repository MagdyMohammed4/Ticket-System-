using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystem.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public string ImagePath { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [ForeignKey("User")]
        public int UserId { get; set; }
        public AppUser User { get; set; }
        


    }
}
