using System;
using System.Collections.Generic;
using System.Text;
using GSW.Domain.Domain.Backlogs.Shared;

namespace GSW.Domain.Domain.Dictionaries
{
    public class BacklogDictionary
    {
        public int Id { get; set; }
        public BacklogTypeEnum Type { get; set; }
    }
}
