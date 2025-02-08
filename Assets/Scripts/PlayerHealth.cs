using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Texture2D[] textures;
    private Image[] hpIcons;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         hpIcons = this.transform.gameObject.GetComponentsInChildren<Image>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePlayerHealth(float newValue){


        //7
        //
        for (int i = 0; i < hpIcons.Length; i++){

            if (i+1 > newValue){ //no hp/partial

                if (i+1 < newValue + 1){ //partial hp
                    int partial = (int) ((newValue - i) / 0.25f); 

                    //Tex
                    //Sprite newSprite = Sprite.Create(textures[partial], new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                    hpIcons[i].sprite = MakeHPSprite(partial);
                } else {
                    //Sprite newSprite = Sprite.Create(textures[0], new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));  //empty hp
                    hpIcons[i].sprite = MakeHPSprite(0);
                }

            } else{ //full hp icon
                //Sprite newSprite = Sprite.Create(textures[4], new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                hpIcons[i].sprite = MakeHPSprite(4);
            }

        }

    }

    private Sprite MakeHPSprite(int index){
        Sprite newSprite = Sprite.Create(textures[index], new Rect(0, 0, textures[index].width, textures[index].height), new Vector2(0.5f, 0.5f));
        return newSprite;

    }
}
