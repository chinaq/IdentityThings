using System.Collections.Generic;

namespace Api.Data
{
    public class Blog
    {
        public int ID { get; set; }
        public string Owner { get; set; }
        public List<Post> Posts { get; set; }
    }
}