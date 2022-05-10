using System;
using System.ComponentModel.DataAnnotations;

namespace CarRepairApp.Models.Entities
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class WorkProcess : BaseEntity
    {
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }
        [Required]
        public DateTime StartOfWork { get; set; }
        [Required]
        public decimal PriceInRubles { get; set; }
        public int WorkerId { get; set; }
        public int CustomerId { get; set; }
        public virtual int ProcessStageId { get; set; }
        public int CarId { get; set; }

        public virtual User Worker { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ProcessStage ProcessStage { get; set; }
        public virtual Car Car { get; set; }
    }
}
