using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SunEnergy : MonoBehaviour
{
    [Header("UI �̵� ����")]
    [SerializeField] private GameObject flyImagePrefab; // �ݵ�� UI�� Image ������
    [SerializeField] private RectTransform targetUI;     // UI Ÿ�� (��: �� ������)

    [SerializeField] private int value = 25;
    [SerializeField] private float fallSpeed = 1.5f;
    [SerializeField] private float stopY = 1.5f;
    [SerializeField] private float lifeTime = 6f;
    [SerializeField] private float sideDriftRange = 1f; // �¿� �̵� ����

    private bool isFalling = true;
    private Vector3 startPos;
    private Vector3 targetPos;


    private void Awake()
    {
        if (targetUI == null)
        {
            GameObject go = GameObject.Find("Money");
            if (go != null)
                targetUI = go.GetComponent<RectTransform>();
        }
    }



    private void Start()
    {
        startPos = transform.position;

        //  �¿� ���� �� �ϳ� ����
        float direction = Random.value > 0.5f ? 1f : -1f;
        float offset = Random.Range(0.2f, sideDriftRange);

        // X�� �������� �ణ ��鸲 ������ ���� ����
        targetPos = startPos + new Vector3(offset * direction, -Mathf.Abs(startPos.y - stopY), 0);

        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        if (isFalling)
        {
            //�ε巴�� ��ǥ ��ġ�� �̵�
            transform.position = Vector3.MoveTowards(transform.position, targetPos, fallSpeed * Time.deltaTime);

            // �����ϸ� ����
            if (Vector3.Distance(transform.position, targetPos) < 0.01f)
            {
                isFalling = false;
            }
        }
    }

   private void OnMouseDown()
   {   
   
       // ȭ�� ��ǥ ���
       //Vector3 start = Camera.main.WorldToScreenPoint(transform.position);
       //GameObject image = Instantiate(flyImagePrefab, transform.parent); // Canvas �Ʒ��� ����
       //image.transform.SetParent(GameObject.Find("UIMainGame").transform, false); // Canvas �̸� Ȯ��
       //image.transform.position = start;
   
       // �̵� ����
       //StartCoroutine(FlyToUI(image.GetComponent<RectTransform>(), targetUI));
   
       GameManager.Instance.Player.AddMoney(value);
       Destroy(gameObject);
   }
    //
    //private IEnumerator FlyToUI(RectTransform fly, RectTransform target)
    //{
    //    float duration = 0.5f;
    //    float elapsed = 0f;
    //
    //    RectTransform parentRect = fly.parent.GetComponent<RectTransform>();
    //
    //    // ��� ��ġ
    //    Vector2 screenStart = Camera.main.WorldToScreenPoint(transform.position);
    //    Vector2 startAnchoredPos;
    //    if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect, screenStart, Camera.main, out startAnchoredPos))
    //    {
    //        Debug.LogError("Start ��ȯ ����");
    //        yield break;
    //    }
    //
    //    // ���� ��ġ (targetUI.position�� ���� ��ġ�̹Ƿ� ScreenPoint�� ��ȯ �ʿ�)
    //    Vector2 screenEnd = Camera.main.WorldToScreenPoint(target.position);
    //    Vector2 endAnchoredPos;
    //    if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect, screenEnd, Camera.main, out endAnchoredPos))
    //    {
    //        Debug.LogError("End ��ȯ ����");
    //        yield break;
    //    }
    //
    //    fly.anchoredPosition = startAnchoredPos;
    //
    //    while (elapsed < duration)
    //    {
    //        elapsed += Time.deltaTime;
    //        float t = Mathf.Clamp01(elapsed / duration);
    //        fly.anchoredPosition = Vector2.Lerp(startAnchoredPos, endAnchoredPos, t);
    //        yield return null;
    //    }
    //
    //    Destroy(fly.gameObject);
    //}

    public void SetValue(int amount)
    {
        value = amount;
    }
}
