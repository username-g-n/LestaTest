using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;
using UnityEngine.SceneManagement;
using StarterAssets;

public class Player : MonoBehaviour
{
    public Image healthbar;
    [SerializeField] private float healthValue;
    [SerializeField] private bool isImmune = false;
    [SerializeField] private float immunityTimeSec = 0.2f;
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private TextMeshProUGUI hp;
    [SerializeField] private TextMeshProUGUI timeInGame;
    public bool startTimer = false;
    [SerializeField] private float gameTime = 0;
    private string rr="";
    [SerializeField] private bool win;


    private void Start()
    {
        Time.timeScale = 1;
        startTimer = false;
        rr = healthValue.ToString();
        hp.text = rr;
        Cursor.lockState = CursorLockMode.Locked;

    }
    private void Update()
    {
        if (startTimer == true)
        {
            gameTime += 1 * Time.deltaTime;
         
        }
        if (transform.position.y < 0)
        {
            win = false;
            GetComponent<Player>().EndGame(win);
        }
      
    }
    public void OnRestartLevel()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);

    }
    public void OnMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void EndGame(bool winner)
    {

        Time.timeScale = 0;

        startTimer = false;
        if (winner == false)
        {
            Cursor.lockState = CursorLockMode.None;
            deathMenu.SetActive(true);
            
        }
        else
        {
            float gg = Mathf.Round(gameTime);
            //timeInGame.text = "sec: " + gg.ToString();
            //if (gg < 60)
            //{
                timeInGame.text = "Время: " + Mathf.Round(gg).ToString() + " cек. ";
            //}
            /*
            else if (gameTime >= 60)
            {
                if (gg < 3600)
                {
                    timeInGame.text = Mathf.Round(gg % 60 / 60).ToString() + " мин. " +
                        Mathf.Round(gg % 60).ToString() + " сек. ";
                }
                if (gg > 3600)
                {
                    timeInGame.text = Mathf.Round(gg / 3600).ToString() + " ч. " +
                        Mathf.Round(gg % 60 / 60).ToString() + " мин. " +
                        Mathf.Round(gg % 60).ToString() + " cек. ";
                }
            }
            */
            winMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
       
    }
    public void TakeDamage(int damage)
    {
        if (isImmune == false)
        {
            
            healthValue = healthValue - damage;
            healthbar.fillAmount = healthValue/ 100f;
            rr = healthValue.ToString();
            hp.text = rr;
            Debug.Log(rr);
            isImmune = true;
            StartCoroutine(IsImmune(immunityTimeSec));
            if (healthValue < 1)
            {
                win = false;
                GetComponent<Player>().EndGame(win);
            }

        }
    }

    IEnumerator IsImmune(float wasHurt)
    {
        yield return new WaitForSeconds(wasHurt);
        isImmune = false;
    }



    





}
