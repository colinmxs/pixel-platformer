namespace PixelPlatformer
{
    using UnityEngine;

    public class Screamer : MonoBehaviour
    {
        public AudioClip EndSong;
        public AudioClip ZombieClip;
        public AudioClip[] Clips;
        public AudioClip[] JumpClips;
        public AudioClip DeathClip;
        public AudioSource Source;

        public void Jump()
        {
            var index = Random.Range(0, JumpClips.Length);
            var clip = JumpClips[index];
            Source.clip = clip;
            Source.volume = 0.5f;
            Source.Play();
        }

        public void PlayZombie()
        {
            if (!Source.isPlaying)
            {
                Source.clip = ZombieClip;
                Source.Play();
            }
        }

        internal void PlayEndSong()
        {
            Source.clip = EndSong;
            Source.Play();
        }

        public void Scream()
        {
            Source.volume = 1f;
            var index = Random.Range(0, Clips.Length);
            var clip = Clips[index];
            Source.clip = clip;
            Source.Play();
        }

        public void Death()
        {
            Source.volume = 1f;
            Source.clip = DeathClip;
            Source.Play();
        }
    }
}