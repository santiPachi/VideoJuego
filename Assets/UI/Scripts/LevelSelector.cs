using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  
using UnityEngine.UI;
public class LevelSelector : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject levelButton;
    public GameObject levelContariner;
    public GameObject UIMainMenu;
    private void Start()
    {
        Sprite[] thumbnails =  Resources.LoadAll<Sprite>("Levels");
        foreach(Sprite tb in thumbnails){
            GameObject container =  Instantiate(levelButton) as GameObject;
            container.GetComponent<Image>().sprite = tb;
            container.transform.SetParent(levelContariner.transform,false);
            string sceneName = tb.name;
            container.GetComponent<Button>().onClick.AddListener(()=> LoadLevel(sceneName));
        }
    }

   
    private void LoadLevel(string sceneName){
        UIMainMenu.SetActive(false);
        SceneManager.LoadScene(sceneName);
    }

    // Update is called once per frame
   
}
