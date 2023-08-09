using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponWheelController : MonoBehaviour
{
    public GameObject player;
    public Animator anim;
    private bool weaponWheelSelected = false;
    public Image selectedItem;
    public Sprite noImage;
    public static int weaponID;
    public Sprite Fist;
    public Sprite Pistol;
    public Sprite Minigun;
    public Sprite SniperRifle;
    public Sprite Shotgun;
    public Sprite Scythe;
    public Sprite RocketLauncher;
    public Sprite MYSTERYITEM;
    public GameObject[] Weapons;
    public Button[] Buttons;
    public WeaponWheelButtonController[] ButtonScripts;
    private int selectedButton;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("WeaponWheelSelecter") < 0)
        {
            if(selectedButton == 0)
            {
                selectedButton = 8;
            } else
            {
                selectedButton = selectedButton--;
            }
            DisableButtons();
            SelectButton(selectedButton);
        } else if (Input.GetAxisRaw("WeaponWheelSelecter") > 0)
        {
            if (selectedButton == 8)
            {
                selectedButton = 0;
            } else
            {
                selectedButton = selectedButton++;
            }
            DisableButtons();
            SelectButton(selectedButton);
        }
        if (Input.GetButtonDown("WeaponWheel"))
        {
            weaponWheelSelected = !weaponWheelSelected;
        }

        if (weaponWheelSelected)
        {
            anim.SetBool("OpenWeaponWheel", true);
            player.GetComponent<PlayerMovement>().canMove = false;
        } else
        {
            anim.SetBool("OpenWeaponWheel", false);
            player.GetComponent<PlayerMovement>().canMove = true;
        }

        switch (weaponID)
        {
            case 0: //Fist
                selectedItem.sprite = Fist;
                DisableWeapons();
                break;
            case 1: //Pistol
                selectedItem.sprite = Pistol;
                if (!Weapons[0].activeInHierarchy)
                {
                    DisableWeapons();
                    StartCoroutine(Waittenth());
                    Weapons[0].SetActive(true);
                }
                break;
            case 2: //Minigun
                selectedItem.sprite = Minigun;
                if (!Weapons[1].activeInHierarchy)
                {
                    DisableWeapons();
                    StartCoroutine(Waittenth());
                    Weapons[1].SetActive(true);
                }
                break;
            case 3: //SniperRifle
                selectedItem.sprite = SniperRifle;
                if (!Weapons[2].activeInHierarchy)
                {
                    DisableWeapons();
                    StartCoroutine(Waittenth());
                    Weapons[2].SetActive(true);
                }
                break;
            case 4: //Shotgun
                selectedItem.sprite = Shotgun;
                if (!Weapons[3].activeInHierarchy)
                {
                    DisableWeapons();
                    StartCoroutine(Waittenth());
                    Weapons[3].SetActive(true);
                }
                break;
            case 5: //Scythe
                selectedItem.sprite = Scythe;
                if (!Weapons[4].activeInHierarchy)
                {
                    DisableWeapons();
                    StartCoroutine(Waittenth());
                    Weapons[4].SetActive(true);
                }
                break;
            case 6: //RocketLauncher
                selectedItem.sprite = RocketLauncher;
                if (!Weapons[5].activeInHierarchy)
                {
                    DisableWeapons();
                    StartCoroutine(Waittenth());
                    Weapons[5].SetActive(true);
                }
                break;
            case 7: //MYSTERYITEM
                selectedItem.sprite = MYSTERYITEM;
                if (!Weapons[6].activeInHierarchy)
                {
                    DisableWeapons();
                    StartCoroutine(Waittenth());
                    Weapons[6].SetActive(true);
                }
                break;
        }
    }

    void DisableWeapons()
    {
        for(int i = 0; i < Weapons.Length; i++)
        {
            Weapons[i].SetActive(false);
        }
    }
    void DisableButtons()
    {
        for(int i = 0; i < Buttons.Length; i++)
        {
            ButtonScripts[i].DeSelected();
        }
    }

    void SelectButton(int buttonID)
    {
        ButtonScripts[buttonID].Selected();
    }

    void SelButton(int buttonID)
    {
        Buttons[buttonID].Select();
    }

    void DeselButtons(int buttonID)
    {
        for(int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].Select();
        }
    }

    IEnumerator Waittenth()
    {
        yield return new WaitForSeconds(0.1f);
    }

}
