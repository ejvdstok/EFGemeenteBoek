using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Entities
{
    public class Medewerker : Persoon
    {
        private string Gegevens => $"{PersoonId} - {AfdelingId} - {TelefoonNr}";

        public int AfdelingId { get; set; }

        // ---------------------
        // Navigation Properties
        // ---------------------

        public virtual Persoon Persoon { get; set; }
        public virtual Afdeling Afdeling { get; set; }

        // -------
        // Methods
        // -------
        public override string ToString()
        {
            return Gegevens;
        }
    }
}
