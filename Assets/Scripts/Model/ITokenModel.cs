namespace Model
{
    public interface ITokenModel
    {
        int Tokens { get; }
        void SetTokens(int coins);
        void AddToken();
    }
}