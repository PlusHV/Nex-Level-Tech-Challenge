using UnityEngine;
using TMPro;
public class MenuManager : MonoBehaviour
{

    public float distance = 2.0f;
    public Player player;
 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {

        Transform camera = player.transform.parent; 
        this.transform.position = camera.position + new Vector3(camera.forward.x, camera.forward.y, camera.forward.z).normalized * distance;
        this.transform.LookAt(camera.position);
        this.transform.forward *= -1;
    }


    public void ActivateMenu(string message){

        this.transform.gameObject.SetActive(true);

        Transform messageT = this.transform.Find("Message");

        messageT.GetComponent<TMP_Text>().text = message;

    }
}
