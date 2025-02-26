namespace EntitiesLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Club")]
    public partial class Club
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Club()
        {
            ClubResult = new HashSet<ClubResult>();
            Player = new HashSet<Player>();
            RegistrationForTournament = new HashSet<RegistrationForTournament>();
        }

        [Key]
        public int club_id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        [Required]
        [StringLength(40)]
        public string address { get; set; }

        [Required]
        [StringLength(30)]
        public string contact { get; set; }

        [Column(TypeName = "date")]
        public DateTime dateOfEstablishment { get; set; }

        [Required]
        [StringLength(50)]
        public string username { get; set; }

        [StringLength(200)]
        public string password { get; set; }

        public int federation_id { get; set; }

        public virtual ChessFederation ChessFederation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClubResult> ClubResult { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Player> Player { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RegistrationForTournament> RegistrationForTournament { get; set; }
    }
}
