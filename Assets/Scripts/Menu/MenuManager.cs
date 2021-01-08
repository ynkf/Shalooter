using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private Menu[] _menus;

    public static MenuManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void OpenMenu(string menuName)
    {
        foreach (var menu in _menus)
        {
            if (menu.menuName == menuName)
            {
                menu.Open();
            }
            else if (menu.isOpen)
            {
                CloseMenu(menu);
            }
        }
    }

    public void OpenMenu(Menu menu)
    {
        foreach (var m in _menus)
        {
            if (m.isOpen)
            {
                CloseMenu(m);
            }
        }

        menu.Open();
    }

    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }
}
