using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace bitlc_do1
{
    class ModulAction_Wiki 
    {
        public String WikiClick(string url)
        {
            string urladress = url;
            string returnstr = "";

            WebClient client = new WebClient();
            //using (Stream stream = client.OpenRead("https://de.wikipedia.org/wiki/Spezial:Zuf%C3%A4llige_Seite"))
            using (Stream stream = client.OpenRead("https://de.wikipedia.org/w/api.php?format=json&action=query&prop=extracts&explaintext=1&titles=" + urladress))
            using (StreamReader reader = new StreamReader(stream))
            {
                JsonSerializer ser = new JsonSerializer();
                Resulti result = ser.Deserialize<Resulti>(new JsonTextReader(reader));
                foreach (Page page in result.Query.Pages.Values)
                {
                    returnstr += page.Extract;
                }
            }
            return returnstr;
        }
        public class Resulti
        {
            public Query Query { get; set; }
        }
        public class Query
        {
            public Dictionary<string, Page> Pages { get; set; }
        }
        public class Page
        {
            public string Extract { get; set; }
        }

    }
}
