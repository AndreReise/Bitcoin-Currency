
using System;
using System.Net;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Bitcoin_Currency
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Start receiving");
            Begin();
            Console.ReadKey();
        }
        static async void Begin()
        {

            var info = GetExchangeInfo("EUR", "ETH");


            Console.WriteLine(string.Format("{0} {1} {2}", DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString(), info.FriendlyLast));
            if (Calculate(info.FriendlyLast) == false)
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"Resources/Alarm.wav");
                player.Play();
                await Task.Delay(3000);
            }

            await Task.Delay(3000);
            Begin();
        }
        static bool Calculate(string tmp)
        {
            Char delitimer = ' ';
            String[] income = tmp.Split(delitimer);

            double EthCurrency =  Convert.ToDouble(income[3]);
            const double TotalHashRate = 241.207;
            const double HashRate = 136;
            const double PowerUsage = 99.8;
            const double PowerCost = 0.03629;


            if (((HashRate * 100) / TotalHashRate) * EthCurrency > PowerCost * PowerUsage)
            {
                return true;
            }
            else return false;

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