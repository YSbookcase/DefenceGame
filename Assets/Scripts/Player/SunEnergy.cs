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
