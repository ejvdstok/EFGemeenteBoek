using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Entities
{
    public class Straat
    {
        public int StraatId { get; set; }
        public string StraatNaam { get; set; }
        public int GemeenteId { get; set; }

        // ---------------------
        // Navigation Properties
        // ---------------------
        public virtual Gemeente Gemeente { get; set; }
        public virtual ICollection<Adres> Adressen { get; set; }
    }
}
