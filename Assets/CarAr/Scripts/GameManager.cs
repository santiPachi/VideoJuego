using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;
    public  GameObject finalPortal;
    public PlayerController Player;
    public List<GameObject> Items;
    public Material materialRed;
    public Material materialYellow;
    private Transform spawnPoint; 


    public AudioClip[] collectableClips;
    private AudioSource sound; 

    private int points = 0;
    private int itemAmnt = 0;
    private int playerLifes = 3;
    private bool stopMovement = false;
    private bool playerMovement = true;
    private int itemsPerLevel ;
    private int highScore;
    
   
    void Awake()
    {
        if(instance == null){
            instance = this;
        }else if(instance != null){
            Destroy(gameObject);
        } 

        itemsPerLevel = Items.Count -1;
        Items[itemAmnt].GetComponent<Renderer>().material = materialRed;
    }
    public bool StopMovement{
        get{ return stopMovement;}
    }
    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();   
        highScore = PlayerPrefs.GetInt("Score",0); 
        // sound = GetComponent<AudioSource>;
    }

    // Update is called once per frame
    void Update()
    {
        ValidateAdvance();   
    }
    public void PickItem(){
        points += 10;
        itemAmnt ++;
        Items[itemAmnt].GetComponent<Renderer>().material = materialRed;
        // sound.PlayOneShot(collectableClips[0]);
        print("La cantidad de puntos "+points);
    }
    public void PickCoin(){
        points += 2;
        // sound.PlayOneShot(collectableClips[1]);
        print("La cantidad de puntos "+points);
    }
    public void LoseLifes(){
        if(playerLifes > 1){
            playerLifes --;
            KillPlayer();
            Invoke("SpawnPlayer",1f);
        }else{
            playerLifes = 0;
            FinishGame();
        }
    }

    public void EnablePortal(bool isEnable){
        finalPortal.SetActive(isEnable);
        stopMovement = isEnable;
    }
    public void PlayerMove(bool canMove){
        playerMovement = canMove;
    }

    public void SpawnPlayer(){
        // player.SetActive(true);
        PlayerMove(true);
        // player.transform.position = spawnPoint.position;
    }

    public void KillPlayer(){
        PlayerMove(false);
        Player.Reload(); 
    }
    public void LevelCompleted(){
        PlayerMove(false);
        if(points > highScore){
            PlayerPrefs.SetInt("Score",points);
        }
        print("La puntuacion maxima es"+highScore);
        // Invoke("PlayAgain",3f);

    }
    public void PlayAgain(){
        // SceneManager.LoadScene(0);
    }

    public void FinishGame(){
        KillPlayer();
        stopMovement = true;
    }

    public void ValidateAdvance(){
        if(itemsPerLevel == itemAmnt){
            LevelCompleted();
            EnablePortal(true);
        }
    }


  }
