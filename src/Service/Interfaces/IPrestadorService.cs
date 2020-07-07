namespace Service.Interfaces
{
  public interface IPrestadorService
  {
    string PrestadorDescription(string codigoPrestador);
    string PrestadorMedico(string pUfCrm, int pNrCrm);
  }
}