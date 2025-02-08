using UnityEngine;

public class Door : MonoBehaviour {

    private float maxOpen = 1.5f;
    private float minOpen = 0.75f;

    private Transform leftDoor;
    private Transform rightDoor;
    
    private float openValue = 0.0f;
    private float currentTimer = 0.0f;
    const float doorTime = 1.0f;

    private bool locked = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leftDoor = this.transform.Find("Left");
        rightDoor = this.transform.Find("Right");
    }

    // Update is called once per frame
    void Update()
    {
     

        if (!locked && openValue < 1.0f){
            OpenDoor();
        }

    }



    void OpenDoor(){

        currentTimer+= Time.deltaTime;
        openValue = Mathf.Min(1.0f, currentTimer/doorTime);

        leftDoor.localPosition = new Vector3(0.0f, 0.0f, Mathf.Lerp(minOpen, maxOpen, openValue));
        rightDoor.localPosition = new Vector3(0.0f, 0.0f, Mathf.Lerp(-minOpen, -maxOpen, openValue));
        


    }

    public void UnlockDoor(){
        locked = false;
    }
}
