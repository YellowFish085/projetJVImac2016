using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieLife : MonoBehaviour {

    public float lifePoint = 10;

    private List<GameObject> assailants; // Use when the GM die

    void Awake()
    {
        assailants = new List<GameObject>();
    }

    public void IncrementLife(float inc)
    {
        Debug.Log("IncrementLife");
        lifePoint += inc;
        if(lifePoint <= 0)
            FinishHimself();
    }

    public bool IsAlive()
    {
        return (lifePoint > 0);
    }
    
    private void FinishHimself()
    {
        for(int i = assailants.Count - 1; i >= 0; --i)
        {
            ZombieCannibal zCannibal = assailants[i].GetComponent<ZombieCannibal>();
            if (!zCannibal) //Si cannibal
            {
                ZombieTargetBehaviour zTargetBehaviour = assailants[i].GetComponent<ZombieTargetBehaviour>();
                zTargetBehaviour.SetTarget(null);
            }
            else
                zCannibal.SetTarget(null);
        }
        Destroy(this.gameObject);
    }

    public void AddAssailant(GameObject assailant)
    {
        assailants.Add(assailant);
    }

    public void RemoveAssailent(GameObject assailant)
    {
        assailants.Remove(assailant);
    }
}
