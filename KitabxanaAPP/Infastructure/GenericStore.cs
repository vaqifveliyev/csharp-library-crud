using KitabxanaAPP.DataModels;
using System.Collections;

namespace KitabxanaAPP.Infastructure
{
    [Serializable]
    public class GenericStore<T> : IEnumerable<T>
        where T: class, IEquatable<T>, IEntity
    {
        T[] data = new T[0];
        

        public void Add(T item)
        {
            int len = data.Length;
            Array.Resize(ref data, len+1);
            data[len] = item;
        }

        public T GetById(int id)
        {
            var found = Array.Find(data, item  => item.Id == id);
            return found;
        }


        public T[] GetAll(Predicate<T>match)
        {
            var foundAll = Array.FindAll(data, match);
            return foundAll;
        }

        public void Remove(T item)
        {
            int index = Array.IndexOf(data, item);
            if (index != -1)
            {
                for (int i = index; i < data.Length - 1; i++)
                {
                    data[i] = data[i + 1];
                }
                Array.Resize(ref data, data.Length - 1);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in data)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public T this[int index]
        {
            get
            {
                return data[index];
            }
        }

        public int Count
        {
            get
            {
                return data.Length;
            }
        }

    }
}
