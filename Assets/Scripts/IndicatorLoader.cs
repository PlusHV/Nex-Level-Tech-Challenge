using UnityEngine;

public class IndicatorLoader : MonoBehaviour
{

    public GameObject crossPrefab;
    public GameObject directionalPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetPrefab(Attack attack){
        if (attack.AttackType == "cross"){
            return crossPrefab;
        } else if (attack.AttackType == "directional"){
            return directionalPrefab;
        }
        return null;
    }
}
