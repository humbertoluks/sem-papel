using Domain.Arguments;

namespace Service.Interfaces
{
  public interface IBeneficiarioService
  {
    BeneficiarioResponse SeachBeneficiario(string cartaoBeneficiario);
  }
}
