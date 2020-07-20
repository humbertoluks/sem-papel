namespace Service.Interfaces
{
  public interface IPrestadorService
  {
    string PrestadorDescription(string codigoPrestador);
    string PrestadorMedico(string codigoPrestador, string ufCrm, int nrCrm, string nomeMedico);
  }
}