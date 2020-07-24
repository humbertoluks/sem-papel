using Domain.Arguments;

namespace Service.Interfaces
{
  public interface IPushService
  {
      string GetPushCode(IPushRequest pushRequest);
  }
}
