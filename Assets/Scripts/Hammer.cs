using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    public static event System.Action HitBoss;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void PlaySmashAnim ()
    {
        animator.SetTrigger("HammerFall");
    }

    public void Impact ()
    {
        if (HitBoss != null)
        {
            HitBoss();
        }
    }
}
