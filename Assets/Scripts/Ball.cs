using TMPro;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float bounceForce = 2f; // �򱻻��к����ϵ������
    public float upForce = 10f; // �򱻻��к����ϵ������
    public float cooldownTime = 1.0f; // ���ʱ��
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
        lastBounceTime = -cooldownTime; // ��ʼ��Ϊһ����С��ֵ��ȷ����һ�ο����ܵ���
        bulletPower = GameManager.Instance.buttlePower;
    }

    public void BallCreate()
    {
        ballRigidbody = this.gameObject.GetComponent<Rigidbody2D>();

        // �����ƶ�����Ļ�м�
        // LeanTween.moveX(gameObject, transform.position.x + moveX, 1.5f).setEaseOutQuad().setOnComplete(Init);
        BallCreateInit();
        // ��ӽǶ���ת
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

        // �����ƶ�����Ļ�м�
        LeanTween.moveX(gameObject, transform.position.x + moveX, moveSpeed).setEaseOutQuad().setOnComplete(Init); ;

        // ��ӽǶ���ת
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
        //�������ֵ��0ʱ����һ��������
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "bottomboard")
        {
            // �����ײ��������� "bottomboard"
            // �����������ϣ��ִ�еĲ���
            // ���磺������ٶ���Ϊ��
            // ballRigidbody.velocity = Vector2.zero;

            // ���һ�����ϵ���
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
