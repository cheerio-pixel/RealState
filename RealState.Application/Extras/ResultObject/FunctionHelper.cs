namespace RealState.Application.Extras.ResultObject
{
    public static class FunctionHelper
    {
        public static Func<A, Func<B, R>> Curry<A, B, R>(this Func<A, B, R> func)
        => a => b => func(a, b);
        public static Func<A, Func<B, Func<C, R>>> Curry<A, B, C, R>(this Func<A, B, C, R> func)
        => a => b => c => func(a, b, c);

        public static Result<R> Map<A, R>(this Func<A, R> f, Result<A> resultA)
        => resultA.Map(f);

        public static Result<R> Lift<A, B, R>(this Func<A, B, R> self,
                                              Result<A> resultA,
                                              Result<B> resultB)
        => self.Curry()
               .Map(resultA)
               .Apply(resultB);

        public static Result<R> Lift<A, B, C, R>(this Func<A, B, C, R> self,
                                                 Result<A> resultA,
                                                 Result<B> resultB,
                                                 Result<C> resultC)
        => self.Curry()
               .Map(resultA)
               .Apply(resultB)
               .Apply(resultC);

        public static T Id<T>(T t)
        => t;

        public static Func<A, T> Const<A, T>(T t)
        => a => t;
    }
}