using System.Collections.Generic;
using GDGame;

namespace GDLibrary
{
    public class AssetDictionary<V>
    {
        private Main game;
        private string id;
        private Dictionary<string, V> dictionary;

        public V this[string key]
        {
            get
            {
                key = key.ToLower();
                return this.dictionary[key];
            }
        }

        public AssetDictionary(Main game, string id)
        {
            this.game = game;
            this.id = id;
            this.dictionary = new Dictionary<string, V>();
        }
        public void Add(string key, string path)
        {
            key = key.ToLower();

            //duplicates? Equals() and GetHashCode()
            if (!this.dictionary.ContainsKey(key))
            {
                V value = this.game.Content.Load<V>(path);
                this.dictionary.Add(key, value);
            }
        }

        public void Add(string path)
        {
            string key = StringUtility.ParseNameFromPath(path).ToLower();
            //duplicates? Equals() and GetHashCode()
            if (!this.dictionary.ContainsKey(key))
            {
                V value = this.game.Content.Load<V>(path);
                this.dictionary.Add(key, value);
            }
        }
        public bool Remove(string key)
        {
            key = key.ToLower();
            //unloading asset from RAM???
            return this.dictionary.Remove(key);
        }
        public V Search(string key)
        {
            key = key.ToLower();
            return this.dictionary[key];
        }

        //to do - load/save from file, debug print, size
    }
}
