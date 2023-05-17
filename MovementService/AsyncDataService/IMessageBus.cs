using MovementService.Dtos;

namespace MovementService.AsyncDataService
{
    public interface IMessageBus
    {
        void PublishNewMovement(MovementPublishedDto movementPublished);
    }
}