namespace AccountService.EventProcesing
{
    public interface IEventProcessor
    {
        void ProccessEvent(string message);
    }
}