using UnityEngine;

[ExecuteAlways]
public class CameraSetting : MonoBehaviour
{
    private void OnEnable()
    {
        if (Camera.main != null)
        {
            Camera.main.transparencySortMode = TransparencySortMode.CustomAxis;
            Camera.main.transparencySortAxis = new Vector3(0, 1, 0); // Y축 정렬 기준
            //Debug.Log("SortMode: " + Camera.main.transparencySortMode);
            //Debug.Log("SortAxis: " + Camera.main.transparencySortAxis);
        }
    }
}