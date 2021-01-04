using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    //Define Buttons
    public Button BeginButton;
    public Button Set1;
    public Button Set2;
    public Button Set3;
    public Button Set4;
    public Button Set5;

    public Dropdown MainWeaponDropdown;
    public Dropdown SecondaryWeaponDropdown;
    public Dropdown ThrowableDropdown;

    //Define Canvas
    public GameObject WeaponSelector;
    public GameObject InventoryBar;

    //Inventory Text
    public Text InventoryMainWeapon;
    public Text InventorySecondaryWeapon;
    public Text InventoryThrowable;

    //Weapon Selection Screen Text
    public Text SelectorMainWeapon;
    public Text SelectorSecondaryWeapon;
    public Text SelectorThorwable;

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


    //integer for telling which weapon is active 0 = main, 1 = secondary, 2 = throwable
    private int activeWeapon = 0;

    // Start is called before the first frame update
    void Start()
      {

        DeactivateAllWeapons();
        InventoryBar.SetActive(false);
        WeaponSet1();

        //add listener on the button
        Button btnStart = BeginButton.GetComponent<Button>();
        Button btnSet1 = Set1.GetComponent<Button>();
        Button btnSet2 = Set2.GetComponent<Button>();
        Button btnSet3 = Set3.GetComponent<Button>();
        Button btnSet4 = Set4.GetComponent<Button>();
        Button btnSet5 = Set5.GetComponent<Button>();
        btnStart.onClick.AddListener(OnClickStart);
        btnSet1.onClick.AddListener(WeaponSet1);
        btnSet2.onClick.AddListener(WeaponSet2);
        btnSet3.onClick.AddListener(WeaponSet3);
        btnSet4.onClick.AddListener(WeaponSet4);
        btnSet5.onClick.AddListener(WeaponSet5);
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

    void OnClickStart()
    {     
        //Hide the big weapon selector and show the smaller inventory
        WeaponSelector.SetActive(false);
        InventoryBar.SetActive(true);

        SetInventoryText();
        ActivateMainWeapon();
    }

    void WeaponSet1()
    {
        SelectorMainWeapon.text         = "M4";
        SelectorSecondaryWeapon.text    = "M9";
        SelectorThorwable.text          = "Grenade";

        MainWeaponName = "M4";
        SecondaryWeaponName = "M9";
        ThrowableName = "Grenade";                
    }

    void WeaponSet2()
    {
        SelectorMainWeapon.text = "M200";
        SelectorSecondaryWeapon.text = "RPG7";
        SelectorThorwable.text = "Grenade";

        MainWeaponName = "M200";
        SecondaryWeaponName = "RPG7";
        ThrowableName = "Grenade";
    }

    void WeaponSet3()
    {
        SelectorMainWeapon.text = "MP5";
        SelectorSecondaryWeapon.text = "SPAS12";
        SelectorThorwable.text = "Grenade";

        MainWeaponName = "MP5";
        SecondaryWeaponName = "SPAS12";
        ThrowableName = "Grenade";
    }

    void WeaponSet4()
    {
        SelectorMainWeapon.text = "PKM";
        SelectorSecondaryWeapon.text = "Knife";
        SelectorThorwable.text = "Grenade";

        MainWeaponName = "PKM";
        SecondaryWeaponName = "Knife";
        ThrowableName = "Grenade";
    }

    void WeaponSet5()
    {
        SelectorMainWeapon.text = "M4";
        SelectorSecondaryWeapon.text = "SPAS12";
        SelectorThorwable.text = "Grenade";

        MainWeaponName = "M4";
        SecondaryWeaponName = "SPAS12";
        ThrowableName = "Grenade";
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
        InventoryMainWeapon.color = Color.red;
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
        InventorySecondaryWeapon.color = Color.red;
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
        InventoryThrowable.color = Color.red;
    }

    void DeactivateActiveWeapon()
    {
        if(activeWeapon == 0)
        {
            GameObject.Find(MainWeaponName).SetActive(false);
            InventoryMainWeapon.color = Color.white;
        }

        else if(activeWeapon == 1)
        {
            GameObject.Find(SecondaryWeaponName).SetActive(false);
            InventorySecondaryWeapon.color = Color.white;

        }

        else if(activeWeapon == 2)
        {
            GameObject.Find(ThrowableName).SetActive(false);
            InventoryThrowable.color = Color.white;

        }
    }
}
