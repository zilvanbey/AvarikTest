using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorScript : MonoBehaviour
{
    public float conveyorSpeed = 3f;
    public Vector3 direction;

    bool forceStop;
    bool inspection;
    public Vector3 inspectionDirection;

    public List<GameObject> objects;


    private void FixedUpdate()
    {
        if (inspection == false)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].GetComponent<Rigidbody>().AddForce(conveyorSpeed * direction);
            }
        }
        

        if (inspection)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].GetComponent<Rigidbody>().AddForce(conveyorSpeed * inspectionDirection);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        objects.Add(collision.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        objects.Remove(collision.gameObject);
    }

    public void ForceStop() //called from force stop button
    {
        if (inspection == false)
        {
            forceStop = !forceStop;
            for (int i = 0; i < objects.Count; i++)
            {
                if (forceStop)
                {
                    objects[i].GetComponent<Rigidbody>().drag = 100;
                }

                else if (forceStop == false)
                {
                    objects[i].GetComponent<Rigidbody>().drag = 0;

                }
            }
        }
    }

    public void Inspect() //called from inspection button
    {
        inspection = !inspection;

        if (inspection)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].GetComponent<Rigidbody>().drag = 100;



            }

            CancelInvoke("PutToSide");
            Invoke("PutToSide", 0.5f);
        }
    }

    void PutToSide()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            objects[i].GetComponent<Rigidbody>().drag = 0;

        }
    }
}
