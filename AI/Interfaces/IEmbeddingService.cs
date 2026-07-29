using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Interfaces
{
    public interface IEmbeddingService
    {
        Task<float[]> CreateEmbeddingAsync(string text);
    }
}
