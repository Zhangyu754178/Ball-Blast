using UnityEngine;

public class UGUITouchMove : MonoBehaviour
{
    public float speed = 5f; // �ƶ��ٶ�
    private RectTransform rectTransform;
    private float minX, maxX;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        SetMinMaxX();
    }

    void SetMinMaxX()
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas != null)
        {
            RectTransform canvasRect = canvas.GetComponent<RectTransform>();
            float halfWidth = rectTransform.rect.width / 2;
            minX = -canvasRect.rect.width / 2 + halfWidth;
            maxX = canvasRect.rect.width / 2 - halfWidth;
        }
    }

    void Update()
    {
        MoveWithInput();
    }

    void MoveWithInput()
    {
        Vector3 currentPosition = rectTransform.localPosition;

#if UNITY_STANDALONE || UNITY_EDITOR
        // ʹ����������ȡ�ƶ�����
        float mouseDelta = Input.GetAxis("Mouse X");
        currentPosition.x += mouseDelta * speed * Time.deltaTime;
#elif UNITY_ANDROID || UNITY_IOS
        // ʹ�ô��������ȡ�ƶ�����
        if (Input.touchCount > 0)
        {
            float touchDelta = Input.GetTouch(0).deltaPosition.x;
            currentPosition.x += touchDelta * speed * Time.deltaTime;
        }
#endif

        // ��������Ļ��Χ��
        currentPosition.x = Mathf.Clamp(currentPosition.x, minX, maxX);

        // �������ƶ����µ�λ��
        rectTransform.localPosition = currentPosition;
    }
}
