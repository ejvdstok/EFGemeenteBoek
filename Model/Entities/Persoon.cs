using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Entities
{
    public class Persoon : ICloneable
    {
        private string Gegevens => $"{PersoonId} - {VoorNaam} {FamilieNaam} - {Geslacht} - {GeboorteDatum}";

        // ----------
        // Properties
        // ----------
        public int PersoonId { get; set; }
        public string VoorNaam { get; set; }
        public string FamilieNaam { get; set; }
        public Geslacht Geslacht { get; set; }          // (enum: M, V)
        public DateTime GeboorteDatum { get; set; }
        public int AdresId { get; set; }
        public int GeboorteplaatsId { get; set; }

        public bool Geblokkeerd = false;
        public string TelefoonNr { get; set; }
        public string LoginNaam { get; set; }
        public string LoginPaswoord { get; set; }
        public int VerkeerdeLoginsAantal { get; set; }
        public int LoginAantal { get; set; }
        public int TaalId { get; set; }
        public byte[] Aangepast { get; set; } // Concurrency
        public string Naam => $"{VoorNaam} {FamilieNaam}";

        // ---------------------
        // Navigation Properties
        // ---------------------

        public virtual Taal Taal { get; set; }
        public virtual Gemeente GeboortePlaats { get; set; }
        public virtual Adres Adres { get; set; }

        // -------
        // Methods
        // -------
        public override string ToString()
        {
           return Gegevens;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
