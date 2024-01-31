using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class ShootBullet : MonoBehaviour
{
    public Image bulletPrefab;
    public RectTransform bulletSpawnPoint;
    public RectTransform bulletParent;

    private Queue<Image> bulletPool;
    public float bulletCount = 10; // 初始值
    private const float FireRate = 0.05f;
    private float fixedFireRate = FireRate; // 固定的发射间隔
    private int OneSecondShootTimes = 0;
    private int fixWidth = 7;
    private Coroutine gameCoroutine;
    private void Start()
    {

    }


    public void StartGame()
    {
        bulletCount = GameManager.Instance.bulletCount;
        bulletPool = GameManager.Instance.bulletPool;
        gameCoroutine = StartCoroutine(ShootBullets());
    }

    public void StopGame()
    {
        StopCoroutine(gameCoroutine);
    }

    //todo 计算发射子弹补偿
    IEnumerator ShootBullets()
    {

        float timeToShoot = 1.0f / fixedFireRate;

        float realFireRate = FireRate; // 固定的发射间隔

        int bulletsToShoot = Mathf.FloorToInt(bulletCount / timeToShoot);

        float realSecondBullets = bulletsToShoot * timeToShoot;

        float needAddBullets = bulletCount - realSecondBullets;

        while (true)
        {


            if (bulletCount < timeToShoot)
            {
                realFireRate = 1 / bulletCount;
                timeToShoot = bulletCount;
            }
            int realbulletsToShoot = bulletsToShoot;
            if (needAddBullets > 0)
            {
                needAddBullets--;
                realbulletsToShoot += 1;
            }


            if (realbulletsToShoot < 1)
                realbulletsToShoot = 1;

            for (int i = 0; i < realbulletsToShoot; i++)
            {
                Image bullet = GetBullet();
                RectTransform bulletRect = bullet.rectTransform;
                float offset = -((realbulletsToShoot - 1) * fixWidth) / 2;
                bulletRect.anchoredPosition = bulletSpawnPoint.anchoredPosition + new Vector2(offset + i * fixWidth, 0f);
            }

            OneSecondShootTimes += 1;
            if(OneSecondShootTimes == timeToShoot)
            {
                OneSecondShootTimes = 0;
                needAddBullets = bulletCount - realSecondBullets;
            }

            yield return new WaitForSeconds(realFireRate);

        }
    }

    public Image GetBullet()
    {
        if (bulletPool.Count == 0)
        {
            Image bullet = Instantiate(bulletPrefab, bulletParent);
            RectTransform bulletRect = bullet.rectTransform;
            bulletRect.SetParent(bulletParent);
            return bullet;
        }
        else
        {
            Image bullet = bulletPool.Dequeue();
            bullet.gameObject.SetActive(true);
            return bullet;
        }
    }
}
