using System;

namespace Additional.Utils
{
    public static class ThrowUtils
    {
        public static void ManyInstancesOfSingleton()
            => throw new InvalidOperationException("Many instances of singleton mono behaviour");

        public static void StateDuplicated()
            => throw new InvalidOperationException("Can't add 2 similar states in state machine");
    }
}