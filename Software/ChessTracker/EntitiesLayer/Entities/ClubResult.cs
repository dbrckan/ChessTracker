namespace EntitiesLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ClubResult")]
    public partial class ClubResult
    {
        [Key]
        public int clubResult_id { get; set; }

        public decimal score { get; set; }

        public int club_id { get; set; }

        public int tournament_id { get; set; }

        public virtual Club Club { get; set; }

        public virtual Tournament Tournament { get; set; }
    }
}
