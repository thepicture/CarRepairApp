using System.ComponentModel.DataAnnotations;

namespace CarRepairApp.Models.Entities
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class Part : BaseEntity
    {
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }
        [Required]
        [MaxLength(50)]
        public string BaseCode { get; set; }
        public byte[] Photo { get; set; }
        [Required]
        public decimal PriceInRubles { get; set; }
        [MaxLength(2048)]
        public string Description { get; set; }
        public int ProviderId { get; set; }
        public int CarId { get; set; }

        public virtual Provider Provider { get; set; }
        public virtual Car Car { get; set; }
    }
}
