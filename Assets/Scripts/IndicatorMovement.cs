using UnityEngine;

public class IndicatorMovement : MonoBehaviour
{

    public string attackType; //cross or directional
    public string variation; //In this case, the first attack

    private Vector3 destination;
    private Vector3 originalVector;

    private bool moving = false;

    private float time = 0.0f;
    private float speed = 0.0f;
    private float maxTime = 1.0f;
    private float lingerTime = 2.0f; //once animation finishes, time for indicator to stay
    private float holdTime = 1.0f; //once indicator is loaded in, hold the animation until this time.

    private Transform sphere;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //StartAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (moving){
            time+= Time.deltaTime;
            if (time >= holdTime){
                if (attackType == "cross"){

                    float newZ = Mathf.LerpAngle(originalVector.z, destination.z, (time - holdTime)/maxTime);
                    this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, newZ);

                } else if (attackType == "directional"){


                    Vector3 newPos = Vector3.Lerp(originalVector, destination, (time - holdTime)/maxTime);
                    sphere.localPosition = newPos;

                }

                if (time >= maxTime + holdTime){
                    moving = false;
                    Destroy(this.transform.gameObject, lingerTime);
                }

            }

        } 


    }

    public void StartAnimation(){
        if (attackType == "cross"){
            if (variation == "cardinal"){
                this.transform.rotation  = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                originalVector = new Vector3(0.0f, 0.0f, 0.0f);
                destination = new Vector3(0.0f, 0.0f, 45.0f);
            } else if (variation == "intercardinal"){
                this.transform.rotation  = Quaternion.Euler(0.0f, 0.0f, 45.0f);
                originalVector = new Vector3(0.0f, 0.0f, 45.0f);
                destination = new Vector3(0.0f, 0.0f, 0.0f);
            }

            time = 0.0f;
            moving = true;

        } else if (attackType == "directional"){

            sphere = this.transform.Find("Sphere");
            if (variation == "left"){
                sphere.localPosition = new Vector3(0.25f, 0.0f, 0.0f);
                originalVector = new Vector3(0.25f, 0.0f, 0.0f);
                destination = new Vector3(-0.25f, 0.0f, 0.0f);
            } else if (variation == "right"){
                sphere.localPosition = new Vector3(-0.25f, 0.0f, 0.0f);
                originalVector = new Vector3(-0.25f, 0.0f, 0.0f);
                destination = new Vector3(0.25f, 0.0f, 0.0f);
            }

            time = 0.0f;
            moving = true;
        }

    }

    public void Variation(string val){
        variation = val;
    }

    public void InitializeIndicator(Attack attack){
        variation = attack.Variation;
        attackType = attack.AttackType;
        StartAnimation();
    }
}
