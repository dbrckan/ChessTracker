namespace EntitiesLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Tournament")]
    public partial class Tournament
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tournament()
        {
            ClubResult = new HashSet<ClubResult>();
            PlayerResult = new HashSet<PlayerResult>();
            RegistrationForTournament = new HashSet<RegistrationForTournament>();
            Round = new HashSet<Round>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int tournament_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime date { get; set; }

        public TimeSpan time { get; set; }

        [Required]
        [StringLength(50)]
        public string place { get; set; }

        [Required]
        [StringLength(30)]
        public string type { get; set; }

        public int numberOfRounds { get; set; }

        public int judge_id { get; set; }

        public int federation_id { get; set; }
        
        public string name { get; set; }

        public virtual ChessFederation ChessFederation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClubResult> ClubResult { get; set; }

        public virtual Judge Judge { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlayerResult> PlayerResult { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RegistrationForTournament> RegistrationForTournament { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Round> Round { get; set; }
    }
}
