using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GDLibrary;
using Microsoft.Xna.Framework;
using GDGame;

namespace GDLibrary
{
    public class CameraManager : GameComponent
    {
        private List<Camera2D> list;

        public Camera2D this[int index]  //see SpriteManager for loop
        {
            get
            {
                return this.list[index];
            }
        }

        public int Size() //see SpriteManager for loop
        {
            return this.list.Count;
        }

        public CameraManager(Main game)
            : base(game)
        {
            this.list = new List<Camera2D>();
        }
        //remove, removeby, toString
        public void Add(Camera2D c)
        {
            //Remember to override equals and gethashcode in Camera2D
            if (!this.list.Contains(c))
                this.list.Add(c);
        }

        public void AddAll(List<Camera2D> addList)
        {
            foreach (Camera2D c in addList)
            {
                if (!this.list.Contains(c))
                    this.list.Add(c);
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Camera2D c in this.list)
            {
                c.Update(gameTime);
            }

            base.Update(gameTime);
        }

        public void Clear()
        {
            this.list.Clear();
        }
    }
}
