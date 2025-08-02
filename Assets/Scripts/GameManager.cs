using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int totalDays = 5;
    public int currentDay = 0;
    private bool playingDay;

    public int[] scaleShoppers;
    public int[] scaleNeedDegrees;
    public int[] needUnlockDay; // index matches enum index

    public int currentMoney = 0;
    public int earnedMoney = 0;
    public int moneyGoal;

    public int satisfiedShoppers = 0;
    public int dissatisfiedShoppers = 0;

    void Awake()
    {
        //If Instance does not exist yet, this instance should be the Instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("More than one Singleton in the scene " + transform + " - " + Instance);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playingDay = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDay()
    {
        if (playingDay)
        {
            return;
        }

        currentDay++;
        ShopperManager.Instance.StartDay(scaleShoppers[currentDay - 1]);
        playingDay = true;
    }

    public void EndDay()
    {
        if (currentDay + 1 > totalDays)
        {
            UIManager.Instance.ShowGameEndScreen();
        }
        else
        {
            playingDay = false;
        }
    }
}
