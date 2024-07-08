using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idimon
{
    public abstract class Idimons
    {
        public int Id { get; set; }

        public IdimonType Type { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public string HP { get; set; }

        public string Attack { get; set; }

        public string Defense { get; set; }

        public string Speed { get; set; }
    }
}
