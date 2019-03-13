namespace ISApp.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ApplicationKey")]
    public partial class ApplicationKey
    {
        public ApplicationKey()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid AppKey { get; set; }

        [Required]
        [StringLength(50)]
        public string AppName { get; set; }
    }
}

