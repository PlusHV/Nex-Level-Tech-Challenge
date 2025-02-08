using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{

    public PlayerHealth playerHealthUI;
    public MenuManager menuManager;


    const float maxHealth = 8.0f;
    [SerializeField]
    private float health = maxHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IncreaseHealth(0.0f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReduceHealth(float val){
        health = Mathf.Max(0.0f, health - val);

        if (health <= 0.0f){
            menuManager.ActivateMenu("Game Over!");
        }
        playerHealthUI.UpdatePlayerHealth(health);
    }

    public void IncreaseHealth(float val){
        health = Mathf.Min(maxHealth, health + val);
        playerHealthUI.UpdatePlayerHealth(health);
    }

    void OnTriggerEnter(Collider other){
        
    
        if (null != other.transform.GetComponent<MeshGenerator>()){

            ReduceHealth(other.transform.GetComponent<MeshGenerator>().Damage);
        }
    }

    public void WinGame(){
        menuManager.ActivateMenu("You Win!");
    }

    public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
