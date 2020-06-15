using System;
using Domain.Notifications;

namespace Domain.ValueObjects
{
    public class Email : Notifiable
    {
        public Email(string address)
        {
            if (address.Length < 5)
                AddNotification("Email", "Email inválido");
            Address = address;
        }
        public string Address { get; set; }
        
        public override string ToString()
        {
            return Address;
        }
    }
}