using System.Reflection;

namespace K2P.Core.Reflection
{
    public static class NetObjectEx
    {
        public static bool InvokeMethodIfExists( this object @object, string name, BindingFlags flags, params object[] parameters )
        {
            var methodInfo = @object.GetType().GetMethod( name, flags );
            if( methodInfo == null )
                return false;

            methodInfo.Invoke( @object, parameters );
            return true;
        }

        public static (bool, T) InvokeMethodIfExists<T>( this object @object, string name, BindingFlags flags, params object[] parameters )
        {
            var methodInfo = @object.GetType().GetMethod( name, flags );
            if( methodInfo == null )
                return ( false, default );

            return ( true, (T)methodInfo.Invoke( @object, parameters ) );
        }
    }
}
