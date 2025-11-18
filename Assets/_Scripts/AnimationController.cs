using UnityEngine;

public static class AnimationController
{
    // Individual type-specific methods - clean and simple
    public static void SetBool(Animator animator, string parameterName, bool value)
    {
        if (animator == null) return;
        animator.SetBool(parameterName, value);
    }

    public static void SetFloat(Animator animator, string parameterName, float value)
    {
        if (animator == null) return;
        animator.SetFloat(parameterName, value);
    }

    public static void SetInt(Animator animator, string parameterName, int value)
    {
        if (animator == null) return;
        animator.SetInteger(parameterName, value);
    }

    public static void SetTrigger(Animator animator, string parameterName)
    {
        if (animator == null) return;
        animator.SetTrigger(parameterName);
    }

    public static void ResetTrigger(Animator animator, string parameterName)
    {
        if (animator == null) return;
        animator.ResetTrigger(parameterName);
    }
}