﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NetCoreApp.Models
{
    public class Match
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

        [Required]
        public string Name { get; set; }

        [DisplayName("Display Order")]
        [Range(1,100, ErrorMessage ="Display Order must be between 1 and 100 only")]
        public int DisplayOrder { get; set; }

    }
}
