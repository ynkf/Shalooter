using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
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
    [SerializeField]
    private Transform _roomListContent;
    [SerializeField]
    private GameObject _roomListItemPrefab;
    [SerializeField]
    private Transform _playerListContent;
    [SerializeField]
    private GameObject _playerListItemPrefab;
    [SerializeField]
    private GameObject _startGameButton;


    public static Launcher Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        MenuManager.Instance.OpenMenu(MenuNames.TitleMenu);
        PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString();
    }

    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenu(MenuNames.RoomMenu);
        _roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        foreach (Transform child in _playerListContent)
        {
            Destroy(child.gameObject);
        }

        foreach (var player in PhotonNetwork.PlayerList)
        {
            Instantiate(_playerListItemPrefab, _playerListContent).GetComponent<PlayerListItem>().SetUp(player);
        }

        _startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        _startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        _errorText.text = "Room Creation Failed: " + message;
        MenuManager.Instance.OpenMenu(MenuNames.ErrorMenu);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(_playerListItemPrefab, _playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }

    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu(MenuNames.TitleMenu);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform transform in _roomListContent)
        {
            Destroy(transform.gameObject);
        }

        foreach (var room in roomList)
        {
            if (!room.RemovedFromList)
            {
                Instantiate(_roomListItemPrefab, _roomListContent).GetComponent<RoomListItem>().SetUp(room);
            }
        }
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

    public void JoinRoom(RoomInfo roomInfo)
    {
        PhotonNetwork.JoinRoom(roomInfo.Name);

        MenuManager.Instance.OpenMenu(MenuNames.LoadingMenu);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();

        MenuManager.Instance.OpenMenu(MenuNames.LoadingMenu);
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(SceneIndexes.FactoryMapScene);
    }
}
