using AuthService.Dtos;

namespace AuthService.AsyncDataServices
{
    public interface IMessageBus
    {
        void PublishNewPermition(PermitionPublishedDto permitionPublished);

        void PublishNewUser(UserPublishedDto userPublished);
    }
}