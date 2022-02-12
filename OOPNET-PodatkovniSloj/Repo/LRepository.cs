using OOPNET_PodatkovniSloj.Models;
using OOPNET_PodatkovniSloj.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace OOPNET_PodatkovniSloj
{
    class LRepository : IRepo
    {
        private const char DEL = '|';

        public Task<List<CountryInfo>> GetDrzave(Championship prvenstvo)
        {
            string p = GetChampionshipUrl(prvenstvo);
            return Task.Run(() =>
            {
                using (StreamReader r = new StreamReader("../../../worldcup.sfg.io/" + p + "/results.json"))
                {
                    string json = r.ReadToEnd();
                    return JsonConvert.DeserializeObject<List<CountryInfo>>(json);
                }
            });
        }

        private string GetChampionshipUrl(Championship prvenstvo)
        {
            string p = null;
            switch (prvenstvo)
            {
                case Championship.Musko:
                    p = "men";
                    break;
                case Championship.Zensko:
                    p = "women";
                    break;
                default:
                    break;
            }
            return p;
        }

        public Task<List<Match>> GetMatches(string fifacode, Championship prevenstvo)
        {
            string p = GetChampionshipUrl(prevenstvo);

            return Task.Run(() =>
            {
                string json = "";
                using (StreamReader r = new StreamReader("../../../worldcup.sfg.io/" + p + "/matches.json"))
                {
                    json = r.ReadToEnd();

                }
                var list = JsonConvert.DeserializeObject<List<Match>>(json).Where(s => s.HomeTeam.Code == fifacode || s.AwayTeam.Code == fifacode).ToList();

                return Task.FromResult(list);
            });
        }

        public string[] ProcitajPostavke()
        {
            var lines = File.ReadAllLines("../../../podstavke.txt");
            string[] data = lines[0].Split(DEL);
            return data;
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
