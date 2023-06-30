namespace WebApi.Repository
{
    public class IntGenerator
    {
        public IntGenerator(int init = 0)
        {
            _value = init;
        }
        private int _value;

        public int Next => _value == int.MaxValue ?
            throw new IndexOutOfRangeException() : ++_value;

        public int Current => _value;

    }
}
