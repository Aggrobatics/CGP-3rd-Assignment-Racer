using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GDLibrary
{
    public class AnimationController : Controller
    {
        //passing in the dictionary means all controllers can "feed off" the same XML data
        private Dictionary<string, AnimationData> dictionary;
        private TextureParameters textureParameters;
        private AnimationStatusType currentAnimationStatus;

        private int timeSinceLastFrameInMs, timeBetweenFrameInMs, currentFrame;
        private Rectangle StartSourceRectangle;
        private AnimationData currentAnimationData;
        private int currentFrameRate;
        private Transform2D transform;

        public AnimationController(IActor parentActor, ControllerType controllerType,
            bool bEnabled, Dictionary<string, AnimationData> animationDataDictionary,
            string name, bool bLoop, int frameRate)
            : base(parentActor, controllerType, bEnabled)
        {
            this.dictionary = animationDataDictionary;
            this.textureParameters = parentActor.GetTextureParameters();
            this.transform = parentActor.GetTransform();
            Play(name, bLoop, frameRate);
        }

        public void Play(string name, bool bLoop, int frameRate)
        {
            AnimationData newAnimationData = SetCurrentAnimation(name);


            if (this.currentAnimationData == null)
            {
                SetFrame(newAnimationData, bLoop, frameRate);
            }
            else
            {
                if (!newAnimationData.Equals(this.currentAnimationData))
                {
                    SetFrame(newAnimationData, bLoop, frameRate);
                }
                else
                {
                    if (this.currentAnimationStatus == AnimationStatusType.Stopped)
                    {
                        SetFrame(newAnimationData, bLoop, frameRate);
                    }
                }
            }
        }


        public AnimationData SetCurrentAnimation(string name)
        {
            if (this.dictionary.ContainsKey(name))
            {
                AnimationData currentAnimationData = this.dictionary[name];
                this.transform.Origin = currentAnimationData.origin;
                this.transform.OriginalBounds = new Rectangle(0, 0, currentAnimationData.sourceRectangle.Width, currentAnimationData.sourceRectangle.Height);
                this.textureParameters.SourceRectangle = currentAnimationData.sourceRectangle;
                this.StartSourceRectangle = this.textureParameters.SourceRectangle; //store to allow loop
                return currentAnimationData;
            }
            return null;
        }

        private void SetFrame(AnimationData newAnimationData, bool bLoop, int frameRate)
        {
            this.currentAnimationData = newAnimationData;
            this.currentFrame = 0;
            this.currentFrameRate = (frameRate > 0) ? frameRate : 1;
            this.timeBetweenFrameInMs = (int)(1000.0 / this.currentFrameRate);
            this.timeSinceLastFrameInMs = this.timeBetweenFrameInMs;
            this.currentAnimationStatus = bLoop ? AnimationStatusType.PlayingLooped : AnimationStatusType.PlayingOnce;
        }

        public override void Update(GameTime gameTime)
        {
            Animate(gameTime);
        }

        private void Animate(GameTime gameTime)
        {
            this.timeSinceLastFrameInMs += gameTime.ElapsedGameTime.Milliseconds;

            if (this.timeSinceLastFrameInMs > this.timeBetweenFrameInMs) //time to play the next frame?
            {
                //can we advance a frame without overrunning the end of the animation?
                if (this.currentFrame < this.currentAnimationData.frameCount)
                {
                    //set source rectangle to show the correct fram
                    this.textureParameters.SourceRectangle = MathUtility.AnimationAdd(this.StartSourceRectangle, currentFrame * this.currentAnimationData.sourceRectangle.Width, 0);
                    //reset the time until next frame
                    this.timeSinceLastFrameInMs = 0;
                    //increment current frame for the next time around this if()
                    this.currentFrame++;
                }
                //are we at end of animation?
                else
                {
                    //if animation is set to loop then reset all
                    if (this.currentAnimationStatus == AnimationStatusType.PlayingLooped) //if repeat, reset to start of animation
                    {
                        //reset to first frame
                        this.currentFrame = 0;
                        //means no reset delay in animation
                        this.timeSinceLastFrameInMs = this.timeBetweenFrameInMs;
                        //set source rectangle to show first frame
                        this.textureParameters.SourceRectangle = MathUtility.AnimationAdd(this.StartSourceRectangle, 0, 0);
                    }
                    else
                    {
                        //set to finished, which allows the controller to query status and remove the animation, if finished
                        this.currentAnimationStatus = AnimationStatusType.Stopped;
                    }
                }
            }
        }
    }
}
