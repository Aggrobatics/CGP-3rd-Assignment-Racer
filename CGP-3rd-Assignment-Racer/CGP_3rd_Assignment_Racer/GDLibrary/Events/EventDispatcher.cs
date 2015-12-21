using GDGame;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GDLibrary
{
    //adding events - step 1 add a delegate - this stores a list of all interested listener callback methods
    public delegate void MouseEventHandler(EventData eventData);
    //add more here...

    public class EventDispatcher : GameComponent
    {
        //event collection
        private static Stack<EventData> stack = new Stack<EventData>();

        //adding events - step 2 add an event type - this is triggered when we receive an EventData of the correct type
        public event MouseEventHandler MouseEvent, MouseClickEvent;


        public EventDispatcher(Main game)
            : base(game)
        {

        }

        //called by any entity wishing to add an event to the stack for processing
        public static void Publish(EventData eventData)
        {
            //if we would like to ensure uniqueness of events we can use a HashSet to identify duplicates
            stack.Push(eventData);
        }

        //process all events in the stack
        //how will stack size affect the time taken to process any one event?
        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < stack.Count; i++)
            {
                Process(stack.Pop());
            }

            base.Update(gameTime);
        }

        private void Process(EventData eventData)
        {
            //adding events - step 3 - process the EventData and generate the apprropriate event e.g. OnMouseEvent
            switch (eventData.EventType)
            {
                case EventType.MouseEnter: OnMouseEvent(eventData);
                    break;
                case EventType.MouseExit: OnMouseEvent(eventData);
                    break;
                case EventType.MouseLeftClick: OnMouseClickEvent(eventData);
                    break;
                case EventType.MouseRightClick: OnMouseClickEvent(eventData);
                    break;
                default:
                    break;
            }
        }

        //called when a mouse over event needs to be generated
        protected virtual void OnMouseEvent(EventData eventData)
        {
            //adding events - step 4 - generate the event! (MouseClickEvent is non-null if an object has subscribed to this event)
            if (MouseClickEvent != null)
                MouseClickEvent(eventData);
        }

        //called when a mouse click event needs to be generated
        protected virtual void OnMouseClickEvent(EventData eventData)
        {
            //non-null if an object has subscribed to this event
            if (MouseEvent != null)
                MouseEvent(eventData);
        }


        //remove all events - we could use this for a restart - see Main::DoRestart()
        public static void Clear()
        {
            if (stack.Count > 0)
                stack.Clear();
        }
    }
}
