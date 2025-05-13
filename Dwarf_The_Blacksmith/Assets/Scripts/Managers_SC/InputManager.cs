using UnityEngine;

public class InputManager : MonoBehaviour
{
    private EquipmentManager equipmentManager;

    void Start()
    {
        equipmentManager = GetComponent<EquipmentManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            equipmentManager.SwitchWeapons();
        }
    }
}
