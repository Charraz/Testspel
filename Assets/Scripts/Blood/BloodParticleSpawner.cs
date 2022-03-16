using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodParticleSpawner : MonoBehaviour
{
    public ParticleSystem bloodSplatParticles;
    public GameObject bloodSplatPrefab;
    public Transform bloodSplatHolder;

    private void Update()
    {
        SpawnBloodEffect();
        Destroy(gameObject);
    }

    private void SpawnBloodEffect()
    {
        GameObject splat = Instantiate(bloodSplatPrefab, transform.position, Quaternion.identity) as GameObject;
        //splat.transform.SetParent(bloodSplatHolder, true);
        BloodSplat bloodSplatScript = splat.GetComponent<BloodSplat>();

        bloodSplatParticles.transform.position = transform.position;
        bloodSplatParticles.Play();
        bloodSplatScript.Initialize(BloodSplat.SplatLocation.Foreground);
    }
}
