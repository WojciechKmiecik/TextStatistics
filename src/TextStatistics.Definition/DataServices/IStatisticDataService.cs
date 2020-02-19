using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TextStatistics.Definition.DataServices
{
    public interface IStatisticDataService
    {
        Task<long> AddStatistics(Models.TextStatistics model);
        Task<Models.TextStatistics> Get(string guid);
        Task<List<Models.TextStatistics>> GetAll();
        Task UpdateStatistics(Definition.Models.TextStatistics model);
    }
}
