using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private PlayerController player;
    public static GameManager instance;
    public Text liveText;
    public Text rocketText;
    public Text moneyText;
    public Text scoreText;
    public Text upgradeCostText;
    public int money;
    public int score;
    public float startWait;
    public GameObject[] enemies;
    public Boundary boundary;
    public Vector2 spawnWait;
    public int enemyCountMax = 10;
    public float spawnWaitMin;
    public float waveWait;
    public float waveWaitMin;
    public bool gameOver = false;
    private int enemyCount = 1;
    public GameObject loseWindow;

    private void Awake()
    {
        //find object that has script player controller and set that info as player
        player = FindObjectOfType<PlayerController>();
        instance = this;
        //money =PlayerPrefs.GetInt("Money");
        //score =PlayerPrefs.GetInt("Score");
    }

    private void Start()
    {
        StartCoroutine(SpawnWave());
    }

    private void Update()
    {
        SetMoneyText();
        SetScoreText();
        GameOver();
        if(Input.GetKeyDown(KeyCode.P))
        {
            money -= 10;
            PlayerPrefs.SetInt("Money", GameManager.instance.money);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            //delete player prefs key
            PlayerPrefs.DeleteKey("Money");
            PlayerPrefs.DeleteKey("Score");
        }
    }

    IEnumerator SpawnWave()
    {
        //wait for amount of our startwait value
        yield return new WaitForSeconds(startWait);
        //then check if its not game over
        while (!gameOver)
        {
            //loop through enemy list  
            for (int i = 0; i < enemyCount; i++)
            {
                // pick an enemy by random between index 0 and length of list 
                GameObject enemy = enemies[Random.Range(0, enemies.Length)];
                // pick a position by random between our x boundary and ymin boundary
                Vector3 spawnPosition = new Vector3(Random.Range(boundary.xMin, boundary.xMax), boundary.yMin, 0);
                Instantiate(enemy, spawnPosition, Quaternion.identity);
                //choose a random time between 2 value 
                yield return new WaitForSeconds(Random.Range(spawnWait.x, spawnWait.y));
            }
            //add one enemy to our loop
            enemyCount++;
            //check if enemy count become more than or equal of our max enemy that we should have in scene
            if(enemyCount >= enemyCountMax)
            {
                //then equal enemy count as max enemy count(for example if max is 10 and
                //number go to 11 it return back to 10
                enemyCount = enemyCountMax;
                //reduce 0.1 second from our spawn wait.x value
                spawnWait.x -= 0.1f;
                //reduce 0.1 second from our spawn wait.y value
                spawnWait.y -= 0.1f;
                //check if spawnwait.y become less than our spawn wait min then...
                if(spawnWait.y <= spawnWaitMin)
                {
                    //put value of our spawnWait.y as spawnwait min
                    spawnWait.y = spawnWaitMin;
                }
                //check if spawnwait.y become less than our spawn wait min then...
                if (spawnWait.x <= spawnWaitMin)
                {
                    //put value of our spawnWait.x as spawnwait min
                    spawnWait.x = spawnWaitMin;
                }
                yield return new WaitForSeconds(waveWait);
                waveWait -= 0.1f;
                if (waveWait <= waveWaitMin)
                    waveWait = waveWaitMin;
            }
        }
    }

    public void GameOver()
    {
        //if player lives become zero
        if(player.lives <= 0)
        {
            //activate our lose window
            loseWindow.SetActive(true);
            //stop the time
            Time.timeScale = 0;

        }
        else //if player lives is more than zero
        {
            //set the normal time of game
            Time.timeScale = 1;
        }
    }

    public void ReplyLevel()
    {
        //Play again the scene theat we are currently inside of it
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);       
        AudioManager.instance.PlaySFX(3);
    }

    public void BackToMainMenu()
    {
        //Load the scene with index zero
        SceneManager.LoadScene(0);
        AudioManager.instance.PlaySFX(3);
    }

    public void SetLivesText(int lives)
    {

        liveText.text ="X" +lives.ToString();
    }

    public void SetRocketText(int rocket)
    {
        rocketText.text = "X" + rocket.ToString();
    }

    public void SetMoneyText()
    {
        moneyText.text = money.ToString();
    }
    public void SetScoreText()
    {
        scoreText.text = score.ToString();
    }

    public void SetUpgradeCostText(int upgradeCost)
    {
        if(player.level < player.maxLevel)
        {
            upgradeCostText.text = upgradeCost.ToString();
        }
        else if(player.level == player.maxLevel)
        {
            upgradeCostText.text = "Max";
        }

    }
}
