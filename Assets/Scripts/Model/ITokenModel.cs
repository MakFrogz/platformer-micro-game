namespace Model
{
    public interface ITokenModel
    {
        public int Tokens { get; }
        public void SetTokens(int coins);
        public void AddToken();
    }
}