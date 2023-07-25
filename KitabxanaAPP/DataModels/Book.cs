using KitabxanaAPP.Infastructure;
using KitabxanaAPP.StableModels;

namespace KitabxanaAPP.DataModels
{
    [Serializable]
    public class Book : IEquatable<Book>, IEntity
    {
        static int counter = 0;

        public Book()
        {
            counter++;
            this.Id = counter;
        }


        public int Id { get; private set; }

        public string Name { get; set; }
        public int AuthorId { get; set; }
        public Genre Genre { get; set; }
        public decimal Price { get; set; }
        public int PageCount { get; set; }


        public bool Equals(Book? other)
        {
            if (other == null) return false;
            return this.Id == other.Id;
        }

        public override string ToString()
        {
            return $"ID: {Id}\nKitabın adı: {Name}\nJanr: {Genre}\nSəhifə sayı: {PageCount}\nQiyməti: {Price}₼\n===========================";
            
        }

    }
}
