using System;

namespace Assets.Scripts.Extencions
{
    public static class EnumExtencion
    {
        public static int ToInt<T>(this T convertingEnumElement) where T : struct, IConvertible
        {
            return Convert.ToInt32(convertingEnumElement);
        }
    }
}
