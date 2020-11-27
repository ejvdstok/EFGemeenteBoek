using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Entities
{
    public class Gemeente
    {
        public int GemeenteId { get; set; }
        public string GemeenteNaam { get; set; }
        public int Postcode { get; set; }
        public int ProvincieId { get; set; }
        public int? HoofdGemeenteId { get; set; }
        public int TaalId { get; set; }

        // ---------------------
        // Navigation Properties
        // ---------------------
        public virtual Taal Taal { get; set; }
        public virtual Provincie Provincie { get; set; }
        public virtual ICollection<Straat> Straten { get; set; }
        public virtual ICollection<Persoon> Personen { get; set; }

        public virtual Gemeente HoofdGemeente { get; set;  }
        public virtual ICollection<Gemeente> Gemeenten { get; set; }
    }
}
