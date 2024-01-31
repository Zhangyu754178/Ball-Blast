using TMPro;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float bounceForce = 2f; // 球被击中后向上弹起的力
    public float upForce = 10f; // 球被击中后向上弹起的力
    public float cooldownTime = 1.0f; // 间隔时间
    public TextMeshProUGUI textMeshPro;
    public float baseForce = 2f;

    public float gravityScale = 0.4f;
    public float moveSpeed = 1f;

    public float ballCount = 100;
    public CircleCollider2D circleCollider;
    public Rigidbody2D ballRigidbody;
    private float lastBounceTime;

    [HideInInspector]
    public bool isLeft = false;

    [HideInInspector]
    public int difficulty = 1;

    private float bulletPower = 1;
    private void Start()
    {
        ballRigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        circleCollider = this.gameObject.GetComponent<CircleCollider2D>();
        lastBounceTime = -cooldownTime; // 初始化为一个较小的值，确保第一次可以受到力
        bulletPower = GameManager.Instance.buttlePower;
    }

    public void BallCreate()
    {
        ballRigidbody = this.gameObject.GetComponent<Rigidbody2D>();

        // 缓慢移动到屏幕中间
        // LeanTween.moveX(gameObject, transform.position.x + moveX, 1.5f).setEaseOutQuad().setOnComplete(Init);
        BallCreateInit();
        // 添加角度旋转
        float rotateAngle = isLeft ? -360f : 360f;
        LeanTween.rotateZ(gameObject, rotateAngle, moveSpeed).setEaseOutQuad();

        transform.localScale = transform.localScale * difficulty;
        ballCount = ballCount * difficulty;
        textMeshPro.text = ballCount.ToString();
    }

    public void BallCreateInit()
    {
        ballRigidbody.gravityScale = gravityScale;
        float Force = isLeft ? -baseForce : baseForce;
        ballRigidbody.AddForce(Vector2.left * Force, ForceMode2D.Impulse);
        ballRigidbody.AddForce(Vector2.up * baseForce, ForceMode2D.Impulse);

    }

    public void GameCreate()
    {
        float moveX = isLeft ? 1f : -1f;

        // 缓慢移动到屏幕中间
        LeanTween.moveX(gameObject, transform.position.x + moveX, moveSpeed).setEaseOutQuad().setOnComplete(Init); ;

        // 添加角度旋转
        float rotateAngle = isLeft ? -360f : 360f;
        LeanTween.rotateZ(gameObject, rotateAngle, moveSpeed).setEaseOutQuad();

        transform.localScale = transform.localScale * difficulty;
        ballCount = ballCount * difficulty;
        textMeshPro.text = ballCount.ToString();
    }


    public void Init()
    {
        ballRigidbody.gravityScale = gravityScale;

        float Force = isLeft ? -2f : 2f;

        ballRigidbody.AddForce(Vector2.left * Force, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        ballCount -= bulletPower;
        textMeshPro.text = ballCount.ToString();
        if (ballCount == 0)
        {
            int nextdifficulty = difficulty - 1;
            if (nextdifficulty > 0)
            {
                GameManager.Instance.ballController.SpawnBallCreater(difficulty - 1, transform.position,1);
                GameManager.Instance.ballController.SpawnBallCreater(difficulty - 1, transform.position, 2);
            }
            Destroy(gameObject);
        }
        //球的生命值到0时，加一个向上力
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "bottomboard")
        {
            // 如果碰撞体的名字是 "bottomboard"
            // 在这里添加你希望执行的操作
            // 例如：将球的速度设为零
            // ballRigidbody.velocity = Vector2.zero;

            // 添加一个向上的力
            // ballRigidbody.AddForce(Vector2.up * upForce, ForceMode2D.Impulse);
            Vector2 currentVelocity = ballRigidbody.velocity;
            switch (difficulty)
            {
                case 1:
                    ballRigidbody.velocity = new Vector2(currentVelocity.x, 5);
                    break;
                case 2:
                    ballRigidbody.velocity = new Vector2(currentVelocity.x, 5.5f);
                    break;
                case 3:
                    ballRigidbody.velocity = new Vector2(currentVelocity.x, 6);
                    break;
            }

        }
    }
}
