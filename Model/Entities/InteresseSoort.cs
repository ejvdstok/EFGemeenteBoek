using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Entities
{
    public class InteresseSoort
    {
        public int InteresseSoortId { get; set; }
        public string InteresseSoortNaam { get; set; }

        // ---------------------
        // Navigation Properties
        // ---------------------
        public virtual ICollection<ProfielInteresse> ProfielInteresses { get; set; }

    }
}
