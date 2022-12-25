using System.Collections.Generic;

namespace Bad.Code.BadSmells._05MutableData
{
    public class MiscreantService
    {
        public string AlertForMiscreants(List<string> people)
        {
            foreach (var p in people)
            {
                if (p == "Don")
                {
                    setOffAlarms();
                    return "Don";
                }
                else if (p == "John")
                {
                    setOffAlarms();
                    return "John";
                }
            }

            return "";
        }

        private void setOffAlarms() { }
    }
}