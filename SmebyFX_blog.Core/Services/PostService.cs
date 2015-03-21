using System.Collections.Generic;
using SmebyFX_blog.Core.Repositories;
using SmebyFX_blog.Models;

namespace SmebyFX_blog.Core.Services
{
    public class PostService
    {
        private readonly PostRepository _postRepository;

        public PostService(PostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public List<Post> GetPosts()
        {
            return _postRepository.GetPosts();
        }

        public List<Post> GetPosts(string tag)
        {
            return _postRepository.GetPostsByTag(tag);
        }
    }
}
