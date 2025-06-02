using System.Collections;
using UnityEngine;

public class CameraIntroController : MonoBehaviour
{
    [SerializeField] private Transform endPosition;            // (25,8,0)
    [SerializeField] private Vector3 gameplayPosition = new(12f, 8f, 0f);
    [SerializeField] private float moveDuration = 3f;
    [SerializeField] private GameObject unitCardPanel;
    [SerializeField] private AnimationCurve easeOutCurve;      // Ease-out curve

    private void Start()
    {
        transform.position = new Vector3(5f, 8f, 0f); // 시작 위치
        unitCardPanel.SetActive(false);
        StartCoroutine(MoveTargetSmoothly());
    }

    private IEnumerator MoveTargetSmoothly()
    {
        Vector3 start = transform.position;
        Vector3 end = endPosition.position;

        float elapsed = 0f;
        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / moveDuration);
            float easedT = easeOutCurve.Evaluate(t);
            transform.position = Vector3.Lerp(start, end, easedT);
            yield return null;
        }

        unitCardPanel.SetActive(true);
    }

    public void OnClickPlayButton()
    {
        // 플레이 시에는 감속 없이 즉시 이동
        transform.position = gameplayPosition;
        unitCardPanel.SetActive(false);
    }
}
