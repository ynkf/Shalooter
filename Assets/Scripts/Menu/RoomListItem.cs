using Photon.Realtime;
using TMPro;
using UnityEngine;

public class RoomListItem : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _roomNameField;

    public RoomInfo roomInfo;
    public void SetUp(RoomInfo room)
    {
        roomInfo = room;
        _roomNameField.text = room.Name;
    }

    public void OnClick()
    {
        Launcher.Instance.JoinRoom(roomInfo);
    }
}
