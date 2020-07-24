using Domain.ValueObjects;

namespace Domain.Arguments
{
  public interface IPushRequest
  {
    string CodAtendimento { get; set; }
    IntPrestador Prestador { get; set; }
    IntAssociado Associado { get; set; }
  }

  public class PushRequest : IPushRequest
  {
    public PushRequest()
    {
        Prestador = new IntPrestador(){ 
            Localizacao =  new Localizacao{
            Latitude = "",
            Longitude = ""
        }};
        Associado = new IntAssociado(); 
    }
    public string CodAtendimento { get; set; }
    public IntPrestador Prestador { get; set; }
    public IntAssociado Associado { get; set; }
  }

  public class IntPrestador
  {
    public string Endereco { get; set; }
    public string CodPrestador { get; set; }
    public string NomePrestador { get; set; }
    public string NomeProfissional { get; set; }
    public Localizacao Localizacao { get; set; }
  }

  public class IntAssociado
  {
      public string CodAssociado { get; set; }
      public string CodAcompanhante { get; set; }
      public Localizacao Localizacao { get; set; }
  }
}
