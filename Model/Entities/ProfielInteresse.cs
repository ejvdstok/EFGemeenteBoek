using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Entities
{
    public class ProfielInteresse
    {
        public int PersoonId { get; set; } 
        public int InteresseSoortId { get; set; } 
        public string ProfielInteresseTekst { get; set; }
        public byte[] Aangepast { get; set; } // Concurrency

        // ---------------------
        // Navigation Properties
        // ---------------------

        public virtual Profiel Profiel { get; set; }
        public virtual InteresseSoort InteresseSoort { get; set; }
    }
}
