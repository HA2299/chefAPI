using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Models
{
    public class VectorDocument
    {
        public string Id { get; set; } = string.Empty;

        public float[] Vector { get; set; } = Array.Empty<float>();

        public Dictionary<string, object> Metadata { get; set; } = new();
    }
}
