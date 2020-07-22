namespace Domain.ValueObjects
{
    public class Localizacao: ValueObject
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}