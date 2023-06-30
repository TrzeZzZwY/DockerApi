namespace WebApi.Models.Mapper
{
    public static class Mapper
    {
        public static Out Map<In, Out>(Func<In, Out> mapFunc, In input) where In : class where Out : class
        {
            return mapFunc(input);
        }
    }
}
