using BookList.Infra.BaseClasses;
using BookList.Infra.EventStore;

namespace BookList.Infra.ReadModels
{
    public static class ReadModelRegistration
    {
        public static void Register()
        {
            EventDistributor.Publish(new EventFromES { });
        }
    }
}
