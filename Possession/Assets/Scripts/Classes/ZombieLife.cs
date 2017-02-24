using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieLife : MonoBehaviour {

    public float lifePoint = 10;

    public bool IncrementLife(float inc)
    {
        lifePoint += inc;
        if(lifePoint <= 0)
        {
            Destroy(this.gameObject);
            return false;
        }
        return true;
    }

    public bool IsAlive()
    {
        return (lifePoint <= 0);
    }
}
