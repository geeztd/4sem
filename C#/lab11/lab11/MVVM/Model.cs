using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab11.MVVM {
    public class Consultation {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string time { get; set; }

        public DateOnly date { get; set; }

        public bool isFree { get; set; }

        public Consultation(string name, string subject, string time, DateOnly date, bool isFree = true) {
            this.Name = name;
            this.Subject = subject;
            this.time = time;
            this.date = date;
            this.isFree = isFree;
        }

    }
}

