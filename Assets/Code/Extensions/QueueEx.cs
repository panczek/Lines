namespace System.Collections.Generic
{
    public static class QueueEx
    {
        public static bool IsNullOrEmpty<T>( this Queue<T> queue )
        {
            if( queue != null && queue.Count > 0 )
                return false;

            return true;
        }
    }
}