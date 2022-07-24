using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotateMin =-180, rotateMax=180;
    private float rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        //find a random value and put it as rotateSpeed
        rotateSpeed = Random.Range(rotateMin, rotateMax);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotateSpeed) * Time.deltaTime);
    }
}
