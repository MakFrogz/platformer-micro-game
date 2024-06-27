using UnityEngine.Audio;

namespace Audio
{
    public class AudioService : IAudioService
    {
        private AudioMixer _audioMixer;

        public AudioService(AudioMixer audioMixer)
        {
            _audioMixer = audioMixer;
        }
        
        public void Mute(string exposedParameter, bool mute)
        {
            float volume = mute ? -80 : 0;
            _audioMixer.SetFloat(exposedParameter, volume);
        }

        public void SetPitch(string exposedParameter, float pitch)
        {
            _audioMixer.SetFloat(exposedParameter, pitch);
        }
    }
}