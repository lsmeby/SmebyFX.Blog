namespace SmebyFX_blog.Core.Services
{
    public class PostService_old
    {
        public PostService_old()
        {

        }

//        public List<Domain.Post> GetPosts()
//        {
//            var posts = _postDao.GetBlogPosts();
//            posts.ForEach(AttachTags);
//            return posts;
//        }
//
//        public List<Domain.Post> GetPosts(string tagUrlSlug)
//        {
//            var tag = GetTag(tagUrlSlug);
//            var posts = _postDao.GetBlogPostsByTag(tag.Id);
//            posts.ForEach(AttachTags);
//            return posts;
//        }
//
//        public Tag GetTag(string tagUrlSlug)
//        {
//            var tag = _tagDao.GetTag(tagUrlSlug);
//
//            if (tag == null)
//            {
//                throw new ArgumentException("The provided tag doesn't exist!");
//            }
//
//            return tag;
//        }
//
//        public Domain.Post GetPost(string urlSlug, DateTime date)
//        {
//            var post = _postDao.GetPost(urlSlug, date);
//
//            if (post == null)
//            {
//                throw new ArgumentException(string.Format("Couldn't find post with url slug '{0}' from {1}", urlSlug, date.ToShortDateString()));
//            }
//
//            AttachTags(post);
//            return post;
//        }
//
//        public Domain.Post GetPost(int postId)
//        {
//            var post = _postDao.GetPost(postId);
//
//            if (post == null)
//            {
//                throw new ArgumentException(string.Format("Couldn't find post with id '{0}'", postId));
//            }
//
//            AttachTags(post);
//            return post;
//        }
//
//        public List<Domain.Post> GetPosts(DateTime date)
//        {
//            var posts = _postDao.GetPostsByDate(date);
//            posts.ForEach(AttachTags);
//            return posts;
//        }
//
//        public List<Domain.Post> GetPosts(int year, int month)
//        {
//            var posts = _postDao.GetPostsByMonth(year, month);
//            posts.ForEach(AttachTags);
//            return posts;
//        }
//
//        public List<Domain.Post> GetPosts(int year)
//        {
//            var posts = _postDao.GetPostsByYear(year);
//            posts.ForEach(AttachTags);
//            return posts;
//        }
//
//        public List<Tag> GetTags()
//        {
//            var tags = _tagDao.GetTags();
//            tags.ForEach(AttachTagCount);
//            return tags;
//        }
//
//        public void CreateOrEditTag(int? id, string title, string urlSlug)
//        {
//            if (string.IsNullOrEmpty(title))
//            {
//                throw new ArgumentNullException("Tag title must have a value.");
//            }
//            if (id == null)
//            {
//                _tagDao.Add(new Tag
//                {
//                    Title = title,
//                    UrlSlug = urlSlug
//                });
//            }
//            else
//            {
//                _tagDao.Update(new Tag
//                {
//                    Id = (int)id,
//                    Title = title,
//                    UrlSlug = urlSlug
//                });
//            }
//        }
//
//        public void CreatePost(Domain.Post post)
//        {
//            var postId = _postDao.Add(post);
//            post.Tags.Materialize().ForEach(t => _postTagDao.AddTagToPost(postId, t.Id));
//        }
//
//        public void UpdatePost(Domain.Post post)
//        {
//            _postDao.Update(post);
//            _postTagDao.RemoveAllTagsFromPost(post.Id);
//            post.Tags.Materialize().ForEach(t => _postTagDao.AddTagToPost(post.Id, t.Id));
//        }
//
//        public void DeletePost(int postId)
//        {
//            _postTagDao.RemoveAllTagsFromPost(postId);
//            _postDao.Delete(postId);
//        }
//
//        public bool IsTagInUse(int tagId)
//        {
//            return _postTagDao.GetPostCount(tagId) > 0;
//        }
//
//        public void DeleteTag(int tagId)
//        {
//            _tagDao.Delete(tagId);
//        }
//
//        private void AttachTags(Domain.Post post)
//        {
//            post.Tags = _tagDao.GetTagsForPost(post.Id);
//        }
//
//        private void AttachTagCount(Tag tag)
//        {
//            tag.Usages = _postTagDao.GetPostCount(tag.Id);
//        }
    }
}
