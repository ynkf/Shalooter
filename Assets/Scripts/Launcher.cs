using Photon.Pun;
using TMPro;
using UnityEngine;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TMP_InputField _roomNameInputField;
    [SerializeField]
    private TMP_Text _errorText;
    [SerializeField]
    private TMP_Text _roomNameText;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        MenuManager.Instance.OpenMenu(MenuNames.TitleMenu);
    }

    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenu(MenuNames.RoomMenu);
        _roomNameText.text = PhotonNetwork.CurrentRoom.Name;
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        _errorText.text = "Room Creation Failed: " + message;
        MenuManager.Instance.OpenMenu(MenuNames.ErrorMenu);
    }

    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu(MenuNames.TitleMenu);
    }

    public void CreateRoom()
    {
        var roomName = _roomNameInputField.text;

        if (string.IsNullOrEmpty(roomName))
        {
            return;
        }

        PhotonNetwork.CreateRoom(roomName);

        MenuManager.Instance.OpenMenu(MenuNames.LoadingMenu);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();

        MenuManager.Instance.OpenMenu(MenuNames.LoadingMenu);
    }
}
