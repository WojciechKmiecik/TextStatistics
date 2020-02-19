using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextStatistics.Dal.Mappings;
using TextStatistics.Definition.DataServices;

namespace TextStatistics.Dal.DataServices
{

    internal class StatisticDataService : IStatisticDataService
    {
        private readonly TextStatisticsContext _textStatisticsContext;

        public StatisticDataService(TextStatisticsContext textStatisticsContext)
        {
            _textStatisticsContext = textStatisticsContext;
        }

        public async Task<List<Definition.Models.TextStatistics>> GetAll()
        {
            return await _textStatisticsContext.Statistics.Select(x => x.Map()).ToListAsync();
        }
        public async Task<Definition.Models.TextStatistics> Get(string guid)
        {
            return await _textStatisticsContext.Statistics
                .Where(x => string.Equals(x.Guid, guid, StringComparison.OrdinalIgnoreCase))
                .Select(x => x.Map()).FirstOrDefaultAsync();
        }
        public async Task<long> AddStatistics(Definition.Models.TextStatistics model)
        {
           var entry =  await _textStatisticsContext.Statistics.AddAsync(model.Map());
            await _textStatisticsContext.SaveChangesAsync();
            return entry?.Entity?.Id ?? 0;
        }
        public async Task UpdateStatistics(Definition.Models.TextStatistics model)
        {
            var entity = _textStatisticsContext.Statistics.Update(model.Map());
            await _textStatisticsContext.SaveChangesAsync();
        }

    }
}
