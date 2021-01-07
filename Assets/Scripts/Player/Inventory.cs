using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    /*
     * This is the Class for the Inventory/Weapon Selector. In the following Class following functions will be treaded:
     * -Disable/Enable selected Weapon
     * -Weapon Selector
     * -Inventory
     * -Healthbar
     * -Ammunationdisplay
     * -Death Screan
     * Colors: 5AFFFD-->Turkis & 000000-->Black Background(A=228)
     */
    //Define Buttons
    public Button BeginButton;
    public Button Set1;
    public Button Set2;
    public Button Set3;
    public Button Set4;
    public Button Set5;

    //Define Canvas
    public GameObject WeaponSelector;
    public GameObject InventoryBar;
    public GameObject DeathScreen;

    //Inventory Text
    public Text InventoryMainWeapon;
    public Text InventorySecondaryWeapon;
    public Text InventoryThrowable;
    public Text Healthbar;
    public Text Ammunition;

    //Weapon Selection Screen Text
    public Text SelectorMainWeapon;
    public Text SelectorSecondaryWeapon;

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
    List<string> SecondaryWeapon = new List<string>() { "M9", "RPG7", "SPAS12"};
    List<string> Throwables = new List<string>() { "" };

    //Player Health
    private int PlayerHealth = 100;

    //Weapon Ammunation
    private string ammunation;


    //integer for telling which weapon is active 0 = main, 1 = secondary, 2 = throwable
    private int activeWeapon = 0;

    // Start is called before the first frame update
    void Start()
    {
        DeactivateAllWeapons();
        InventoryBar.SetActive(false);
        DeathScreen.SetActive(false);
        WeaponSet1();

        //add listener to all the buttons on the Selectorscreen
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
    }

    void OnClickStart()
    {
        //Hide the big weapon selector and show the smaller inventory
        WeaponSelector.SetActive(false);
        InventoryBar.SetActive(true);

        SetInventoryText();
        ActivateMainWeapon();

        Healthbar.text = PlayerHealth.ToString();
        Healthbar.color = Color.green;

        //!!Also call the function for spawing on the map!!
    }

    void WeaponSet1()
    {
        SelectorMainWeapon.text = "M4";
        SelectorSecondaryWeapon.text = "M9";

        MainWeaponName = "M4";
        SecondaryWeaponName = "M9";
        ThrowableName = "";
    }

    void WeaponSet2()
    {
        SelectorMainWeapon.text = "M200";
        SelectorSecondaryWeapon.text = "RPG7";

        MainWeaponName = "M200";
        SecondaryWeaponName = "RPG7";
        ThrowableName = "";
    }

    void WeaponSet3()
    {
        SelectorMainWeapon.text = "MP5";
        SelectorSecondaryWeapon.text = "SPAS12";


        MainWeaponName = "MP5";
        SecondaryWeaponName = "SPAS12";
        ThrowableName = "";
    }

    void WeaponSet4()
    {
        SelectorMainWeapon.text = "PKM";
        SelectorSecondaryWeapon.text = "M9";


        MainWeaponName = "PKM";
        SecondaryWeaponName = "M9";
        ThrowableName = "";
    }

    void WeaponSet5()
    {
        SelectorMainWeapon.text = "M4";
        SelectorSecondaryWeapon.text = "SPAS12";

        MainWeaponName = "M4";
        SecondaryWeaponName = "SPAS12";
        ThrowableName = "";
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
    }

    void ActivateMainWeapon()
    {

        //gameObject.Find(SecondaryWeaponName).SetActive(false);
        // gameObject.Find(ThrowableName).SetActive(false);

        if (MainWeaponName == "M4")
        {
            M4.SetActive(true);
            M4.GetComponent<WeaponScript>().changedWeapon();
        }

        else if (MainWeaponName == "M200")
        {
            M200.SetActive(true);
            M200.GetComponent<WeaponScript>().changedWeapon();

        }

        else if (MainWeaponName == "MP5")
        {
            MP5.SetActive(true);
            MP5.GetComponent<WeaponScript>().changedWeapon();

        }

        else if (MainWeaponName == "PKM")
        {
            PKM.SetActive(true);
            PKM.GetComponent<WeaponScript>().changedWeapon();

        }

        activeWeapon = 0;
        InventoryMainWeapon.color = Color.red;
    }

    void ActivateSecondary()
    { 
        if (SecondaryWeaponName == "RPG7")
        {
            RPG7.SetActive(true);
            RPG7.GetComponent<WeaponScript>().changedWeapon();

        }

        else if (SecondaryWeaponName == "SPAS12")
        {
            SPAS12.SetActive(true);
            SPAS12.GetComponent<WeaponScript>().changedWeapon();

        }

        else if(SecondaryWeaponName == "M9")
        {
            M9.SetActive(true);
            M9.GetComponent<WeaponScript>().changedWeapon();
        }

        activeWeapon = 1;
        InventorySecondaryWeapon.color = Color.red;
    }

    void ActivateThrowable()
    {
       // GameObject.Find(MainWeaponName).SetActive(false);
       // GameObject.Find(SecondaryWeaponName).SetActive(false);

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

    //This is the method for showing how much health the player has left.
    //The parameter is damage. (for ex. The damage he gets is 20 then the parameter int would be 20)
    public void takeDamage(int health)
    {
        PlayerHealth = health;
        //text for health display
        Healthbar.text = PlayerHealth.ToString();
        if( PlayerHealth > 60)
        {
            Healthbar.color = Color.green;
        }
        
        else if(PlayerHealth <= 59 && PlayerHealth > 30)
        {
            Healthbar.color = Color.yellow;
        }

        else
        {
            Healthbar.color = Color.red;
        }
    }
    
    //this is the function to call when the player dies. every weapon deactivates, the inventory bar goes away, and the weapons selector shows up after afew seconds.
    public void die()
    {
        /*
         1. Death Screen
         2. Deactivate all weapons
         3. Deactivate inventory
         4. Wait 3 seconds
         5. Show Weapon Selector*/

        DeathScreen.SetActive(true);
        DeactivateAllWeapons();
        InventoryBar.SetActive(false);
        StartCoroutine(waiter());
        DeathScreen.SetActive(false);
        WeaponSelector.SetActive(true);

    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(3.0f); 
    }

    //This is the Method which will update the ammunation display on the screen while shooting
    public void updateAmmo(int magazine, int bullets)
    {
        ammunation = magazine + "/" + bullets;
        Ammunition.text = ammunation;        
    }
}
