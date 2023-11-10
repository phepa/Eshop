namespace Eshop.Shared.Helpers
{
    public static class ObjectHelper
    {
        public static bool IsNull(this object obj)
        {
            return obj is null;
        }

        public static bool NotNull(this object obj)
        {
            return !obj.IsNull();
        }
    }
}
