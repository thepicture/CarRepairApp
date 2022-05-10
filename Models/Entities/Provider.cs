using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarRepairApp.Models.Entities
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class Provider : BaseEntity
    {
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        public ICollection<Part> Parts { get; set; }
    }
}