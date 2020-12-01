using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Entities
{
    public class Bericht
    {
        public int BerichtId { get; set; }
        public int HoofdBerichtId { get; set; }
        public int GemeenteId { get; set; }
        public int PersoonId { get; set; }
        public int BerichtTypeId { get; set; }
        public DateTime BerichtTijdstip { get; set; }
        public string BerichtTitel { get; set; }
        public string BerichtTekst { get; set; }

        public int Level { get; set; }

        public byte[] Aangepast { get; set; }

        // ---------------------
        // Navigation Properties
        // ---------------------
        public virtual Profiel Profiel { get; set; }
        public virtual BerichtType BerichtType { get; set; }

        public virtual Bericht HoofdBericht { get; set; }

        public virtual Gemeente Gemeente { get; set; }

        public virtual ICollection<Bericht> Berichten { get; set; }
    }
}
