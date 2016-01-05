namespace Events
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class EventHolder
    {
        private readonly Dictionary<string, Event> eventsOrderdByTitle = new Dictionary<string, Event>();
        private readonly SortedSet<Event> eventsSortedByDate = new SortedSet<Event>();
        private readonly StringBuilder output;

        public EventHolder(StringBuilder output)
        {
            this.output = output;
        }

        public void AddEvent(DateTime date, string title, string location)
        {
            Event newEvent = new Event(date, title, location);
            this.eventsOrderdByTitle.Add(title.ToLower(), newEvent);
            this.eventsSortedByDate.Add(newEvent);
            Messages.EventAdded(this.output);
        }

        public void DeleteEvents(string titleToDelete)
        {
            int removedEventsCount = 0;
            string title = titleToDelete.ToLower();

            if (this.eventsOrderdByTitle.ContainsKey(title))
            {
                this.eventsSortedByDate.Remove(this.eventsOrderdByTitle[title]);
                this.eventsOrderdByTitle.Remove(title);
                removedEventsCount++;
            }

            Messages.EventDeleted(removedEventsCount, this.output);
        }

        public void ListEvents(DateTime date, int count)
        {
            IEnumerable<Event> eventsToShow = this.eventsSortedByDate.Where(ev => ev.Date == date);
            int showed = 0;

            foreach (var eventToShow in eventsToShow)
            {
                if (showed == count)
                {
                    break;
                }

                Messages.PrintEvent(eventToShow, this.output);
                showed++;
            }

            if (showed == 0)
            {
                Messages.NoEventsFound(this.output);
            }
        }
    }
}
