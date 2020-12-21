using Photon.Pun;
using UnityEngine;

// 플레이어 캐릭터를 사용자 입력에 따라 움직이는 스크립트
public class PlayerMovement : MonoBehaviourPun {
    public float moveSpeed = 5f; // 앞뒤 움직임의 속도
    public float rotateSpeed = 180f; // 좌우 회전 속도

    private Animator playerAnimator; // 플레이어 캐릭터의 애니메이터
    private PlayerInput playerInput; // 플레이어 입력을 알려주는 컴포넌트
    private Rigidbody playerRigidbody; // 플레이어 캐릭터의 리지드바디

    private void Start() {
        // 사용할 컴포넌트들의 참조를 가져오기
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }

    // FixedUpdate는 물리 갱신 주기에 맞춰 실행됨
    private void FixedUpdate() {
        // 로컬 플레이어만 직접 위치와 회전을 변경 가능
        if (!photonView.IsMine)
        {
            return;
        }

        // 회전 실행
        Rotate();
        // 움직임 실행
        Move();

        // 입력값에 따라 애니메이터의 Move 파라미터 값을 변경
        playerAnimator.SetFloat("Move", playerInput.move + playerInput.rotate);
    }

    // 입력값에 따라 캐릭터를 앞뒤로 움직임
    private void Move() {

        var forward = Camera.main.transform.forward;
        forward.y = 0;
        forward.Normalize();

        var right = Camera.main.transform.right;
        right.y = 0;
        right.Normalize();

        var rigidbody = GetComponent<Rigidbody>();

        Vector3 v = new Vector3();

        v += forward * moveSpeed * playerInput.move;

        v += right * moveSpeed * playerInput.rotate;

        rigidbody.velocity = v;
        return;
    }

    // 입력값에 따라 캐릭터를 좌우로 회전
    private void Rotate() {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, 0);
        float enter;
        plane.Raycast(ray, out enter);
        var point = ray.GetPoint(enter);
        transform.LookAt(point);
        var eulerAngles = transform.eulerAngles;
        eulerAngles.x = 0;
        eulerAngles.z = 0;
        transform.eulerAngles = eulerAngles;

        return;
    }
}