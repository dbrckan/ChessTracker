namespace EntitiesLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RegistrationForTournament")]
    public partial class RegistrationForTournament
    {
        [Key]
        public int registration_id { get; set; }

        [Required]
        [StringLength(20)]
        public string registerByClub { get; set; }

        public int player_id { get; set; }

        public int club_id { get; set; }

        public int tournament_id { get; set; }

        public virtual Club Club { get; set; }

        public virtual Player Player { get; set; }

        public virtual Tournament Tournament { get; set; }
    }
}
