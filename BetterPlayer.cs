namespace Trivia
{
    class BetterPlayer
    {
        public BetterPlayer(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new NotAValidPlayerNameException();
            }
            Name = name;
        }

        public string Name { get; }
    }
}
