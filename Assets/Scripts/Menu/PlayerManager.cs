using Photon.Pun;
using System.IO;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PhotonView _photonView;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (_photonView.IsMine)
        {
            CreateController();
        }
    }

    private void CreateController()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), new Vector3(1, 0, 1), Quaternion.identity);
    }
}
