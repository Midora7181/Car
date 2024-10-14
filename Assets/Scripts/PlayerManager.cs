using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static bool isGameStarted;
    public GameObject text;
    public  int numCoins;
    public Text textCoin;
    public static PlayerManager instance;
    private void Awake()
    {
            instance = this;
    }
    void Start()
    {
        numCoins = 0;
        isGameStarted = false;
        Time.timeScale = 1;
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }

        if (SwipeManager.tap && !isGameStarted)
        {
            isGameStarted = true;
            Destroy(text);

        }
    }

    public  void addCoin()
    {
        numCoins++;
        textCoin.text = "Coins: " + numCoins.ToString();
    }
}
