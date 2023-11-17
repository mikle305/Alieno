using System;

namespace Additional.Utils
{
    public static class ThrowUtils
    {
        public static void ManyInstancesOfSingleton(string objectName)
            => throw new InvalidOperationException($"Many instances of singleton mono behaviour ({objectName}");

        public static void StateDuplicated()
            => throw new InvalidOperationException("Can't add 2 similar states in state machine");
        
        public static void ValueLessThanZero() 
            => throw new ArgumentException("Value must be not not less than zero");

        public static void ComponentAlreadyAdded()
            => throw new InvalidOperationException("Component is already added, can't add same components to entity");
    }
}