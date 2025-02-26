namespace EntitiesLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GameHistory")]
    public partial class GameHistory
    {
        [Key]
        [Column(Order = 0, TypeName = "date")]
        public DateTime date { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int record_id { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int player_id { get; set; }

        public virtual GameRecord GameRecord { get; set; }

        public virtual Player Player { get; set; }
    }
}
