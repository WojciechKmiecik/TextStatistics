using System;
using System.Collections.Generic;
using System.Text;

namespace TextStatistics.Definition.Models
{
    public class TextStatistics
    {
        public uint HyphenCount { get; set; }
        public uint WordCount { get; set; }
        public uint SpacesCount { get; set; }
        public string Guid { get; set; }
        public long Id { get; set; }
    }
}
