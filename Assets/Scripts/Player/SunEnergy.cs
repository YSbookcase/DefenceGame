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

    public void SetValue(int amount)
    {
        value = amount;
    }

    private void OnMouseDown()
   {   

       GameManager.Instance.Player.AddMoney(value);
       Destroy(gameObject);
   }




}
