using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TextStatistics.Dal.DataModel
{
    internal class StatisticsEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Guid { get; set; }
        public uint HyphenCount { get; set; }
        public uint WordCount { get; set; }
        public uint SpacesCount { get; set; }
    }
}
