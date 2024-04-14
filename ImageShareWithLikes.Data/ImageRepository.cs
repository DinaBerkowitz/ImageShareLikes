using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ImageShareWithLikes.Data
{
    public class ImageRepository
    {
        private readonly string _connectionString;

        public ImageRepository(string cs)
        {
            _connectionString = cs;
        }


        public List<Image> GetAll()
        {
            using var context = new ImageDBContext(_connectionString);
            return context.Images.ToList();
        }


        public void AddImage(Image image)
        {

            using var context = new ImageDBContext(_connectionString);
            context.Images.Add(image);
            context.SaveChanges();

        }

        public Image GetById(int id)
        {
            using var context = new ImageDBContext(_connectionString);
            return context.Images.FirstOrDefault(i => i.Id == id);
        }

        public void AddLikes(int id)
        {
            using var context = new ImageDBContext(_connectionString);
            Image image = context.Images.FirstOrDefault(i => i.Id == id);
            image.Likes++;
            context.SaveChanges();
        }

        public int GetLikes(int id)
        {
            using var context = new ImageDBContext(_connectionString);
            Image image = context.Images.FirstOrDefault(i => i.Id == id);
            return image.Likes;
        }
    }
}
