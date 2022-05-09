using CarRepairApp.Services;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarRepairApp.Models.Entities
{
    public class Feedback : BaseEntity, IDataErrorInfo
    {
        public string this[string columnName]
        {
            get
            {
                if (columnName == "Message")
                {
                    if (string.IsNullOrWhiteSpace(Message) || Message.Length < 5)
                    {
                        return "Введите сообщение от 5 символов";
                    }
                }

                if (columnName == "Receiver")
                {
                    if (Receiver == null)
                    {
                        return "Укажите, кому отправить сообщение";
                    }
                }
                return null;
            }
        }

        public bool IsValid
        {
            get
            {
                if (this[nameof(Message)] != null) return false;
                if (this[nameof(Receiver)] != null) return false;
                return true;
            }
        }

        [Required]
        public DateTime PostingDate { get; set; }
        [Required]
        public string Message { get; set; }
        public int PosterId { get; set; }
        public int ReceiverId { get; set; }

        public virtual User Poster { get; set; }
        public virtual User Receiver { get; set; }

        public string Error => throw new NotImplementedException();
        public bool IsSelfWriting
        {
            get
            {
                return DependencyService
                    .Get<IIdentityService<User>>()
                    .WeakIdentity.Id == PosterId;
            }
        }
    }
}