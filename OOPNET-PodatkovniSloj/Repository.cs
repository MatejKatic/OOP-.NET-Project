using Newtonsoft.Json;
using OOPNET_PodatkovniSloj.Models;
using OOPNET_PodatkovniSloj.Repo;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPNET_PodatkovniSloj
{
    class Repository : IRepo
    {

        private const char DEL = '|';
        private const string URLmusko = "http://world-cup-json-2018.herokuapp.com/";
        private const string URLzensko = "http://worldcup.sfg.io/";
        public Task<List<CountryInfo>> GetDrzave(Championship prvenstvo)
        {
            string url = GetFullUrlByChampionship(prvenstvo);
            return Task.Run(() =>
            {
                var apiClient = new RestClient(url);
                IRestResponse<CountryInfo> apiResult = apiClient.Execute<CountryInfo>(new RestRequest());
                return JsonConvert.DeserializeObject<List<CountryInfo>>(apiResult.Content);
            });
        }

        private string GetFullUrlByChampionship(Championship prvenstvo)
        {
            string url = String.Empty;

            switch (prvenstvo)
            {
                case Championship.Musko:
                    url = URLmusko + "teams/results";
                    break;
                case Championship.Zensko:
                    url = URLzensko + "teams/results";
                    break;
            }
            return url;
        }

        public Task<List<Match>> GetMatches(string fifacode, Championship prvenstvo)
        {
            string url = GetUrl(fifacode, prvenstvo);

            return Task.Run(() =>
            {
                var apiClient = new RestClient(url);
                IRestResponse<Match> apiResult = apiClient.Execute<Match>(new RestRequest());
                return JsonConvert.DeserializeObject<List<Match>>(apiResult.Content);
            });
        }

        private string GetUrl(string fifacode, Championship prvenstvo)
        {
            string url = String.Empty;

            switch (prvenstvo)
            {
                case Championship.Musko:
                    url = URLmusko + "matches/country?fifa_code=" + fifacode;
                    break;
                case Championship.Zensko:
                    url = URLzensko + "matches/country?fifa_code=" + fifacode;
                    break;
            }
            return url;
        }

        public string[] ProcitajPostavke()
        {
            var lines = File.ReadAllLines("../../../podstavke.txt");
            string[] data = lines[0].Split(DEL);
            return data; ;
        }

        public void ZapisiPostavke(string jezik, string prvenstvo, string kodreprezentacije, List<string> igraci, string resolution)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(jezik);
            sb.Append(DEL);
            sb.Append(prvenstvo);
            sb.Append(DEL);
            sb.Append(kodreprezentacije);

            if (igraci != null)
            {
                foreach (var igrac in igraci)
                {
                    sb.Append(DEL);
                    sb.Append(igrac);
                }
            }
            sb.Append(DEL);
            sb.Append(resolution);
            File.WriteAllText("../../../podstavke.txt", sb.ToString());
        }
    }
}
