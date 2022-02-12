using OOPNET_PodatkovniSloj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPNET_PodatkovniSloj.Repo
{
    public interface IRepo
    {
        Task<List<Match>> GetMatches(string fifacode, Championship prevenstvo);
        Task<List<CountryInfo>> GetDrzave(Championship prevenstvo);
        void ZapisiPostavke(string jezik, string prvenstvo, string kodreprezentacije, List<string> igraci, string resolution);
        string[] ProcitajPostavke();
    }
}
