using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SunEnergy : MonoBehaviour
{
    [Header("UI 이동 연출")]
    [SerializeField] private GameObject flyImagePrefab; // 반드시 UI용 Image 프리팹
    [SerializeField] private RectTransform targetUI;     // UI 타겟 (예: 돈 아이콘)

    [SerializeField] private int value = 25;
    [SerializeField] private float fallSpeed = 1.5f;
    [SerializeField] private float stopY = 1.5f;
    [SerializeField] private float lifeTime = 6f;
    [SerializeField] private float sideDriftRange = 1f; // 좌우 이동 범위

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

        //  좌우 방향 중 하나 선택
        float direction = Random.value > 0.5f ? 1f : -1f;
        float offset = Random.Range(0.2f, sideDriftRange);

        // X축 기준으로 약간 흔들림 포함한 도착 지점
        targetPos = startPos + new Vector3(offset * direction, -Mathf.Abs(startPos.y - stopY), 0);

        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        if (isFalling)
        {
            //부드럽게 목표 위치로 이동
            transform.position = Vector3.MoveTowards(transform.position, targetPos, fallSpeed * Time.deltaTime);

            // 도착하면 멈춤
            if (Vector3.Distance(transform.position, targetPos) < 0.01f)
            {
                isFalling = false;
            }
        }
    }

   private void OnMouseDown()
   {   
   
       // 화면 좌표 계산
       //Vector3 start = Camera.main.WorldToScreenPoint(transform.position);
       //GameObject image = Instantiate(flyImagePrefab, transform.parent); // Canvas 아래에 생성
       //image.transform.SetParent(GameObject.Find("UIMainGame").transform, false); // Canvas 이름 확인
       //image.transform.position = start;
   
       // 이동 연출
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
    //    // 출발 위치
    //    Vector2 screenStart = Camera.main.WorldToScreenPoint(transform.position);
    //    Vector2 startAnchoredPos;
    //    if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect, screenStart, Camera.main, out startAnchoredPos))
    //    {
    //        Debug.LogError("Start 변환 실패");
    //        yield break;
    //    }
    //
    //    // 도착 위치 (targetUI.position은 월드 위치이므로 ScreenPoint로 변환 필요)
    //    Vector2 screenEnd = Camera.main.WorldToScreenPoint(target.position);
    //    Vector2 endAnchoredPos;
    //    if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect, screenEnd, Camera.main, out endAnchoredPos))
    //    {
    //        Debug.LogError("End 변환 실패");
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
