using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GDLibrary
{
    public class GenericDictionary<K, V>
    {
        private string id;
        private Dictionary<K, V> dictionary;

        public V this[K id]
        {
            get
            {
                return this.dictionary[id];
            }
        }

        public GenericDictionary(string id)
        {
            this.id = id;
            this.dictionary = new Dictionary<K, V>();
        }
        public void Add(K key, V value)
        {
            //duplicates? Equals() and GetHashCode()
            if (!this.dictionary.ContainsKey(key))
                this.dictionary.Add(key, value);
        }
        public bool Remove(K key)
        {
            return this.dictionary.Remove(key);
        }
        public V Search(K key)
        {
            return this.dictionary[key];
        }

        //to do - load/save from file, debug print, size
    }
}
