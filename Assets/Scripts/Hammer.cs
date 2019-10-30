using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    public static event System.Action HitBoss;
    public void PlaySmashAnim ()
    {

    }

    public void Impact ()
    {
        if (HitBoss != null)
        {
            HitBoss();
        }
    }
}
