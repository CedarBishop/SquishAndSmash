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
        print("played anim");
        StartCoroutine("Impact");
    }

    public IEnumerator Impact ()
    {
        yield return new WaitForSeconds(1);
        if (HitBoss != null)
        {
            HitBoss();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            print("trigged0");
            PlaySmashAnim();
        }
    }
}
