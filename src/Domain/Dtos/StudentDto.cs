using Domain.ValueObjects;

namespace Domain.Dtos
{
    public class StudentDto
    {
        public int Id { get; set; }
        public Name Name { get; set; }
        public Document Document { get; set; }
        public Email Email { get; set; }
    }
}