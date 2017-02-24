using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CircularBuffer;


public class ZombieSelector
{
    private const int BUFFER_SIZE = 20;
    private CircularBuffer<GameObject> buffer;
    private Vector3 scientistPosition;
    private float maxSwappingDistance;
    private GameObject selected;

    public ZombieSelector(Vector3 scientistPosition, float maxSwappingDistance)
    {
        this.buffer = new CircularBuffer<GameObject>(BUFFER_SIZE);

        this.scientistPosition = scientistPosition;
        this.maxSwappingDistance = maxSwappingDistance;

        var zombies = GetZombiesAround();
        foreach(GameObject z in zombies)
        {
            if(!z.HasTag("Cannibal"))
                buffer.PushBack(z);
        }
    }

    public void Update()
    {
        var zombiesToAdd = GetZombiesAround().Except(buffer);
        foreach (GameObject z in zombiesToAdd)
        {
            buffer.PushBack(z);
        }
    }

    public GameObject Next()
    {
        if (buffer.IsEmpty)
            return null;

        GameObject zombie = null;
        while(zombie == null)
        {
            GameObject tmp = buffer.Front();
            buffer.PopFront();
            if (InDistance(tmp))
                zombie = tmp;
        }
        buffer.PushBack(zombie);
        return zombie;

    }

    private IEnumerable<GameObject> GetZombiesAround()
    {
        return GameObject.FindGameObjectsWithTag("Zombie").Where(InDistance);
    }

    private bool InDistance(GameObject zombie)
    {
        return Vector3.Distance(scientistPosition, zombie.transform.position) < maxSwappingDistance;
    }

    private bool NotInDistance(GameObject zombie)
    {
        return !InDistance(zombie);
    }
}

