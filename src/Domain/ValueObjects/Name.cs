using System;

namespace Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public Name(string firstname, string lastname)
        {
            if (firstname.Length < 5)
                throw new Exception("Nome Inválido");
            
            if (lastname.Length < 5)
                throw new Exception("Sobrenome Inválido");
            
            Firstname = firstname;
            Lastname = lastname;
        }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        
        public override string ToString(){
            return $"{Firstname} {Lastname}";
        }
    }
}