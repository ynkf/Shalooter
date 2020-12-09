using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    //Define Dropdowns
    public Dropdown MainWeaponDropdown;
    public Dropdown SecondaryWeaponDropdown;
    public Dropdown ThrowableDropdown;

    //Define Buttons
    public Button BeginButton;

    //Define Canvas
    public GameObject WeaponSelector;
    public GameObject InventoryBar;

    //Weapon name
    private string MainWeaponName;
    private string SecondaryWeaponName;
    private string ThrowableName;

    List<string> MainWeapons = new List<string>() { "M4", "M200", "MP5", "PKM" };
    List<string> SecondaryWeapon = new List<string>() { "M9", "RPG7", "SPAS12", "Knife" };
    List<string> Throwables = new List<string>() { "Grenade" };


    // Start is called before the first frame update
    void Start()
      {
        //maybe disable movement that the user can select the weapons in "peace"

        //add listener on the button
        Button btn = BeginButton.GetComponent<Button>();
        btn.onClick.AddListener(OnClickStart);

        PopulateLists();
        //add more function for the preparations of the weapons

      }

    // Update is called once per frame
    void Update()
    {
        
    }      

    void PopulateLists()
    {
        MainWeaponDropdown.AddOptions(MainWeapons);
        SecondaryWeaponDropdown.AddOptions(SecondaryWeapon);
        ThrowableDropdown.AddOptions(Throwables);
    }

    void OnClickStart()
    {
        MainWeaponName = MainWeaponDropdown.options[MainWeaponDropdown.value].text;
        SecondaryWeaponName = SecondaryWeaponDropdown.options[SecondaryWeaponDropdown.value].text;
        ThrowableName = ThrowableDropdown.options[ThrowableDropdown.value].text;
        print(MainWeaponName + SecondaryWeaponName + ThrowableName);

        //Hide the big weapon selector and show the smaller inventory
        HideUI();
    }

    void HideUI()
    {
        WeaponSelector.SetActive(false);
    }


}
