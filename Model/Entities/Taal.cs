using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Entities
{
    public class Taal
    {
        public int TaalId { get; set; }
        public string TaalCode { get; set; }
        public string TaalNaam { get; set; }

        // ---------------------
        // Navigation Properties
        // ---------------------
        public virtual ICollection<Persoon> Personen { get; set; }
        public virtual ICollection<Gemeente> Gemeenten { get; set; }
    }
}
