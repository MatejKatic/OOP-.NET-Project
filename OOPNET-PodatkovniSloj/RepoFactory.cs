using OOPNET_PodatkovniSloj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Configuration;

namespace OOPNET_PodatkovniSloj.Repo
{
    public static class RepoFactory
    {
        public static IRepo GetRepository()
        {
            switch ((RepoType)Properties.SelectRepository.Default.RepoSelected)
            {
                case RepoType.API:
                    return new Repository();
                case RepoType.LocalRepository:
                    return new LRepository();
                default:
                    return new LRepository();
            }
        }
    }
}
