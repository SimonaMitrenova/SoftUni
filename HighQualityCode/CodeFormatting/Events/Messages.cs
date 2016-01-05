namespace Events
{
    using System.Text;

    public static class Messages
    {
        public static void EventAdded(StringBuilder output)
        {
            output.AppendLine("Event added");
        }

        public static void EventDeleted(int deletedEventsCount, StringBuilder output)
        {
            if (deletedEventsCount == 0)
            {
                NoEventsFound(output);
            }
            else
            {
                output.AppendLine(string.Format("{0} events deleted", deletedEventsCount));
            }
        }

        public static void NoEventsFound(StringBuilder output)
        {
            output.AppendLine("No events found");
        }

        public static void PrintEvent(Event eventToPrint, StringBuilder output)
        {
            if (eventToPrint != null)
            {
                output.AppendLine(eventToPrint.ToString());
            }
        }
    }
}
