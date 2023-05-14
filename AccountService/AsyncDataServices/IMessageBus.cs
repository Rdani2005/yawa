using AccountService.Dtos;

namespace AccountService.AsyncDataServices
{
    public interface IMessageBus
    {
        void PublishNewAccount(AccountPublishedDto accountPublished);
    }
}