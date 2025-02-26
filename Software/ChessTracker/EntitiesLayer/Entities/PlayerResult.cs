namespace EntitiesLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PlayerResult")]
    public partial class PlayerResult
    {
        [Key]
        public int playerResult_id { get; set; }

        public decimal score { get; set; }

        public int player_id { get; set; }

        public int tournament_id { get; set; }

        public virtual Player Player { get; set; }

        public virtual Tournament Tournament { get; set; }
    }
}
