using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Entities
{
    public class Medewerker : Persoon
    {
        public int AfdelingId { get; set; }

        // ---------------------
        // Navigation Properties
        // ---------------------

        public virtual Persoon Persoon { get; set; }
        public virtual Afdeling Afdeling { get; set; }
    }
}
