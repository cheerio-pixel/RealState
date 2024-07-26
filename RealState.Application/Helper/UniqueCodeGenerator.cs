

namespace RealState.Application.Helper
{
    public static class UniqueCodeGenerator
    {
        public static string UniqueCode()
        {
            return new Random().Next(100000, 999999).ToString();
        }
    }
}
