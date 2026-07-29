using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AI.Models;

namespace AI.Interfaces
{
    public interface IVectorStore
    {
        Task StoreAsync(VectorDocument document);

        Task<List<VectorDocument>> SearchAsync(
            float[] vector,
            int limit = 5
        );
    }
}
