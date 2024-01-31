using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // �ӵ����ƶ��ٶ�
    private Image image;

    private void Start()
    {
        image = gameObject.GetComponent<Image>();

    }

    void Update()
    {
        // ÿһ֡�����ƶ�һ���ľ���
        transform.Translate(Vector3.up * speed * Time.deltaTime);

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        //Destroy(this.gameObject);
        this.gameObject.SetActive(false);
        image.rectTransform.anchoredPosition = Vector2.zero;
        GameManager.Instance.bulletPool.Enqueue(image);
    }
}
