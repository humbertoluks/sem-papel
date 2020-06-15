using Domain.Notifications;
using Domain.ValueObjects;

namespace Domain.Models
{
    public class Student: Entity
    {
        public Student(){}
        public Student(Name name, Document document, Email email)
        {
            Name = name;
            Document = document;
            Email = email;
        }
        public Name Name { get; set; }
        public Document Document { get; set; }
        public Email Email { get; set; }
  }
}