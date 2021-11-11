using UnityEngine;

public static class AnimatorEx
{
    public static void SetTriggerSafe( this Animator anim, int nameHash )
    {
        if( anim != null )
            if( anim.isInitialized && anim.isActiveAndEnabled )
                anim.SetTrigger( nameHash );
    }
}
