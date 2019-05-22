using System;

namespace Trivia
{
    public class DefaultDice : IDice
    {
        static Random random = new Random();
        public int Roll() => random.Next(1, 7);
    }
}
