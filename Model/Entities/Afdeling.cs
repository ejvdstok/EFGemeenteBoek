using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Entities
{
    public class Afdeling
    {
        public int AfdelingId { get; set; }
        public string AfdelingCode { get; set; }
        public string AfdelingNaam { get; set; }
        public string AfdelingTekst { get; set; }

        // ---------------------
        // Navigation Properties
        // ---------------------

        public virtual ICollection<Medewerker> Medewerkers { get; set; }
    }
}
