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

    //Define MainWeapons
    public GameObject M4;
    public GameObject M200;
    public GameObject MP5;
    public GameObject PKM;

    //Define SecondaryWeapons
    public GameObject Knife;
    public GameObject M9;
    public GameObject RPG7;
    public GameObject SPAS12;

    //Define Throwables
    public GameObject F1;

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
        foreach (string weapon in MainWeapons)
        {
            GameObject.Find(weapon).SetActive(false);
        }
        foreach (string weapon in SecondaryWeapon)
        {
            GameObject.Find(weapon).SetActive(false);
        }
        foreach (string weapon in Throwables)
        {
            GameObject.Find(weapon).SetActive(false);
        }

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

        ActivateWeapon();

    }

    void HideUI()
    {
        WeaponSelector.SetActive(false);
    }

    void ActivateWeapon()
    {
        //Activate Main Weapon
        if(MainWeaponName == "M4")
        {
            M4.SetActive(true);
        }

        else if(MainWeaponName == "M200")
        {
            M200.SetActive(true);
        }

        else if(MainWeaponName == "MP5")
        {
            MP5.SetActive(true);
        }

        else if(MainWeaponName == "PKM")
        {
            PKM.SetActive(true);
        }

        //Activate Secondary Weapon
        if (SecondaryWeaponName == "Knife")
        {
            Knife.SetActive(true);
        }

        else if (SecondaryWeaponName == "RPG7")
        {
            RPG7.SetActive(true);
        }

        else if (SecondaryWeaponName == "SPAS12")
        {
            SPAS12.SetActive(true);
        }

        //Activate Throwable
        if(ThrowableName == "Grenade")
        {
            F1.SetActive(true);
        }
    }


}
