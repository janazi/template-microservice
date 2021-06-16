using Flunt.Notifications;
using System;

namespace MicroserviceBase.Domain.Entities
{
    public class Customer : Notifiable<Notification>
    {
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CPF { get; set; }
    }
}
