using UnityEngine;

public class TestUnitInitializer : MonoBehaviour
{
    [SerializeField] private UnitData testData;

    private void Start()
    {
        Unit unit = GetComponent<Unit>();
        if (unit != null && testData != null)
        {
            unit.Init(testData);
            Debug.Log($"[TestUnitInitializer] {unit.name} 초기화 완료");
        }
        else
        {
            Debug.LogWarning($"[TestUnitInitializer] {name} 초기화 실패 - testData 누락");
        }
    }
}
