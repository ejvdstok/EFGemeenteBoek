using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Entities
{
    public class Persoon
    {
        public int PersoonId { get; set; }
        public string VoorNaam { get; set; }
        public string FamilieNaam { get; set; }
        public enum Geslacht { M, V }
        public DateTime GeboorteDatum { get; set; }
        public int AdresId { get; set; }
        public int GeboorteplaatsId { get; set; }

        public bool Geblokkeerd = false;
        public string TelefoonNr { get; set; }
        public string LoginNaam { get; set; }
        public string LoginPaswoord { get; set; }
        public int VerkeerdeLoginsAantal;
        public int LoginAantal;
        public int TaalId { get; set; }
        public byte[] Aangepast { get; set; } // Concurrency

        // ---------------------
        // Navigation Properties
        // ---------------------

        public virtual Taal Taal { get; set; }
        public virtual Gemeente GeboortePlaats { get; set; }
        public virtual Adres Adres { get; set; }

    }
}
