using System.Collections;
using UnityEngine;
using UnityEngine.AI; // AI, 내비게이션 시스템 관련 코드를 가져오기

// 적 AI를 구현한다
public class Enemy : LivingEntity {
    public LayerMask whatIsTarget; // 추적 대상 레이어

    private LivingEntity targetEntity; // 추적할 대상
    private NavMeshAgent pathFinder; // 경로계산 AI 에이전트

    public ParticleSystem hitEffect; // 피격시 재생할 파티클 효과
    public AudioClip deathSound; // 사망시 재생할 소리
    public AudioClip hitSound; // 피격시 재생할 소리

    private Animator enemyAnimator; // 애니메이터 컴포넌트
    private AudioSource enemyAudioPlayer; // 오디오 소스 컴포넌트
    private Renderer enemyRenderer; // 렌더러 컴포넌트

    public float damage = 20f; // 공격력
    public float timeBetAttack = 0.5f; // 공격 간격
    private float lastAttackTime; // 마지막 공격 시점

    // 추적할 대상이 존재하는지 알려주는 프로퍼티
    private bool hasTarget
    {
        get
        {
            // 추적할 대상이 존재하고, 대상이 사망하지 않았다면 true
            if (targetEntity != null && !targetEntity.dead)
            {
                return true;
            }

            // 그렇지 않다면 false
            return false;
        }
    }

    private void Awake() {
        // 초기화
        enemyAnimator = GetComponent<Animator>();
        enemyAudioPlayer = GetComponent<AudioSource>();
        enemyRenderer = GetComponent<Renderer>();

        pathFinder = GetComponent<NavMeshAgent>();
    }

    // 적 AI의 초기 스펙을 결정하는 셋업 메서드
    public void Setup(float newHealth, float newDamage, float newSpeed, Color skinColor) {
    }

    private void Start() {
        // 게임 오브젝트 활성화와 동시에 AI의 추적 루틴 시작
        StartCoroutine(UpdatePath());
    }

    private void Update() {
        // 추적 대상의 존재 여부에 따라 다른 애니메이션을 재생
        enemyAnimator.SetBool("HasTarget", hasTarget);

        //추격
        //if(targetEntity != null)
        //{
        //    var offset = targetEntity.transform.position - transform.position;
        //    var direction = offset.normalized;

        //    transform.position += direction * 1f * Time.deltaTime;
        //}
    }

    // 주기적으로 추적할 대상의 위치를 찾아 경로를 갱신
    private IEnumerator UpdatePath() {
        // 살아있는 동안 무한 루프
        while (!dead)
        {
            // 0.25초 주기로 처리 반복
            yield return new WaitForSeconds(0.25f);

            //타격 검색
            var player = FindObjectOfType<PlayerHealth>();
            targetEntity = player;
            if(targetEntity != null)
                pathFinder.SetDestination(targetEntity.transform.position);
            else if(targetEntity.dead || targetEntity == null)
                pathFinder.SetDestination(transform.position);
        }
    }

    // 데미지를 입었을때 실행할 처리
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal) {
        // LivingEntity의 OnDamage()를 실행하여 데미지 적용
        base.OnDamage(damage, hitPoint, hitNormal);

        var effect = hitEffect;
        effect.transform.position = hitPoint;
        effect.transform.forward = hitNormal;
        effect.GetComponent<ParticleSystem>().Play();

        if (!dead)
        {
            enemyAudioPlayer.clip = hitSound;
            enemyAudioPlayer.Play();
        }
    }

    // 사망 처리
    public override void Die() {
        // LivingEntity의 Die()를 실행하여 기본 사망 처리 실행
        base.Die();

        enemyAudioPlayer.clip = deathSound;
        enemyAudioPlayer.Play();

        //사망 애니메이션 동작.
        enemyAnimator.SetTrigger("Die");

        //임시 : 플레이어의 공격이 사망시 관통할 수 있게
        //충돌체를 제거하고,
        //충돌체 제거시 중력에 의해 떨어지는 현상을 제거함.
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().enabled = false;

        /*추가*/ GameManager.instance.AddScore(100);
        FindObjectOfType<EnemySpawner>().RemoveEnemy(this);
    }

    private void OnTriggerStay(Collider other) 
    {
        // 트리거 충돌한 상대방 게임 오브젝트가 추적 대상이라면 공격 실행

        //만약, other.attachedRigidbody가 없다면, 
        //동작하지 않음
        if (other.attachedRigidbody == null)
            return;

        //other.attachedRigidbody에 //LivingEntity가 targetEntity와 같다면,
        LivingEntity livingEntity 
            = other.attachedRigidbody.GetComponent<LivingEntity>();

        //5.번 정답
        if (livingEntity.dead)
            return;

        if (dead)
            return;
            
        if (livingEntity != null && livingEntity == targetEntity)
        {
            //공격할 타이밍인지 체크//만약 공격 딜레이가 아니라면 동작을 호출함. //마지막 공격 시간 + 공격 딜레이의 시간보다 //현재 시간이 더 나중이면 공격
            if(lastAttackTime + timeBetAttack <= Time.time)
            {
                //공격
                Vector3 hitPosition 
                    = livingEntity.transform.position + Vector3.up;

                Vector3 hitDirection
                    = transform.position - livingEntity.transform.position;

                livingEntity.OnDamage(damage, hitPosition, hitDirection);

                lastAttackTime = Time.time;
            }
        }
    }
}