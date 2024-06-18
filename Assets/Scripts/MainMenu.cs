using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    //serializefiled feld in unity zum auswählen von buttons/panels usw.
    [SerializeField] private CanvasGroup mainpanel;
    [SerializeField] private Button startnewGamebutton;
    [SerializeField] private Button levelselectionbutton;
    
    [SerializeField] private CanvasGroup levelSelectionPanel;
    [SerializeField] private Button level1button;
    [SerializeField] private Button level2button;
    [SerializeField] private Button level3button;
    [SerializeField] private Button quitlevelselectionbutton;
    
    [SerializeField] private Button QUitgamebutton;
    //string ist ein feld zum schreiben
    [SerializeField] private string nameNextScene1;
    [SerializeField] private string nameNextScene2;
    [SerializeField] private string nameNextScene3;
    // Start is called before the first frame update
    void Start()
    {
        //zuweisen was die einzelnen knöpfe/panel machen
        levelSelectionPanel.HideCanvasGroup();
        startnewGamebutton.onClick.AddListener(LoadLevel1);
        levelselectionbutton.onClick.AddListener(openLevelPanel);
        quitlevelselectionbutton.onClick.AddListener(closeLevelPanel);
        level1button.onClick.AddListener(LoadLevel1);
        level2button.onClick.AddListener(LoadLevel2);
        level3button.onClick.AddListener(LoadLevel3);
        QUitgamebutton.onClick.AddListener(quitgame);

        level2button.interactable = false;
        if (PlayerPrefs.HasKey(nameNextScene2))
        {

            if ( PlayerPrefs.GetInt(nameNextScene2) == 1)
            {
                level2button.interactable = true;
            }

           
        }
        
        level3button.interactable = false;
        if (PlayerPrefs.HasKey(nameNextScene3))
        {

            if ( PlayerPrefs.GetInt(nameNextScene3) == 1)
            {
                level3button.interactable = true;
            }

           
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
    void closeLevelPanel()
    {
        //levelscreen ausblenden
        levelSelectionPanel.HideCanvasGroup();
        mainpanel.ShowCanvasGroup();
    }
    void openLevelPanel()
    {
        //levelscreen zeigen
        levelSelectionPanel.ShowCanvasGroup();
        mainpanel.HideCanvasGroup();
    }
    
    void LoadLevel1()
    {
        //aus dem scenenmanager die scene laden die ins SerializeField geschrieben wurde
        //loadnextlevel
        SceneManager.LoadScene(nameNextScene1);
    }
   
    void LoadLevel2()
    {
        //loadnextlevel
        SceneManager.LoadScene(nameNextScene2);
    }
    
    void LoadLevel3()
    {
        //loadnextlevel
        SceneManager.LoadScene(nameNextScene3);
    }
    public void quitgame()
    {
        //außerhalb von unity das programm schließen
        Application.Quit();
        Debug.Log(message:"gameclosed");
    }
}
