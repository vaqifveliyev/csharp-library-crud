using KitabxanaAPP.DataModels;

namespace KitabxanaAPP.Infastructure
{
    [Serializable]
    public class Database
    {
        public GenericStore<Author> author { get; set; }
        public GenericStore<Book> book { get; set; }
    }
}
