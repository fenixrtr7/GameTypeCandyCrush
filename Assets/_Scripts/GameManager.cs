using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance;
    public bool inGame  { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        inGame = false;
        
        if(sharedInstance == null){
            sharedInstance = this;
        }else{
            Destroy(gameObject);
        }
    }
    public void InGame(){
        inGame = true;
    }

    public void StopGame(){
        inGame = false;
    }
    
}
