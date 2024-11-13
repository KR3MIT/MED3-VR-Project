using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShooter : MonoBehaviour
{
    public Material material;
    LaserBeam laserBeam;

    public Material yello;

    // Update is called once per frame
    void Update()
    {
        Destroy(GameObject.Find("Laser"));
        laserBeam = new LaserBeam(gameObject.transform.position, gameObject.transform.right, material, yello);
    }
}
