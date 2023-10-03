namespace Radiao.Domain.Entities
{
    public class Station
    {
        public Guid ChangeUuid { get; set; }

        public Guid StationUuid { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }
        
        public string UrlResolved { get; set; }

        public string Homepage { get; set; }

        public string Favicon { get; set; }

        public string Tags { get; set; }

        public string CountryCode { get; set; }

        public int Votes { get; set; }

        public int ClickCount { get; set; }

        public int Clicktrend { get; set; }

        public Station(
            Guid changeUuid,
            Guid stationUuid,
            string name,
            string urlResolved,
            string url,
            string homepage,
            string favicon,
            string tags,
            string countryCode,
            int votes,
            int clickCount,
            int clicktrend)
        {
            ChangeUuid = changeUuid;
            StationUuid = stationUuid;
            Name = name;
            Url = url;
            UrlResolved = urlResolved;
            Homepage = homepage;
            Favicon = favicon;
            Tags = tags;
            CountryCode = countryCode;
            Votes = votes;
            ClickCount = clickCount;
            Clicktrend = clicktrend;
        }
    }
}
