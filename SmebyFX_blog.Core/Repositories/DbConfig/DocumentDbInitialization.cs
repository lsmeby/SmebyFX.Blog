using System.Linq;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace SmebyFX_blog.Core.Repositories.DbConfig
{
    public class DocumentDbInitialization
    {
        private const string DatabaseId = "SmebyFX_blog";
        private const string PostCollectionId = "PostCollection";

        private readonly DocumentClient _client;
        private readonly Database _database;

        public DocumentDbInitialization(DocumentClient client)
        {
            _client = client;
            _database = _client.CreateDatabaseQuery()
                            .ToArray()
                            .FirstOrDefault(db => db.Id == DatabaseId) ??
                        _client.CreateDatabaseAsync(new Database {Id = DatabaseId}).Result;
        }

        public DocumentCollection GetPostCollection()
        {
            return
                _client.CreateDocumentCollectionQuery(_database.CollectionsLink)
                    .ToArray()
                    .FirstOrDefault(c => c.Id == PostCollectionId) ??
                _client.CreateDocumentCollectionAsync(_database.CollectionsLink,
                    new DocumentCollection {Id = PostCollectionId}).Result;
        }
    }
}
    