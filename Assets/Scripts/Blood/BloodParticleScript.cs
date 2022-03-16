using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodParticleScript : MonoBehaviour
{
    public ParticleSystem bloodSplatParticels;
    public GameObject bloodSplatPrefab;
    public Transform bloodSplatHolder;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();


    private void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(bloodSplatParticels, other, collisionEvents);

        int count = collisionEvents.Count;

        for (int i = 0; i < count; i++)
        {
            GameObject splat = Instantiate(bloodSplatPrefab, collisionEvents[i].intersection, Quaternion.identity) as GameObject;
            //splat.transform.SetParent(bloodSplatHolder, true);
            BloodSplat splatScript = splat.GetComponent<BloodSplat>();
            splatScript.Initialize(BloodSplat.SplatLocation.Foreground);
        }
    }
}
