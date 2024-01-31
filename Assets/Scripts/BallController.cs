using UnityEngine;

public class BallController : MonoBehaviour
{
    public GameObject ballPrefab;
    public GameObject posParent;

    public float spawnXOffset = 800f;
    public Transform ballParent; 
    public float spawnInterval = 2f; 
    public int maxBalls = 4; 

    void Start()
    {
    }

    public void StartGame()
    {
        InvokeRepeating("SpawnBall", 0f, spawnInterval);
    }

    public void StopGame()
    {
        CancelInvoke("SpawnBall");

    }

    public void SpawnBallCreater(int difficulty,Vector3 postion,int side)
    {
        GameObject ballobj = Instantiate(ballPrefab, ballParent);
        ballobj.transform.position = postion;
        Ball ball = ballobj.GetComponent<Ball>();
        ball.isLeft = side == 1;
        ball.difficulty = difficulty;
        ball.BallCreate();
    }

    void SpawnBall()
    {
        if (ballParent.childCount < maxBalls)
        {
            int difficulty = Random.Range(1, 4); // 难度范围1-3
            int side = Random.Range(1, 3); // 1表示左侧，2表示右侧

            // 根据生成位置设置偏移
            float spawnX = side == 1 ? -5f : 5f; // 左侧为-5，右侧为5
            Transform spawnObjects = posParent.transform.Find("ballCreater" + difficulty + "-" + side);

            GameObject ballobj = Instantiate(ballPrefab, ballParent);
            ballobj.transform.position = spawnObjects.position;

            Ball ball = ballobj.GetComponent<Ball>();
            ball.isLeft = side == 1;
            ball.difficulty = difficulty;
            ball.GameCreate();
        }
    }
}
