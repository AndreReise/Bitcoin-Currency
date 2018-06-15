
using System;
using System.Net;
using Newtonsoft.Json;

namespace Bitcoin_Currency
{
    class Program
    {
        static void Main()
        {
            var info = GetExchangeInfo("EUR", "BTC");

            var dt = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddMilliseconds(info.Timestamp).ToLocalTime();

            Console.WriteLine(string.Format("{0} {1} {2}", dt.ToShortDateString(), dt.ToShortTimeString(), info.FriendlyLast));
            Main();
        }
        static ExchangeInfo GetExchangeInfo(string from, string to)
        {
            using (WebClient client = new WebClient())
            {
                var json = client.DownloadString(string.Format(@"https://spectrocoin.com/scapi/ticker/{0}/{1}/", from, to));
                return JsonConvert.DeserializeObject<ExchangeInfo>(json);
            }

        }

    }
    public class ExchangeInfo
    {
        [JsonProperty("currencyFrom")]
        public string CurrencyFrom { get; set; }
        [JsonProperty("currencyFromScale")]
        public int CurrencyFromScale { get; set; }
        [JsonProperty("currencyTo")]
        public string CurrencyTo { get; set; }
        [JsonProperty("currencyToScale")]
        public int CurrencyToScale { get; set; }
        [JsonProperty("last")]
        public double Last { get; set; }
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }
        [JsonProperty("friendlyLast")]
        public string FriendlyLast { get; set; }
    }
}