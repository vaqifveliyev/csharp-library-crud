using KitabxanaAPP.Infastructure;

namespace KitabxanaAPP.DataModels
{
    [Serializable]
    public class Author : IEquatable<Author>, IEntity
    {
        static int counter = 0;

        public Author() 
        {
            counter++;
            this.Id = counter;
        }

        public int Id { get; private set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int BookId { get; set; }


        public bool Equals(Author? other)
        {
            if (other == null) return false;
            return this.Id == other.Id;
        }

        public override string ToString()
        {
            return $"{Id}. {Name} {Surname}";
        }

    }
}
