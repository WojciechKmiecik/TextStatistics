using System;
using System.Collections.Generic;
using System.Text;
using TextStatistics.Dal.DataModel;


namespace TextStatistics.Dal.Mappings
{
    internal static class StatisticsMapper
    {
        // intentionally not using automapper. 
        public static Definition.Models.TextStatistics Map(this StatisticsEntity sE)
        {
            var tsm = new Definition.Models.TextStatistics();
            if (sE != null)
            {
                tsm.Id = sE.Id;
                tsm.Guid = sE.Guid;
                tsm.HyphenCount = sE.HyphenCount;
                tsm.SpacesCount = sE.SpacesCount;
                tsm.WordCount = sE.WordCount;
            }
            return tsm;
        }

        public static StatisticsEntity Map(this Definition.Models.TextStatistics tsm)
        {
            var se = new StatisticsEntity();
            if (tsm != null)
            {
                se.Id = tsm.Id;
                se.Guid = tsm.Guid;
                se.HyphenCount = tsm.HyphenCount;
                se.SpacesCount = tsm.SpacesCount;
                se.WordCount = tsm.WordCount;
            }
            return se;
        }
    }
}
