using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject btnStart;
    public Text txtBulletsCount;
    public Text txtBulletsPower;

    // Start is called before the first frame update
    void Start()
    {
        txtBulletsCount.text = GameManager.Instance.bulletCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void BtnAddBullets()
    {
        GameManager.Instance.bulletCount++;
        txtBulletsCount.text = GameManager.Instance.bulletCount.ToString();

    }

    public void BtnAddPower()
    {
        GameManager.Instance.buttlePower++;
        txtBulletsPower.text = GameManager.Instance.buttlePower.ToString();

    }

    public void StartGame()
    {
        btnStart.SetActive(false);
        GameManager.Instance.StartGame();

    }

    public void StopGame()
    {
        btnStart.SetActive(true);
        GameManager.Instance.StopGame();

    }

    public void OnSliderValueChange(float value)
    {
        GameManager.Instance.touchMove.speed = value;
    }
}
