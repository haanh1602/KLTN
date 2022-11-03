using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public static UImanager _ins;

    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject popupPerk;
    [SerializeField] GameObject buttonSound, buttonMusic;
    [SerializeField] Sprite imgSoundOff, imgSoundOn, imgMusicOff, imgMusicOn;
    [SerializeField] GameObject exp, power, hp;
    [SerializeField] Text textLevel;

    [SerializeField] List<Sprite> listPerk;
    [SerializeField] List<String> listDescription;


    [SerializeField] List<GameObject> showPerk;
    [SerializeField] List<Text> showDescription;

    private List<int> perks;
    private bool turnSound;
    private bool turnMusic;
    public static Action<int> selectPerk = delegate {};

    private void Awake()
    {
        _ins = this;
        perks = new List<int>();
    }
    void Start()
    {
        turnMusic = true;
        turnSound = true;
    }

    // Update is called once per frame
    void Update()
    {
        //OpenPopupPerk(new List<int> { 5, 6, 7 });
    }

    public void SelectPerk(int idPerk)
    {
        int id = perks[idPerk];
        id /= 4;
        popupPerk.SetActive(false);
        Time.timeScale = 1f;
    }
    public int[] listDebug;
    public void OpenPopupPerk(int[] list)
    {
        listDebug = list;
        
        popupPerk.SetActive(true);
        perks = new List<int>(list);
        for(int i = 0; i < perks.Count; i++)
        {
            Image img = showPerk[i].GetComponent<Image>();
            showDescription[i].text = listDescription[perks[i]];
            img.sprite = listPerk[perks[i]];
        }
        Time.timeScale = 0f;
        //set perk
    }

    public void CollectPerk()
    {
        popupPerk.SetActive(false);
        perks.Clear();
        Time.timeScale = 1f;
    }

    public void SetLevel()
    {
        textLevel.text = "Lv." + BasePlayer._ins.level;
    }

    public void SetUIExp()
    {
        float ratio = BasePlayer._ins.currentExp / BasePlayer._ins.maxExp;
        Image img = exp.GetComponent<Image>();
        img.fillAmount = ratio;
    }

    public void UIpower(float number)
    {
        Image img = power.GetComponent<Image>();
        img.fillAmount = number;
    }

    public void SetUIHp()
    {
        float ratio =(float) BasePlayer._ins.currentHp /(float) BasePlayer._ins.maxHp;
        Image img = hp.GetComponent<Image>();
        img.fillAmount = ratio;
    }

    public void btnPause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void btnResume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void btnExit()
    {
        pausePanel.SetActive(false);
        SceneManager.LoadScene("Home");
        Time.timeScale = 1.0f;
    }

    public void btnSound()
    {
        if(turnSound)
        {
            //turn off sound;
            Image img = buttonSound.GetComponent<Image>();
            img.sprite = imgSoundOff;
            turnSound = false;
        }
        else
        {
            //turn on sound;
            Image img = buttonSound.GetComponent<Image>();
            img.sprite = imgSoundOn;
            turnSound = true;
        }
    }

    public void btnMusic()
    {
        if (turnMusic)
        {
            //turn off music;
            Image img = buttonMusic.GetComponent<Image>();
            img.sprite = imgMusicOff;
            turnMusic = false;
        }
        else
        {
            //turn on music;
            Image img = buttonMusic.GetComponent<Image>();
            img.sprite = imgMusicOn;
            turnMusic = true;
        }
    }

    [SerializeField] GameObject panelGameOver;
    public void GameOver()
    {
        Time.timeScale = 0;
        panelGameOver.SetActive(true);

    }
}
