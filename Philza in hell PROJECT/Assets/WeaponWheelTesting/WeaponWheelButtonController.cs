using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponWheelButtonController : MonoBehaviour
{

    public int ID;
    private Animator anim;
    public string itemName;
    public TextMeshProUGUI itemText;
    public Image selectedItem;
    private bool selected = false;
    public Sprite Icon;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (selected)
        {
            selectedItem.sprite = Icon;
            itemText.text = itemName;
        } 
    }

    public void Selected()
    {
        selected = true;
        WeaponWheelController.weaponID = ID;
    }
    public void DeSelected()
    {
        selected = false;
    }

    public void HoverEnter()
    {
        anim.SetBool("Hover", true);
        itemText.text = itemName;
    }
    public void HoverExit()
    {
        anim.SetBool("Hover", false);
        itemText.text = "";
    }

}
