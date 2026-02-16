using System;

namespace Mapster.Utils
{
    internal static class DelegateInvokeCompat
    {
        public static object? InvokeMap(Delegate del, object source, bool canUseDynamic)
        {
#if NET8_0_OR_GREATER
            if (canUseDynamic)
            {
                dynamic fn = del;
                return fn((dynamic)source);
            }
#endif
            return del.DynamicInvoke(source);
        }

        public static object? InvokeMapToTarget(Delegate del, object source, object destination, bool canUseDynamic)
        {
#if NET8_0_OR_GREATER
            if (canUseDynamic)
            {
                dynamic fn = del;
                return fn((dynamic)source, (dynamic)destination);
            }
#endif
            return del.DynamicInvoke(source, destination);
        }

        public static TDestination InvokeMapToTarget<TDestination>(
            Delegate del,
            object source,
            object destination,
            bool canUseDynamic)
        {
#if NET8_0_OR_GREATER
            if (canUseDynamic)
            {
                dynamic fn = del;
                return (TDestination)fn((dynamic)source, (dynamic)destination);
            }
#endif
            return (TDestination)del.DynamicInvoke(source, destination);
        }
    }
}
