using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Entities
{
    public class Profiel : Persoon
    {
        public string KennismakingsTekst { get; set; }
        public DateTime WoontHierSindsDatum { get; set; }
        public string BeroepTekst { get; set; }
        public string FirmaNaam { get; set; }
        public string WebsiteAdres { get; set; }
        public string EmailAdres { get; set; }
        public string FacebookNaam { get; set; }
        public DateTime GoedgekeurdTijdstip { get; set; }
        public DateTime CreatieTijdstip { get; set; }
        public DateTime LaatsteUpdateTijdstip { get; set; }

        // ---------------------
        // Navigation Properties
        // ---------------------
        public virtual ICollection<Bericht> Berichten { get; set; }
        public virtual ICollection<ProfielInteresse> ProfielInteresses { get; set; }
    }
}
