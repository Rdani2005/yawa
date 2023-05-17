namespace MovementService.EventProcessing
{
    public interface IEventProcessor
    {
        void ProccessEvent(string message);
    }
}