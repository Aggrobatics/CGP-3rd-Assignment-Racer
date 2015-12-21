using Microsoft.Xna.Framework;
namespace GDLibrary
{
    public class Node 
    {
        private string id;
        private Vector2 position;     

        #region PROPERTIES
        public string ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }
        #endregion

        public Node(string id, Vector2 position)
        {
            this.id = id; 
            this.position = position;
        }
    }
}
