using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VRTemplate;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LaserBeam //: MonoBehaviour
// this script was inspired by Doc: https://www.youtube.com/watch?v=pNE3rfMGEAw
{
    Vector3 pos, dir;
    private float maxDistance = 100f;
    //everything except layer 6 which is object
    LayerMask rayLayers = ~(1 << 6);
    static bool first,first1 = false;
    GameObject Door;
    GameObject laserPointer;
    LineRenderer laserLine;
    List<Vector3> laserIndices = new List<Vector3>();
    Material decalYallow;
    // Start is called before the first frame update

    public LaserBeam(Vector3 pos, Vector3 dir, Material material,Material decalYallow)
    {
        this.laserLine = new LineRenderer();
        this.laserPointer = new GameObject();
        this.laserPointer.name = "Laser";
        this.pos = pos;
        this.dir = dir;
        
        this.laserLine = this.laserPointer.AddComponent(typeof(LineRenderer)) as LineRenderer;
        this.laserLine.startWidth = 0.1f;
        this.laserLine.endWidth = 0.1f;
        this.laserLine.material = material;
        this.laserLine.startColor = Color.red;
        this.laserLine.endColor = Color.red;

        this.decalYallow = decalYallow;

        CastLaser(pos, dir, laserLine);
        this.decalYallow = decalYallow;
    }

    
    void CastLaser(Vector3 pos, Vector3 dir, LineRenderer laserLine)
    {
        laserIndices.Add(pos);

        Ray ray = new Ray(pos, dir);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 70, rayLayers))
        {
            Checkhit(hit, dir, laserLine);
        }
        else
        {
            laserIndices.Add(ray.GetPoint(maxDistance));
            UpdateLaser();
        }

    }

    void UpdateLaser()
    {
        int count = 0;
        laserLine.positionCount = laserIndices.Count;
        foreach (Vector3 idx in laserIndices)
        {
            laserLine.SetPosition(count, idx);
            count++;
        }

    }
    void Checkhit(RaycastHit hitInfo, Vector3 direction, LineRenderer laserLine)
    {
        if (hitInfo.collider.gameObject.tag == "Mirror")
        {
            Vector3 pos = hitInfo.point;
            Vector3 dir = Vector3.Reflect(direction, hitInfo.normal);
            CastLaser(pos, dir, laserLine);
        }
        else
        {
            laserIndices.Add(hitInfo.point);
            UpdateLaser();
        }
        // if the laser hits the button, activate the barrier and change decal material
        if (hitInfo.collider.gameObject.tag == "YellowButton" && !first1)
        {
            GameObject yellowCube = GameObject.Find("YellowBarrier");
            YellowBarrier yellowBarrier = yellowCube.GetComponent<YellowBarrier>();
            yellowBarrier.BarrierActive();
            first1 = true;

             foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Decal"))
             {
                obj.GetComponent<DecalProjector>().material = decalYallow;
             }   
          

        }
        if (hitInfo.collider.gameObject.tag == "GreenButton" && !first)
        {
            //insert win condition here
            Debug.Log("You win!");
            // play particle effect

            GameObject taskCondition = GameObject.Find("LevelManager");
            TaskCondition task = taskCondition.GetComponent<TaskCondition>();
            task.TaskCompleted();
         
            Door = GameObject.Find("Door with decals");
            Door.GetComponent<Animator>().Play("Door Opening");
            first = true;

        }
    }
}
