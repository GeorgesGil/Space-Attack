using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bullet;
    public float fireRate;
    public Transform[] shotSpawn;
    // Start is called before the first frame update
    void Start()
    {
        //Invokes the method methodName(Fire) in time seconds, then repeatedly every repeatRate seconds.
        InvokeRepeating("Fire", fireRate, fireRate);
    }

    void Fire()
    {
        //loop through our shotspawn points and if its less than our max shptspawnpoint then call the instantiate
        for(int i=0; i<shotSpawn.Length;i++)
        {
            Instantiate(bullet, shotSpawn[i].position, shotSpawn[i].rotation);
        }
    }
}
