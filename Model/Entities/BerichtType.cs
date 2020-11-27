using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Entities
{
    public class BerichtType
    {
        public int BerichtTypeId { get; set; }
        public string BerichtTypeCode { get; set; }
        public string BerichtTypeNaam { get; set; }
        public string BerichtTypeTekst { get; set; }
        // ---------------------
        // Navigation Properties
        // ---------------------
        
        public virtual ICollection<Bericht>Berichten{ get; set; }
    }
}
