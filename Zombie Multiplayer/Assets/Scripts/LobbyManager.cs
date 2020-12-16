using Photon.Pun; // 유니티용 포톤 컴포넌트들
using Photon.Realtime; // 포톤 서비스 관련 라이브러리
using UnityEngine;
using UnityEngine.UI;

// 마스터(매치 메이킹) 서버와 룸 접속을 담당
public class LobbyManager : MonoBehaviourPunCallbacks {
    private string gameVersion = "1"; // 게임 버전

    public Text connectionInfoText; // 네트워크 정보를 표시할 텍스트
    public Button joinButton; // 룸 접속 버튼

    // 게임 실행과 동시에 마스터 서버 접속 시도
    private void Start()
    {
        //게임 서버에 접속
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();

        //조인 룸버튼이 눌리지 않게 설정하고 접속 상태를 표시
        joinButton.interactable = false;
        connectionInfoText.text = "오프라인 : 게임 서버에 접속 중입니다....";
        
    }

    // 마스터 서버 접속 성공시 자동 실행
    public override void OnConnectedToMaster()
    {
        //조인 룸버튼을 활성화하고 접속 상태를 표시
        joinButton.interactable = true;
        connectionInfoText.text = "온라인 : 마스터 서버에 접속했습니다.";
        
    }

    // 마스터 서버 접속 실패시 자동 실행
    public override void OnDisconnected(DisconnectCause cause)
    {
        //조인 룸버튼을 활성화하고 접속 상태를 표시
        joinButton.interactable = false;
        connectionInfoText.text = "오프라인 : 마스터 서버와의 접속이 실패하였습니다. 재접속 실행합니다.";

        //재접속 실행
        PhotonNetwork.ConnectUsingSettings();
    }

    // 룸 접속 시도
    public void Connect()
    {
        joinButton.interactable = false;

        if (PhotonNetwork.IsConnected)
        {
            connectionInfoText.text = "온라인 : 룸에 접속 중...";

            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            connectionInfoText.text = "오프라인 : 마스터 서버와의 연결이 끊어져있습니다. 재접속 실행합니다";

            //재접속 실행
            PhotonNetwork.ConnectUsingSettings();
        }
        
    }

    // (빈 방이 없어)랜덤 룸 참가에 실패한 경우 자동 실행
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        connectionInfoText.text = "온라인 : 빈 룸이 없습니다. 새로 생성합니다";

        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
    }

    // 룸에 참가 완료된 경우 자동 실행
    public override void OnJoinedRoom()
    {
        connectionInfoText.text = "방 참가 성공";
        PhotonNetwork.LoadLevel("Main");
        
    }
}