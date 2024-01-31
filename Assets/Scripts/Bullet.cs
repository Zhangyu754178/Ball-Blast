using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // 子弹的移动速度
    private Image image;

    private void Start()
    {
        image = gameObject.GetComponent<Image>();

    }

    void Update()
    {
        // 每一帧向上移动一定的距离
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
