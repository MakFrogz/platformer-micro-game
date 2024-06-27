namespace Audio
{
    public interface IAudioService
    {
        void Mute(string exposedParameter, bool mute);
        void SetPitch(string exposedParameter, float pitch);
    }
}