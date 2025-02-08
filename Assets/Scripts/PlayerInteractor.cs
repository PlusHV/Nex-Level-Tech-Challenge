using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;
using UnityEngine.UI;
public class PlayerInteractor : MonoBehaviour
{


    [SerializeField] 
    XRInputButtonReader m_UIPressInput = new XRInputButtonReader("Attack");

    const float laserCooldown = 2.0f;

    private float laserCooldownValue = 0.0f;

    private bool laserCooldownActive = false;

    private Transform laserOrigin;

    public GameObject laserPrefab;

    private Transform laserObject;


    private bool laserEndSet = true;

    private Vector3 laserEndLocation;

    public Player player;
    public Slider laserCDSlider;

    public PlayerInteractor otherInteractor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        laserOrigin = this.transform.Find("LaserOrigin");
    }

    // Update is called once per frame
    void Update()
    {

        if (!laserEndSet){
            laserObject.position = laserEndLocation;
            laserEndSet = true;
            laserObject = null;
        }

        if (laserCooldownActive) {
            laserCooldownValue+= Time.deltaTime;


            if (laserCooldownValue >= laserCooldown){
                laserCooldownActive = false;
                laserCooldownValue = 1.0f;
            }
            ///laserCooldownValue = Mathf.Min(laserCooldown, laserCooldownValue); 
        } else if (m_UIPressInput.ReadValue() > 0 && !laserCooldownActive && !otherInteractor.LaserCooldownActive()){ 

            //shoot laser
            ShootLaser();
            laserCooldownActive = true;
            laserCooldownValue = 0.0f;


        } 

        //only update if the other interactor's cd is not on. If it is on, that interactor is handling this change instead.
        if (!otherInteractor.LaserCooldownActive()){

            laserCDSlider.value = Mathf.Min(1.0f, laserCooldownValue);

        }
    }

    private void ShootLaser(){
        
        laserObject =  Instantiate(laserPrefab, laserOrigin.position, Quaternion.identity).transform;

        RaycastHit hit;


        Physics.Raycast(laserOrigin.position, laserOrigin.forward, out hit, 200.0f, LayerMask.GetMask("Default"), QueryTriggerInteraction.Collide);

        if (null == hit.collider){
            laserEndLocation = laserOrigin.forward * 200.0f; 
        } else {
            laserEndLocation = hit.point; 


            //This allows healing off a target multiple times even if already hit
            //TODO - add flag to the target that will ensures healing can only be done if the target still has health 
            if (hit.collider.tag == "Target"){
                player.IncreaseHealth(0.25f);
            }


        }
        laserEndSet = false;


    }

    public bool LaserCooldownActive(){
        return laserCooldownActive;
    }

    public bool CheckEitherLaserCooldown(){
        return (laserCooldownActive || otherInteractor.LaserCooldownActive());
    }
}
