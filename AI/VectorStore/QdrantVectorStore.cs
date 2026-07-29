using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AI.Interfaces;
using AI.Models;
using Qdrant.Client;
using Qdrant.Client.Grpc;

namespace AI.VectorStore;

public class QdrantVectorStore : IVectorStore
{
    private readonly QdrantClient _client;

    private const string CollectionName = "recipes";

    public QdrantVectorStore()
    {
        _client = new QdrantClient("localhost", 6334);

    }
    public async Task InitializeAsync()
    {
        var collections = await _client.ListCollectionsAsync();

        if (!collections.Contains(CollectionName))
        {
            await _client.CreateCollectionAsync(
                CollectionName,
                new VectorParams
                {
                    Size = 3072,
                    Distance = Distance.Cosine
                });
        }
    }
    public async Task StoreAsync(VectorDocument document)
    {
        var point = new Qdrant.Client.Grpc.PointStruct
        {
            Id = (ulong)long.Parse(document.Id),
            Vectors = document.Vector
        };

        foreach (var item in document.Metadata)
        {
            point.Payload.Add(item.Key, item.Value.ToString());
        }

        await _client.UpsertAsync(
            collectionName: CollectionName,
            points: new[] { point }
        );
    }
    public async Task<List<VectorDocument>> SearchAsync(float[] vector, int limit = 5)
    {
        var searchResult = await _client.SearchAsync(
            collectionName: CollectionName,
            vector: vector,
            limit: (ulong)limit
        );

        return searchResult.Select(hit => new VectorDocument
        {
            Id = hit.Id.ToString(),
            Metadata = hit.Payload.ToDictionary(
                p => p.Key,
                p => (object)p.Value.ToString()
            )
        }).ToList();
    }
}