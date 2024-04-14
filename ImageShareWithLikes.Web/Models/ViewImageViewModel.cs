using ImageShareWithLikes.Data;

namespace ImageShareWithLikes.Web.Models
{
    public class ViewImageViewModel
    {
        public Image Image { get; set; }
        public List<int> Ids { get; set; }
    }
}
