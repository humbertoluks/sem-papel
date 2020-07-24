using Domain.Arguments;

namespace Service.Interfaces
{
  public interface ITokenService
  {
      string GetTokenCode(string codBeneficiario);
  }
}
