using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab9.Class {
    public class Orders {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Orders")]
        public virtual User User { get; set; }
    }
}
