namespace ForumData.Seeding
{
    using ForumDataModels;

    internal class PostSeeder
    {
        internal ICollection<Post> CreatePosts()
        {
            ICollection<Post> posts = new HashSet<Post>();

            Post post;

            post = new Post
            {
                Title = "My first post",
                Content = "Lorem Ipsum is simply dummy text of the printing and typesetting industry."
            };
            posts.Add(post);

            post = new Post
            {
                Title = "My second post",
                Content = "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s."

            };
            posts.Add(post);

            post = new Post
            {
                Title = "My third post",
                Content = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature."
            };
            posts.Add(post);

            return posts;
        }
    }
}
