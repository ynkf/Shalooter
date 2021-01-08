using UnityEngine;

public class Menu : MonoBehaviour
{
    public string menuName;
    public bool isOpen;

    public void Open()
    {
        gameObject.SetActive(true);
        isOpen = true;
    }

    public void Close()
    {
        gameObject.SetActive(false);
        isOpen = false;
    }
}
