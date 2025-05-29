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
            Debug.Log($"[TestUnitInitializer] {unit.name} �ʱ�ȭ �Ϸ�");
        }
        else
        {
            Debug.LogWarning($"[TestUnitInitializer] {name} �ʱ�ȭ ���� - testData ����");
        }
    }
}
