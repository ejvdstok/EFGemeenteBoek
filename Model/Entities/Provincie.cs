using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Entities
{
    public class Provincie
    {
        public int ProvincieId { get; set; }
        public string ProvincieCode { get; set; }
        public string Provincienaam { get; set; }

        // ---------------------
        // Navigation Properties
        // ---------------------
        public virtual ICollection<Gemeente> Gemeenten { get; set; }
    }
}
