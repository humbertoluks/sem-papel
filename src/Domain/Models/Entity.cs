using Domain.Notifications;

namespace Domain.Models
{
    public class Entity: Notifiable
    {
        public int Id { get; set; }
    }
}