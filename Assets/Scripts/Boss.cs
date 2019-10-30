using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Hammer.HitBoss += () => Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Hammer.HitBoss -= () => Destroy(gameObject);
    }


}
