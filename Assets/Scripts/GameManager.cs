using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public BallController ballController;
    public Queue<Image> bulletPool = new Queue<Image>();
    public ShootBullet shootBullet;

    public UGUITouchMove touchMove;

    public int bulletCount = 10;
    public int buttlePower = 1;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    public void StartGame()
    {
        ballController.StartGame();
        shootBullet.StartGame();
        touchMove.enabled = true;
    }

    public void StopGame()
    {
        ballController.StopGame();
        shootBullet.StopGame();
        touchMove.enabled = false;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
