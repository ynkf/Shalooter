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

    //Inventory Text
    public Text InventoryMainWeapon;
    public Text InventorySecondaryWeapon;
    public Text InventoryThrowable;

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

    //int for telling which weapon is active 0 = main, 1 = secondary, 2 = throwable
    private int activeWeapon = 0;

    // Start is called before the first frame update
    void Start()
      {

        DeactivateAllWeapons();
        InventoryBar.SetActive(false);

        //add listener on the button
        Button btn = BeginButton.GetComponent<Button>();
        btn.onClick.AddListener(OnClickStart);

        PopulateLists();
        //add more function for the preparations of the weapons

      }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            DeactivateActiveWeapon();
            ActivateMainWeapon();
        }

        if (Input.GetKeyDown("2"))
        {
            DeactivateActiveWeapon();
            ActivateSecondary();
        }

        if (Input.GetKeyDown("3"))
        {
            DeactivateActiveWeapon();
            ActivateThrowable();
        }
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

        WeaponSelector.SetActive(false);
        InventoryBar.SetActive(true);

        SetInventoryText();
        ActivateMainWeapon();

    }

    void SetInventoryText()
    {
        InventoryMainWeapon.text = MainWeaponName;
        InventorySecondaryWeapon.text = SecondaryWeaponName;
        InventoryThrowable.text = ThrowableName;
    }

    void DeactivateAllWeapons()
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
    }

    void ActivateMainWeapon()
    {

        //GameObject.Find(SecondaryWeaponName).SetActive(false);
        //GameObject.Find(ThrowableName).SetActive(false);

        if (MainWeaponName == "M4")
        {
            M4.SetActive(true);
        }

        else if (MainWeaponName == "M200")
        {
            M200.SetActive(true);
        }

        else if (MainWeaponName == "MP5")
        {
            MP5.SetActive(true);
        }

        else if (MainWeaponName == "PKM")
        {
            PKM.SetActive(true);
        }

        activeWeapon = 0;
    }

    void ActivateSecondary()
    {
       // GameObject.Find(MainWeaponName).SetActive(false);
       // GameObject.Find(ThrowableName).SetActive(false);

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

        else if(SecondaryWeaponName == "M9")
        {
            M9.SetActive(true);
        }

        activeWeapon = 1;
    }

    void ActivateThrowable()
    {
       // GameObject.Find(MainWeaponName).SetActive(false);
       // GameObject.Find(SecondaryWeaponName).SetActive(false);

        if (ThrowableName == "Grenade")
        {
            F1.SetActive(true);
        }

        activeWeapon = 2;
    }

    void DeactivateActiveWeapon()
    {
        if(activeWeapon == 0)
        {
            GameObject.Find(MainWeaponName).SetActive(false);
        }

        else if(activeWeapon == 1)
        {
            GameObject.Find(SecondaryWeaponName).SetActive(false);

        }

        else if(activeWeapon == 2)
        {
            GameObject.Find(ThrowableName).SetActive(false);

        }
    }
}
