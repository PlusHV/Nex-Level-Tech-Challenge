using UnityEngine;

public class LaserPointer : MonoBehaviour
{

    private Transform controller;
    private PlayerInteractor connectedInteractor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        controller = this.transform.parent.parent;
        connectedInteractor = this.transform.parent.GetComponent<PlayerInteractor>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (!connectedInteractor.CheckEitherLaserCooldown()){

            GetComponent<LineRenderer>().enabled = true;
            UpdatePointer();

        } else {
            GetComponent<LineRenderer>().enabled = false;
        }


    }


    private void UpdatePointer(){

        Vector3 direction = (this.transform.position - controller.position).normalized;

        LineRenderer lr = GetComponent<LineRenderer>();

        lr.SetPosition(0, controller.position);

        lr.SetPosition(1, controller.position + direction * 40.0f);
    }
}
