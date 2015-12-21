using System.Collections.Generic;
using GDGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace GDLibrary
{
    public class SoundEffectInstanceAndName
    {
        public SoundEffectInstance SoundEffectInstance {get; set;}
        public string Name { get; set; }

        public SoundEffectInstanceAndName(string name, SoundEffectInstance soundEffectInstance)
        {
            this.Name = name;
            this.SoundEffectInstance = soundEffectInstance;
        }
    }


    public class SoundManager : GameComponent
    {
        private Dictionary<string, SoundEffectInfo> soundEffectDictonary;
        private List<SoundEffectInstanceAndName> playingList;

        public SoundManager(Main game)
            : base(game)
        {
            this.soundEffectDictonary = new Dictionary<string, SoundEffectInfo>();
            this.playingList = new List<SoundEffectInstanceAndName>();
        }

        public override void Update(GameTime gameTime)
        {
            CullFinishedSounds();

            base.Update(gameTime);
        }

        SoundEffectInstance effectInstance;
        private void CullFinishedSounds()
        {
            for (int i = 0; i < this.playingList.Count; i++)
            {
                effectInstance = this.playingList[i].SoundEffectInstance;

                //if the sound is not playing any more then remove it
                if (effectInstance.State == SoundState.Stopped)
                    this.playingList.RemoveAt(i);
            }
        }

        public SoundEffectInstanceAndName Find(string name)
        {
            foreach (SoundEffectInstanceAndName soundEffectInstAndName in this.playingList)
            {
                if(soundEffectInstAndName.Name.Equals(name))
                    return soundEffectInstAndName;
            }

            return null;
        }

        #region Play, Volume & Sound Methods
        public void Play(string name)
        {
            SoundEffectInstanceAndName soundEffectInstanceAndName = Find(name);

            if (Find(name) == null)  //not currently in the playlist, so play it
            {
                SoundEffectInstance effectInstance = GetEffectInstance(name);
                effectInstance.Play();
                this.playingList.Add(new SoundEffectInstanceAndName(name, effectInstance));
            }
            else //in the list and paused, then play it
            {
                if(soundEffectInstanceAndName.SoundEffectInstance.State == SoundState.Paused)
                    soundEffectInstanceAndName.SoundEffectInstance.Play();
            }
        }

        public void Pause(string name)
        {
            SoundEffectInstanceAndName soundEffectInstanceAndName = Find(name);

            if ((soundEffectInstanceAndName != null) 
                    && (soundEffectInstanceAndName.SoundEffectInstance.State == SoundState.Playing))
            {
                soundEffectInstanceAndName.SoundEffectInstance.Pause();
            }
        }

        public void Stop(string name)
        {
            SoundEffectInstanceAndName soundEffectInstanceAndName = Find(name);

            if (soundEffectInstanceAndName != null)
            {
                soundEffectInstanceAndName.SoundEffectInstance.Stop();
                this.playingList.Remove(soundEffectInstanceAndName);
            }
        }

        public void Resume(string name)
        {
            SoundEffectInstanceAndName soundEffectInstanceAndName = Find(name);

            if ((soundEffectInstanceAndName != null)
                    && (soundEffectInstanceAndName.SoundEffectInstance.State == SoundState.Paused))
            {
                soundEffectInstanceAndName.SoundEffectInstance.Resume();
            }
        }

        //used to mute all sounds
        public void SetMasterVolumeTo(float masterVolume)
        {
            SoundEffect.MasterVolume = (masterVolume >= 0 && masterVolume <= 1) ? masterVolume : 0.5f;
        }

        //changes the volume on a currently playing sound 
        public void ChangeVolumeBy(string name, float deltaVolume)
        {
            SoundEffectInstanceAndName soundEffectInstanceAndName = Find(name);
            if ((soundEffectInstanceAndName != null)
                    && (soundEffectInstanceAndName.SoundEffectInstance.State == SoundState.Playing))
            {
                float newVolume = soundEffectInstanceAndName.SoundEffectInstance.Volume + deltaVolume;

                if (newVolume >= 0 && newVolume <= 1)
                    soundEffectInstanceAndName.SoundEffectInstance.Volume = newVolume;
            }
        }

        //set the volume on a currently playing sound to a specific value
        public void SetVolumeTo(string name, float volume)
        {
             SoundEffectInstanceAndName soundEffectInstanceAndName = Find(name);
             if ((soundEffectInstanceAndName != null)
                     && (soundEffectInstanceAndName.SoundEffectInstance.State == SoundState.Playing))
             {
                 if (volume >= 0 && volume <= 1)
                     soundEffectInstanceAndName.SoundEffectInstance.Volume = volume;
             }
        }
        #endregion

        #region Dictionary Management Methods
        public bool Add(SoundEffectInfo effectInfo)
        {
            if (!soundEffectDictonary.ContainsKey(effectInfo.Name))
            {
                soundEffectDictonary.Add(effectInfo.Name, effectInfo);
                return true;
            }

            return false;
        }

        public bool Remove(string name)
        {
            //find effect info so we can nullify for garbage collection
            SoundEffectInfo effectInfo = soundEffectDictonary[name];
            //remove the effect info from dictionary and store return value
            bool wasRemoved = soundEffectDictonary.Remove(name);
            //nullify for garbage collection
            effectInfo = null; 
            return wasRemoved;
        }

        public void Clear()
        {
            soundEffectDictonary.Clear();
        }

        public int Size()
        {
            return soundEffectDictonary.Count;
        }

        public SoundEffectInstance GetEffectInstance(string name)
        {
            SoundEffectInfo effectInfo = soundEffectDictonary[name];
            SoundEffectInstance soundEffectInstance = effectInfo.SoundEffect.CreateInstance();
            soundEffectInstance.Volume = effectInfo.Volume;
            soundEffectInstance.Pitch = effectInfo.Pitch;
            soundEffectInstance.Pan = effectInfo.Pan;
            soundEffectInstance.IsLooped = effectInfo.Loop;
            return soundEffectInstance;
        }
        #endregion

    }
}
