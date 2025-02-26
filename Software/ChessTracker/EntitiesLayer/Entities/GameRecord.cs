namespace EntitiesLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GameRecord")]
    public partial class GameRecord
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GameRecord()
        {
            ChangeOfRating = new HashSet<ChangeOfRating>();
            GameHistory = new HashSet<GameHistory>();
        }

        [Key]
        public int record_id { get; set; }

        [StringLength(20)]
        public string result { get; set; }

        public int? game_id { get; set; }

        public int? pair_id { get; set; }

        public virtual Game Game { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChangeOfRating> ChangeOfRating { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GameHistory> GameHistory { get; set; }

        public virtual Pairs Pairs { get; set; }
    }
}
