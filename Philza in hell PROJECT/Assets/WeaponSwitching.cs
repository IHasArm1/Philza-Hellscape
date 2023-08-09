using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;
    MainPlayerInput controls;

    float SelecterAxis;

    private void Awake()
    {
        controls = new MainPlayerInput();
        controls.Player.WeaponSlider.performed += ChangeWeaponSelected;
    }
    void Start()
    {
        SelectWeapon();
    }

    void ChangeWeaponSelected(InputAction.CallbackContext context)
    {
        SelecterAxis = context.ReadValue<float>();
    }

    void NumpadSelector()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (controls.Player.WeaponNumSelector.triggered)
        {
            Debug.Log(controls.Player.WeaponNumSelector.ReadValue<float>());
        }

        int previousSelectedWeapon = selectedWeapon;

        if (SelecterAxis > 0f)
        {
            if(selectedWeapon >= transform.childCount - 1)
            {
                selectedWeapon = 0;
            } else
            {
                selectedWeapon++;
            }
        }
        if (SelecterAxis < 0f)
        {
            if (selectedWeapon <= 0)
            {
                selectedWeapon = transform.childCount - 1;
            }
            else
            {
                selectedWeapon--;
            }
        }
        //changing weapon after all calculations are done
        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }

    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if(i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            } else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
