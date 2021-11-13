#define TEST_EVENT_CLASS
// #define TEST_EVENTS_ON_EXIT
//endif

using System.Collections.Generic;
using UnityEngine;

public class EventList
{
    private readonly List<Handler> tmpHandlers = new List<Handler>();

    private readonly List<Handler> handlers;
    private readonly bool callOnAdd;

    public int HandlerCount => handlers?.Count ?? 0;

    public EventList( bool _callOnAdd = false )
    {
        callOnAdd = _callOnAdd;
        handlers = new List<Handler>();

        #if TEST_EVENTS_ON_EXIT
        Exit.OnApplicationQuitEvent += OnExiting;
        #endif
    }

    public EventList( int capacity, bool _callOnAdd = false )
    {
        callOnAdd = _callOnAdd;
        handlers = new List<Handler>( capacity );

        #if TEST_EVENTS_ON_EXIT
        Exit.OnApplicationQuitEvent += OnExiting;
        #endif
    }

    public EventList( EventList other, bool _callOnAdd = false )
    {
        callOnAdd = _callOnAdd;

        if( other != null )
            handlers = new List<Handler>( other.handlers );
        else
            handlers = new List<Handler>();

        #if TEST_EVENTS_ON_EXIT
        Exit.OnApplicationQuitEvent += OnExiting;
        #endif
    }

    public void TryAddListener( Handler target )
    {
        if( !Contains( target ) )
            AddListener( target );
    }

    public void AddListener( Handler target )
    {
        if( target == null )
            return;

        #if TEST_EVENT_CLASS
        if( handlers.Contains( target ) )
            Debug.LogError( $"EventList [{this}] already contains handler: {target}" );
        #endif

        handlers.Add( target );

        if( callOnAdd )
            target.Invoke();
    }

    public void TryRemoveListener( Handler target )
    {
        if( Contains( target ) )
            RemoveListener( target );
    }

    public void RemoveListener( Handler target )
    {
        if( target == null )
            return;

        #if TEST_EVENT_CLASS
        if( !handlers.Contains( target ) )
            Debug.LogWarning( $"EventList [{this}] does not contain handler: {target}" );
        #endif

        handlers.Remove( target );
    }

    public void AddSilently( Handler target )
    {
        if( target != null )
        {
            #if TEST_EVENT_CLASS
            if( handlers.Contains( target ) )
                Debug.LogError( $"EventList [{this}] already contains handler: {target}" );
            #endif

            handlers.Add( target );
        }
    }

    public bool Contains( Handler handler )
    {
        return handlers.Contains( handler );
    }

    public void Invoke()
    {
        tmpHandlers.Clear();
        tmpHandlers.AddRange( handlers );
        for( int i = 0; i < tmpHandlers.Count; i++ )
            tmpHandlers[i].Invoke();
        tmpHandlers.Clear();
    }

    public void ClearHandlers()
    {
        handlers.Clear();
    }

    #if TEST_EVENTS_ON_EXIT
    private static StringBuilder sb;

    private void OnExiting()
    {
        if( handlers.Count > 0 )
            foreach( var h in handlers )
                Debug.Log( $"[{h?.Target?.GetType().Name}] {h.Target.ToStringSafe()}.{h.Method.Name}" );
    }
    #endif

    public delegate void Handler();
}

public class EventList<TArg>
{
    private readonly List<Handler> tmpHandlers = new List<Handler>();

    private event GetValueHandler GetValue;
    private readonly List<Handler> handlers;
    private readonly bool callOnAdd;

    public int HandlerCount => handlers?.Count ?? 0;

    public EventList( GetValueHandler valueGetter, bool _callOnAdd = false )
    {
        GetValue = valueGetter;
        callOnAdd = _callOnAdd;
        handlers = new List<Handler>();

        #if TEST_EVENTS_ON_EXIT
        Exit.OnApplicationQuitEvent += OnExiting;
        #endif
    }

    public EventList( GetValueHandler valueGetter, int capacity, bool _callOnAdd = false )
    {
        GetValue = valueGetter;
        callOnAdd = _callOnAdd;
        handlers = new List<Handler>( capacity );

        #if TEST_EVENTS_ON_EXIT
        Exit.OnApplicationQuitEvent += OnExiting;
        #endif
    }

    public EventList( GetValueHandler valueGetter, EventList<TArg> other, bool _callOnAdd = false )
    {
        GetValue = valueGetter;
        callOnAdd = _callOnAdd;
        handlers = new List<Handler>( other.handlers );

        #if TEST_EVENTS_ON_EXIT
        Exit.OnApplicationQuitEvent += OnExiting;
        #endif
    }

    public void AddListener( Handler target )
    {
        if( target != null )
        {
            #if TEST_EVENT_CLASS
            if( handlers.Contains( target ) )
                Debug.LogError( $"EventList<{typeof( TArg ).Name}> [{this}] already contains handler: {target}" );
            #endif

            handlers.Add( target );

            if( callOnAdd )
                target.Invoke( GetValue() );
        }
    }

    public void TryRemoveListener( Handler target )
    {
        if( Contains( target ) )
            RemoveListener( target );
    }

    public void RemoveListener( Handler target )
    {
        if( target != null )
        {
            #if TEST_EVENT_CLASS
            if( !handlers.Contains( target ) )
                Debug.LogWarning( $"EventList<{typeof( TArg ).Name}> [{this}] does not contain handler: {target}" );
            #endif
            // remove returns value indicating if value was removed from list
            // so there is no need to check if it contains that value beforehand
            handlers.Remove( target );
        }
    }

    public void AddSilently( Handler target )
    {
        if( target != null )
        {
            #if TEST_EVENT_CLASS
            if( handlers.Contains( target ) )
                Debug.LogError( $"EventList<{typeof( TArg ).Name}> [{this}] already contains handler: {target}" );
            #endif

            handlers.Add( target );
        }
    }

    public bool Contains( Handler handler )
    {
        return handlers.Contains( handler );
    }

    public void Invoke( TArg arg )
    {
        tmpHandlers.Clear();
        tmpHandlers.AddRange( handlers );
        for( int i = 0; i < tmpHandlers.Count; i++ )
            tmpHandlers[i].Invoke( arg );
        tmpHandlers.Clear();
    }

    public void ClearHandlers()
    {
        handlers.Clear();
    }

    #if TEST_EVENTS_ON_EXIT
    private void OnExiting()
    {
        if( handlers.Count > 0 )
            foreach( var h in handlers )
                Debug.Log( $"[{h?.Target?.GetType().Name}] {h.Target.ToStringSafe()}.{h.Method.Name}" );
    }
    #endif

    public delegate void Handler( TArg arg );

    public delegate TArg GetValueHandler();
}
