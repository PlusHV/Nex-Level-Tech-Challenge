using UnityEngine;
using System.Collections.Generic;
public class Enemy : MonoBehaviour
{


    public float maxHealth = 1.0f;
    [SerializeField]
    private float health;

    private bool isDead = false;

    public JSONParser jsonParser;
    public IndicatorLoader indicatorLoader;
    public MeshGenerator mg;
    //public List<string>() attackList;

    public string[] attackPool;

    public string[] attackList;
    private List<Attack> attackQueue;

    private bool waiting = false;
    private float waitTime = 0.0f;
    private float timer = 0.0f;


    private float duration = 0.0f;
    private float durationTimer = 0.0f;

    public Material inactive;
    public Material active;
    public Material enemyHit;
    public Material dead;

    public bool isActive;

    public Door linkedDoor;
    
    public Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


        attackQueue = new List<Attack>();

        //attackQueue = jsonParser.ParseList(attackList);
        SetHealth();

        

       
    }

    // Update is called once per frame
    void Update()
    {
        



        UpdateTimer();

        if (!isDead && isActive){
            this.transform.Find("Sphere").GetComponent<MeshRenderer>().material = active;
            if (!waiting && attackQueue.Count > 0){
                //activate next attack

                if (attackQueue[0].Type == "indicator"){
                    //play indicator
                    if (null != indicatorLoader.GetPrefab(attackQueue[0])){
                        GameObject indicator = Instantiate(indicatorLoader.GetPrefab(attackQueue[0]), this.transform);

                        indicator.transform.GetComponent<IndicatorMovement>().InitializeIndicator(attackQueue[0]);
                        //default timers for indicators
                        waiting = true;
                        timer = 0.0f;
                        waitTime = 4.0f;

                        duration = 2.0f;
                        durationTimer = 0.0f;
                    }

                    attackQueue.RemoveAt(0);
                } else {

                    List<Vector3> points = attackQueue[0].CreateAttackVertices(transform.position);
                    
                    // if (attackQueue[0].Repeat){
                    //     attackQueue.Add(attackQueue[0]);
                    // }


                    

                    mg.GenerateMesh(points, !(attackQueue[0].Type == "cardinals") );

                    mg.SetDamage(attackQueue[0].Damage);

                    waiting = true;
                    timer = 0.0f;
                    waitTime = attackQueue[0].WaitDuration;

                    duration = attackQueue[0].Duration;
                    durationTimer = 0.0f;


                    attackQueue.RemoveAt(0);
                }

            } else if (attackQueue.Count <= 0){ //refill the attack queue
                List<string> attackPoolCopy = new List<string>(attackPool);//copy of the attackPool
                //System.Random rand = new System.Random();
                List<string> tempAttackList = new List<string>();
                while (attackPoolCopy.Count > 0) {

                    int index = Random.Range(0, attackPoolCopy.Count);

                    tempAttackList.Add("indicator"); //we add an indicator before each attack
                    foreach(string element in jsonParser.ParseAttackVariation(attackPoolCopy[index] ) ){ //Get a variation of the chosen attack and add each attack
                        tempAttackList.Add(element);
                    }

                    attackPoolCopy.RemoveAt(index); //remove from pool
                }


                attackQueue = jsonParser.ParseList(tempAttackList.ToArray() );
                attackList = tempAttackList.ToArray();
                

            }
        }

    }


    private void UpdateTimer(){
        if (waiting){

            timer+= Time.deltaTime;

            if (timer >= waitTime){
                waiting = false;
                
            }




        }

        if (durationTimer < duration){
            durationTimer+= Time.deltaTime;

            if (durationTimer >= duration)
            mg.Reset();
        }

    }


    private void SetHealth(){
        health = maxHealth;
        isDead = false;
    }



    public void ReduceHealth(float val){


        health = Mathf.Max(0.0f, health - val);

        if (health <= 0.0f){
            //enemy dies
            isDead = true;
            this.transform.Find("Sphere").GetComponent<MeshRenderer>().material = dead;
            isActive = false;
            mg.Reset();

            if (null != linkedDoor){
                linkedDoor.UnlockDoor();
            }

            if (null != player){
                player.WinGame();
            }

        }
        
    }


    void OnTriggerEnter(Collider other){
        if (other.tag != "PlayerProjectile"){
            return;
        }

        if (!isActive && !isDead) {
            this.transform.Find("Sphere").GetComponent<MeshRenderer>().material = active;
            isActive = true;
            //ReduceHealth(1.0f);
        } else if (isActive && !isDead) {
            this.transform.Find("Sphere").GetComponent<MeshRenderer>().material = enemyHit;
            ReduceHealth(0.25f);
            
        }
        
        


    }

}
