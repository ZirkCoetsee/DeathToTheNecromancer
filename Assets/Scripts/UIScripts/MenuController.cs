using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class MenuController : MonoBehaviour
{

    [Header("Player Stats")]
    [SerializeField] private TMP_Text healthStat = null;
    [SerializeField] private TMP_Text speedStat = null;
    [SerializeField] private TMP_Text damageStat = null;
    Player player;

    [SerializeField] CardsCreator cardsCreator;
    [SerializeField] private TMP_Text Card1StatPositive = null;
    [SerializeField] private TMP_Text Card1StatNegative = null;
    [SerializeField] private TMP_Text Card2StatPositive = null;
    [SerializeField] private TMP_Text Card2StatNegative = null;
    [SerializeField] private TMP_Text Card3StatPositive = null;
    [SerializeField] private TMP_Text Card3StatNegative = null;

    [SerializeField] private TMP_Text Card1StatValuePositive = null;
    [SerializeField] private TMP_Text Card1StatValueNegative = null;
    [SerializeField] private TMP_Text Card2StatValuePositive = null;
    [SerializeField] private TMP_Text Card2StatValueNegative = null;
    [SerializeField] private TMP_Text Card3StatValuePositive = null;
    [SerializeField] private TMP_Text Card3StatValueNegative = null;


    [Header("Volume Settings")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private float defaultVolume = 0.5f;

    [Header("Confirmation Icon")]
    [SerializeField] private GameObject confirmationPrompt = null;

    [Header("Level To Load")]
    public string _levelToLoad;
    // [SerializeField] private GameObject noSavedGameDialog = null;
    bool isDisplaying = false;
    bool isPaused = false;
    string nameOfStat;
    float statValue;

    int cardNum1;
    int cardNum2;
    int cardNum3;

    public void NewGame()
    {
        MusicPlayer musicplayer = FindObjectOfType<MusicPlayer>();
        Destroy(musicplayer);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
        player = FindObjectOfType<Player>();
    }

    //If you wanted to load saved games
    // public void LoadGameDialogYes(){
    //     if(PlayerPrefs.HasKey("SavedLevel")){
    //         levelToLoad = PlayerPrefs.GetString("SavedLevel");
    //         SceneManager.LoadScent(levelToLoad);
    //     }
    //      else
    //     {
    //          noSavedGameDialog.SetActive(true);
    //     }
    // }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0.0");
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume",AudioListener.volume);
        StartCoroutine(ConfirmationBox());
    }

    public IEnumerator ConfirmationBox(){

        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        confirmationPrompt.SetActive(false);

    }

    public void ResetButton(string MenuType)
    {
        if(MenuType == "Audio")
        {
            AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            volumeTextValue.text = defaultVolume.ToString("0.0");
        }
        StartCoroutine(ConfirmationBox());
    }

    public void DisplayPause()
    {
        if(isDisplaying){
            isDisplaying = false;
            PauseGame();
        }else{
            isDisplaying = true;
            PauseGame();
        }
    }

    public void PauseGame(){
        Debug.Log("Is paused value: " + isPaused);
        if(isPaused){
        Time.timeScale = 1;
        AudioListener.pause = false;
        isPaused = false;
        Debug.Log("Resume Game");

        }else{
        Time.timeScale = 0;
        AudioListener.pause = true;
        isPaused = true;
        Debug.Log("Pause Game");


        }
    }

    public void ContinueGame(){
        Time.timeScale = 1;
        AudioListener.pause = false;
        isPaused = false;
        Debug.Log("Resume Game");
    }

    public void RestartLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void GetPlayerStats()
    {
        player = FindObjectOfType<Player>();
        healthStat.text =  (Mathf.Round(player.maxHealth * 10.0f) * 0.1f).ToString();
        speedStat.text = (Mathf.Round(player.maxSpeed * 10.0f) * 0.1f).ToString();
        damageStat.text = (Mathf.Round(player.maxDamage * 10.0f) * 0.1f).ToString();

    }

    public void GetCardStat(string _nameOfStat)
    {
        nameOfStat = _nameOfStat;
    }

    public void GetCardStatValue(float _statValue)
    {
        statValue = _statValue;
    }
    
    public void SetCardsValues()
    {
        player = FindObjectOfType<Player>();
        Debug.Log($"Cards: {cardsCreator.cards}");

        cardNum1 = Random.Range(0, 49);
        cardNum2 = Random.Range(0, 49);
        cardNum3 = Random.Range(0, 49);

        Card1StatValuePositive.text = "+" + cardsCreator.cards[cardNum1].positiveSkillValue.ToString() + "%";
        Card1StatValueNegative.text = "-" + cardsCreator.cards[cardNum1].negativeSkillValue.ToString() + "%";
        Card2StatValuePositive.text = "+" + cardsCreator.cards[cardNum2].positiveSkillValue.ToString() + "%";
        Card2StatValueNegative.text = "-" + cardsCreator.cards[cardNum2].positiveSkillValue.ToString() + "%";
        Card3StatValuePositive.text = "+" + cardsCreator.cards[cardNum3].positiveSkillValue.ToString() + "%";
        Card3StatValueNegative.text = "-" + cardsCreator.cards[cardNum3].positiveSkillValue.ToString() + "%";

        Card1StatPositive.text = cardsCreator.cards[cardNum1].positiveStat.ToString();
        Card1StatNegative.text = cardsCreator.cards[cardNum1].negativeStat.ToString();
        Card2StatPositive.text = cardsCreator.cards[cardNum2].positiveStat.ToString();
        Card2StatNegative.text = cardsCreator.cards[cardNum2].negativeStat.ToString();
        Card3StatPositive.text = cardsCreator.cards[cardNum3].positiveStat.ToString();
        Card3StatNegative.text = cardsCreator.cards[cardNum3].negativeStat.ToString();

    }
    public void ApplyCardStats(int cardNum)
    {
        player = FindObjectOfType<Player>();
        Debug.Log("Card Selected: " + cardNum);
        switch (cardNum)
        {
            case 1:
                switch (cardsCreator.cards[cardNum1].positiveStat.ToString())
                {
                    case "maxHealth":
                        // code block
                        player.maxHealth += player.maxHealth * (cardsCreator.cards[cardNum1].positiveSkillValue / 100);
                        Debug.Log("New Max Health =" + player.maxHealth);
                        break;
                    case "maxSpeed":
                        // code block
                        player.maxSpeed += player.maxHealth * (cardsCreator.cards[cardNum1].positiveSkillValue / 100);
                        Debug.Log("New Max Speed =" + player.maxHealth);

                        break;
                    case "maxDamage":
                        // code block
                        player.maxDamage += player.maxHealth * (cardsCreator.cards[cardNum1].positiveSkillValue / 100);
                        Debug.Log("New Max Damage =" + player.maxHealth);

                        break;
                    default:
                        break;
                }
                switch (cardsCreator.cards[cardNum1].negativeStat.ToString())
                {
                    case "maxHealth":
                        // code block
                        player.maxHealth -= player.maxHealth * (cardsCreator.cards[cardNum1].negativeSkillValue / 100);
                        Debug.Log("New Max Health =" + player.maxHealth);
                        break;
                    case "maxSpeed":
                        // code block
                        player.maxSpeed -= player.maxHealth * (cardsCreator.cards[cardNum1].negativeSkillValue / 100);
                        Debug.Log("New Max Speed =" + player.maxHealth);

                        break;
                    case "maxDamage":
                        // code block
                        player.maxDamage -= player.maxHealth * (cardsCreator.cards[cardNum1].negativeSkillValue / 100);
                        Debug.Log("New Max Damage =" + player.maxHealth);

                        break;
                    default:
                        break;
                }
                break;
            case 2:
                switch (cardsCreator.cards[cardNum2].positiveStat.ToString())
                {
                    case "maxHealth":
                        // code block
                        player.maxHealth += player.maxHealth * (cardsCreator.cards[cardNum2].positiveSkillValue / 100);
                        Debug.Log("New Max Health =" + player.maxHealth);
                        break;
                    case "maxSpeed":
                        // code block
                        player.maxSpeed += player.maxHealth * (cardsCreator.cards[cardNum2].positiveSkillValue / 100);
                        Debug.Log("New Max Speed =" + player.maxHealth);

                        break;
                    case "maxDamage":
                        // code block
                        player.maxDamage += player.maxHealth * (cardsCreator.cards[cardNum2].positiveSkillValue / 100);
                        Debug.Log("New Max Damage =" + player.maxHealth);

                        break;
                    default:
                        break;
                }
                switch (cardsCreator.cards[cardNum2].negativeStat.ToString())
                {
                    case "maxHealth":
                        // code block
                        player.maxHealth -= player.maxHealth * (cardsCreator.cards[cardNum2].negativeSkillValue / 100);
                        Debug.Log("New Max Health =" + player.maxHealth);
                        break;
                    case "maxSpeed":
                        // code block
                        player.maxSpeed -= player.maxHealth * (cardsCreator.cards[cardNum2].negativeSkillValue / 100);
                        Debug.Log("New Max Speed =" + player.maxHealth);

                        break;
                    case "maxDamage":
                        // code block
                        player.maxDamage -= player.maxHealth * (cardsCreator.cards[cardNum2].negativeSkillValue / 100);
                        Debug.Log("New Max Damage =" + player.maxHealth);

                        break;
                    default:
                        break;
                }
                break;
            case 3:
                switch (cardsCreator.cards[cardNum3].positiveStat.ToString())
                {
                    case "maxHealth":
                        // code block
                        player.maxHealth += player.maxHealth * (cardsCreator.cards[cardNum3].positiveSkillValue / 100);
                        Debug.Log("New Max Health =" + player.maxHealth);
                        break;
                    case "maxSpeed":
                        // code block
                        player.maxSpeed += player.maxHealth * (cardsCreator.cards[cardNum3].positiveSkillValue / 100);
                        Debug.Log("New Max Speed =" + player.maxHealth);

                        break;
                    case "maxDamage":
                        // code block
                        player.maxDamage += player.maxHealth * (cardsCreator.cards[cardNum3].positiveSkillValue / 100);
                        Debug.Log("New Max Damage =" + player.maxHealth);

                        break;
                    default:
                        break;
                }
                switch (cardsCreator.cards[cardNum3].negativeStat.ToString())
                {
                    case "maxHealth":
                        // code block
                        player.maxHealth -= player.maxHealth * (cardsCreator.cards[cardNum3].negativeSkillValue / 100);
                        Debug.Log("New Max Health =" + player.maxHealth);
                        break;
                    case "maxSpeed":
                        // code block
                        player.maxSpeed -= player.maxHealth * (cardsCreator.cards[cardNum3].negativeSkillValue / 100);
                        Debug.Log("New Max Speed =" + player.maxHealth);

                        break;
                    case "maxDamage":
                        // code block
                        player.maxDamage -= player.maxHealth * (cardsCreator.cards[cardNum3].negativeSkillValue / 100);
                        Debug.Log("New Max Damage =" + player.maxHealth);

                        break;
                    default:
                        break;
                }
                break;
        }
      
    }

    public void ActivatePlayer()
    {
        player = FindObjectOfType<Player>();
        player.currentState = Player.State.Active;
    }

    public void DisablePlayer()
    {
        player = FindObjectOfType<Player>();
        player.currentState = Player.State.Stopped;

    }
}
