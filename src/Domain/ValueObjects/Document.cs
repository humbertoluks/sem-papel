using System;

namespace Domain.ValueObjects
{
    public class Document : ValueObject
    {
        public Document(string number)
        {
            if (number.Length != 11)
                throw new Exception("CPF inválido");
            
            Number = number;
        }
        public string Number { get; set; }

        public override string ToString()
        {
            return Number;
        }
    }
}