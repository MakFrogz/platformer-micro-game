namespace SaveLoad
{
    public class AudioData
    {
        public bool SoundMute;
        public bool MusicMute;

        public AudioData(bool soundMute, bool musicMute)
        {
            SoundMute = soundMute;
            MusicMute = musicMute;
        }
    }
}