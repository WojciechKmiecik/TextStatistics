using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TextStatistics.Definition.Services
{
    public interface IStatisticsService
    {
        Task<string> GenerateNewStatistics(string input);
        Task<Definition.Models.TextStatistics> GetStatistics(string guid);
    }
}
