using Domain.Arguments;

namespace Service.Interfaces
{
  public interface IPrestadorService
  {
    string PrestadorDescription(string codigoPrestador);
    MedicoResponse PrestadorMedico(string codigoPrestador, string ufCrm, int nrCrm, string nomeMedico);
  }
}
