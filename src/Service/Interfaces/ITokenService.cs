using Domain.Dtos.Push;

namespace Service.Interfaces
{
  public interface IPushService
  {
      string Post(PushRequest pushRequest);
  }
}