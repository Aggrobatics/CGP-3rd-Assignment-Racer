
namespace GDLibrary
{
    public class EventData
    {
        #region Fields
        private object sender;
        private EventType eventType;
        #endregion

        #region Properties
        public object Sender
        {
            get
            {
                return this.sender;
            }
        }
        public EventType EventType
        {
            get
            {
                return this.eventType;
            }
        }
        //why are there no set properties? does an event need to be changed?
        #endregion

        public EventData(object sender, EventType eventType)
        {
            this.sender = sender;
            this.eventType = eventType;
        }
    }
}
