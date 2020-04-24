using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;
    public  GameObject finalPortal;
    public GameObject UIGameOver;
    public GameObject UIWin;
    public GameObject UIGameControlls;
    public PlayerController Player;
    public List<GameObject> Items;
    public Material materialRed;
    public Material materialYellow;
    private Transform spawnPoint; 
 
    public AudioSource soundCoin;
    public AudioSource soundItem; 

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
        finalPortal.SetActive(false);
        itemsPerLevel = Items.Count;
        Items[itemAmnt].GetComponent<Renderer>().material = materialRed;

        int id = 0;
        foreach(GameObject itemObj in Items){
            Item item = itemObj.GetComponent<Item>();
            item.SetId(id);
            id +=1;
        }
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
    public void PlaceCar(){
        if(itemAmnt == 0){
            Player.car.transform.position = new Vector3(0f,0f,0f);
            Player.car.transform.eulerAngles = new Vector3(0f,0f,0f);
        }else{
            Player.car.transform.position = Items[itemAmnt-1].transform.position;
            Player.car.transform.rotation = Items[itemAmnt-1].transform.rotation;
        }
        playerLifes --;
    }
 
    public bool PickItem(int id){
        Debug.Log("id "+ id+" amnt "+itemAmnt);
        if(id == itemAmnt){

            points += 10;
            itemAmnt ++;
            Items[itemAmnt].GetComponent<Renderer>().material = materialRed;
            soundItem.Play();
            // sound.PlayOneShot(collectableClips[0]);
            print("La cantidad de puntos "+points);
            return true;
        }
        return false;
    }
    public void PickCoin(){
        points += 2;
        // sound.PlayOneShot(collectableClips[1]);
        soundCoin.Play();
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
        UIWin.SetActive(true);

    }
    public void PlayAgain(){
        SceneManager.LoadScene("Level1");
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
        if(playerLifes == 0 ){
            UIGameControlls.SetActive(false);
            UIGameOver.SetActive(true);
        }
    }


  }
