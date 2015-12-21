using Microsoft.Xna.Framework.Audio;

using GDGame;

namespace GDLibrary
{
    /// <summary>
    /// SoundEffectInfo stores information on each sound effect used within the game
    /// </summary>
    public class SoundEffectInfo
    {
        protected string effectName;
        protected string effectFileName;
        protected SoundEffectInstance soundEffectInstance;
        protected SoundEffect soundEffect;
        protected float volume, pitch, pan;
        protected bool loop;

        #region PROPERTIES
        public string Name
        {
            get
            {
                return effectName;
            }
            set
            {
                effectName = value;
            }
        }
        public float Volume
        {
            get
            {
                return volume;
            }
            set
            {
                volume = value;
            }
        }
        public float Pitch
        {
            get
            {
                return pitch;
            }
            set
            {
                pitch = value;
            }
        }
        public float Pan
        {
            get
            {
                return pan;
            }
            set
            {
                pan = value;
            }
        }
        public bool Loop
        {
            get
            {
                return loop;
            }
            set
            {
                loop = value;
            }
        }
        public SoundEffectInstance EffectInstance
        {
            get
            {
                return soundEffectInstance;
            }
        }
        public SoundEffect SoundEffect
        {
            get
            {
                return soundEffect;
            }
        }
        #endregion

        public SoundEffectInfo(Main game, string effectName,
            string effectFileName, float volume, float pitch, float pan, bool loop)
        {
            set(game, effectName, effectFileName, volume, pitch, pan, loop);
        }

        public void set(Main game, string effectName,
            string effectFileName, float volume, float pitch, float pan, bool loop)
        {
            this.soundEffect = game.Content.Load<SoundEffect>(@"" + effectFileName);
            this.effectFileName = effectFileName;
            this.effectName = effectName;
            this.volume = volume; 
            this.pitch = pitch; 
            this.pan = pan;
            this.loop = loop;
        }
    }

}
