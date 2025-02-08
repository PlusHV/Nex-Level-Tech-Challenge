using UnityEngine;

public class Targetable : MonoBehaviour
{

    public Material unhit;
    public Material hit;

    private bool isHit;

    private Material[] targetableMaterials;

    public Door linkedDoor;
    public Targetable[] linkedTargets;
    public Targetable parentTarget;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //targetableMaterials = this.GetComponent<Renderer>().materials;


        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){

        if (other.tag != "PlayerProjectile"){
            return;
        }
        isHit = true;
        this.GetComponent<MeshRenderer>().material = hit;
        CheckDoorUnlock();

        
    }

    public void CheckDoorUnlock(){

        if (!isHit) return;

        if (null != linkedDoor){


            bool linkCheck = true;

            if (linkedTargets.Length == 0){
                linkedDoor.UnlockDoor();
                return;
            }

            //check if other linked targets on hit already
            foreach(Targetable linkedTarget in linkedTargets){

                if (!linkedTarget.IsHit()){
                    linkCheck = linkedTarget.IsHit();
                }

            }
            if (linkCheck){
                linkedDoor.UnlockDoor();
            }
        } else if (null != parentTarget){
            parentTarget.CheckDoorUnlock();
        }
    }

    private void Reset(){
        isHit = false;
        this.GetComponent<MeshRenderer>().material = unhit;
    }

    public bool IsHit(){
        return isHit;
    }
}
