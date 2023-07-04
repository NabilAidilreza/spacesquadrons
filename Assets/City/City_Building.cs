using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City_Building : MonoBehaviour
{
    public SpriteRenderer sprite_renderer;
    public Sprite[] Levels = new Sprite[3];
    public int LEVEL = 1;
    // Start is called before the first frame update
    void Start()
    {
        sprite_renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(LEVEL == 1){
            sprite_renderer.sprite = Levels[0];
        }
        else if(LEVEL == 2){
            sprite_renderer.sprite = Levels[1];
        }
        else if(LEVEL == 3){
            sprite_renderer.sprite = Levels[2];
        }
        else{
            sprite_renderer.sprite = Levels[2];
        }
    }
    public void IncreaseLevel(){
        LEVEL ++;
    }
    public int GetLevel(){
        return LEVEL;
    }
}
