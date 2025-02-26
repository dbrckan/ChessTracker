namespace EntitiesLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Judge")]
    public partial class Judge
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Judge()
        {
            Tournament = new HashSet<Tournament>();
        }

        [Key]
        public int judge_id { get; set; }

        [Required]
        [StringLength(30)]
        public string firstName { get; set; }

        [Required]
        [StringLength(40)]
        public string lastName { get; set; }

        [Required]
        [StringLength(30)]
        public string contact { get; set; }

        [Required]
        [StringLength(50)]
        public string username { get; set; }

        [StringLength(200)]
        public string password { get; set; }

        public int federation_id { get; set; }

        public virtual ChessFederation ChessFederation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tournament> Tournament { get; set; }
    }
}
