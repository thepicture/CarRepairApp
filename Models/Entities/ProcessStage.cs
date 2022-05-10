using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarRepairApp.Models.Entities
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ProcessStage : BaseEntity
    {
        [Required]
        public string Title { get; set; }

        public ICollection<WorkProcess> WorkProcesses { get; set; }
    }
}