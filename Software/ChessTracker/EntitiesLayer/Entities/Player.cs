namespace EntitiesLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Player")]
    public partial class Player
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Player()
        {
            Pairs = new HashSet<Pairs>();
            Pairs1 = new HashSet<Pairs>();
            ChangeOfRating = new HashSet<ChangeOfRating>();
            GameHistory = new HashSet<GameHistory>();
            PlayerResult = new HashSet<PlayerResult>();
            RegistrationForTournament = new HashSet<RegistrationForTournament>();
        }

        [Key]
        public int player_id { get; set; }

        [Required]
        [StringLength(30)]
        public string firstName { get; set; }

        [Required]
        [StringLength(40)]
        public string lastName { get; set; }

        [Column(TypeName = "date")]
        public DateTime dateOfBirth { get; set; }

        [Required]
        [StringLength(30)]
        public string contact { get; set; }

        public decimal? rating { get; set; }

        [Required]
        [StringLength(20)]
        public string gender { get; set; }

        [Required]
        [StringLength(50)]
        public string username { get; set; }

        [StringLength(200)]
        public string password { get; set; }

        public int club_id { get; set; }

        public int status_id { get; set; }

        public virtual Club Club { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pairs> Pairs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pairs> Pairs1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChangeOfRating> ChangeOfRating { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GameHistory> GameHistory { get; set; }

        public virtual Status Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlayerResult> PlayerResult { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RegistrationForTournament> RegistrationForTournament { get; set; }
    }
}
