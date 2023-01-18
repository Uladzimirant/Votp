namespace Votp.Utils
{
    public class Randomizer : Random
    {
        public static Randomizer Instance { get; } = new Randomizer();

        private Randomizer()
        {
        }

        private Randomizer(int Seed) : base(Seed)
        {
        }

        protected const string Connos = "BCDFGHGKLMNPQRSTVWXZ";
        protected const string Vowels = "AEIOUY";
        protected readonly int[] Sizes = { 3, 5, 7 }; 
        public string NextWord(int size = -1)
        {
            return StrSequence(size,
                i => i % 2 == 0 ? Connos[Next(Connos.Length)] : Vowels[Next(Vowels.Length)]);
        }
        protected const string AlphaNumbers = "QWERTYUIOPASDFGHJKLZXCVBNM1234567890";
        public string NextAlphaNum(int size = -1)
        {
            return StrSequence(size, i => AlphaNumbers[Next(AlphaNumbers.Length)]);
        }

        protected string StrSequence(int size, Func<int, char> gen)
        {
            return new string(GenerateSequence(size > 0 ? size : Sizes[Next(Sizes.Length)],
                gen).ToArray());
        }

        public IEnumerable<T> GenerateSequence<T>(int minValue, int maxValue, Func<int, T> generator) => GenerateSequence<T>(Next(minValue, maxValue), generator);
        public IEnumerable<T> GenerateSequence<T>(int size, Func<int, T> generator)
        {
            return Enumerable.Range(0, size).Select(generator);
        }
    }
}
