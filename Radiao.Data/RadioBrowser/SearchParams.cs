namespace Radiao.Data.RadioBrowser
{
    public class SearchParams
    {
        public int Limit { get; set; } = 10;

        public SearchCountryCode CountryCode { get; set; } = SearchCountryCode.BR;

        public bool Reverse { get; set; } = true;

        public bool HideBroken { get; set; } = true;

        public SearchOrder Order { get; set; } = SearchOrder.Clickcount;

        public string TagList { get; set; } = "";
    }

    public enum SearchOrder
    {
        Votes,
        Clickcount
    }

    public enum SearchCountryCode
    {
        BR
    }
}
