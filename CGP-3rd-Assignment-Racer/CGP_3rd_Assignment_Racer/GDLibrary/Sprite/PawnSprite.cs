using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GDLibrary
{
    public class PawnSprite : Sprite, IActor
    {
        #region Fields
        private ControllerList controllerList;
        #endregion

        #region Properties
        #endregion

        public PawnSprite(string id, Transform2D transform, TextureParameters textureParameters, bool isCollidable, bool isVisible)
            : base(id, transform, textureParameters, isCollidable, isVisible)
        {
            this.controllerList = new ControllerList(id + " controller list");
        }

        public override void Update(GameTime gameTime)
        {
            if(this.controllerList != null)
                this.controllerList.Update(gameTime);

            //ensure we call Sprite::Update so that Transform::Update() is called
            base.Update(gameTime);
        }

        public TextureParameters GetTextureParameters()
        {
            return this.TextureParameters;
        }

        public Transform2D GetTransform()
        {
            return this.Transform;
        }

        public ControllerList GetControllerList()
        {
            return this.controllerList;
        }

        public int GetBSP()
        {
            return this.Transform.BSPSector;
        }

        public override void Reset()
        {
            //reset all controllers?

            base.Reset();
        }
        public new object Clone()
        {
            PawnSprite clone = new PawnSprite("clone - " + this.ID, (Transform2D)this.Transform.Clone(), (TextureParameters)this.TextureParameters.Clone(),
                this.IsCollidable, this.IsVisible);

            //update the controller list by setting the new parent actor on the cloned sprite
            clone.controllerList = this.controllerList.GetDeepCopyNewParent(clone);
            return clone;
        }
    }
}
