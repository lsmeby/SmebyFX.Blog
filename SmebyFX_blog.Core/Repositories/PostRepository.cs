using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using SmebyFX_blog.Models;
using SmebyFX_blog.Shared.Extensions;

namespace SmebyFX_blog.Core.Repositories
{
    public class PostRepository
    {
        private readonly DocumentClient _client;
        private readonly DocumentCollection _collection;

        public PostRepository(DocumentClient client, DocumentCollection collection)
        {
            _client = client;
            _collection = collection;
        }

        public List<Post> GetPosts()
        {
            return _client.CreateDocumentQuery<Post>(_collection.DocumentsLink)
                .Materialize();
        }

        public List<Post> GetPostsByTag(string tag)
        {
            // Heavily uses the database since it retrieves all posts, but LINQ to DocumentDB only supports Select and Where
            return GetPosts()
                .Where(p => p.Tags.Contains(tag, StringComparer.OrdinalIgnoreCase))
                .Materialize();
        }
    }
}
