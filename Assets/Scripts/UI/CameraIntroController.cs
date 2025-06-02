using System.Collections;
using UnityEngine;

public class CameraIntroController : MonoBehaviour
{
    [SerializeField] private Transform endPosition;            // (25,8,0)
    [SerializeField] private Vector3 gameplayPosition = new(12f, 9.5f, 0f);
    [SerializeField] private float startDelay = 2f;
    [SerializeField] private float mapIntroDuration = 3f;
    [SerializeField] private float gameStartDuration = 1.5f; // ← 더 빠르게
    [SerializeField] private GameObject unitCardPanel;
    [SerializeField] private AnimationCurve easeOutCurve;      // 감속 곡선

    private bool isMoving = false;

    private void Start()
    {
        transform.position = new Vector3(5f, 9.5f, 0f);
        unitCardPanel.SetActive(false);

        StartCoroutine(BeginIntroAfterDelay(startDelay)); // 원하는 대기 시간 설정
    }

    public void OnClickPlayButton()
    {
        Debug.Log("플레이 버튼 눌림"); // ← 로그 꼭 남기기!
        if (!isMoving)
        {
            unitCardPanel.SetActive(false);
            StartCoroutine(MoveTo(gameplayPosition, gameStartDuration, null));
        }
    }


    private IEnumerator BeginIntroAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        yield return MoveTo(endPosition.position, mapIntroDuration, () =>
        {
            unitCardPanel.SetActive(true);
        });
    }



    private IEnumerator MoveTo(Vector3 destination, float duration, System.Action onComplete)
    {
        isMoving = true;

        Vector3 start = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            float easedT = easeOutCurve.Evaluate(t);
            transform.position = Vector3.Lerp(start, destination, easedT);
            yield return null;
        }

        transform.position = destination;
        isMoving = false;

        onComplete?.Invoke();
    }
}
