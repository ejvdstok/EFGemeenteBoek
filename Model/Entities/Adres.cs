using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Entities
{
    public class Adres
    {
        // ----------
        // Properties
        // ----------
        
        public int AdresId { get; set; }
        public int StraatId { get; set; }
        public string HuisNr { get; set; }
        public string BusNr { get; set; }
        public byte[] Aangepast { get; set; } // Concurrency

        // ---------------------
        // Navigation Properties
        // ---------------------

        public virtual Straat Straat { get; set; }
        public virtual ICollection<Persoon> Personen { get; set; }


    }
}
