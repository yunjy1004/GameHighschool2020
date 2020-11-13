using System.Collections.Generic;
using UnityEngine;

// 적 게임 오브젝트를 주기적으로 생성
public class EnemySpawner : MonoBehaviour {
    public Enemy enemyPrefab; // 생성할 적 AI

    public Transform[] spawnPoints; // 적 AI를 소환할 위치들

    public float damageMax = 40f; // 최대 공격력
    public float damageMin = 20f; // 최소 공격력

    public float healthMax = 200f; // 최대 체력
    public float healthMin = 100f; // 최소 체력

    public float speedMax = 3f; // 최대 속도
    public float speedMin = 1f; // 최소 속도

    public Color strongEnemyColor = Color.red; // 강한 적 AI가 가지게 될 피부색

    private List<Enemy> enemies = new List<Enemy>(); // 생성된 적들을 담는 리스트
    private int wave; // 현재 웨이브

    private void Update() {
        // 게임 오버 상태일때는 생성하지 않음
        if (GameManager.instance != null && GameManager.instance.isGameover)
        {
            return;
        }

        // 적을 모두 물리친 경우 다음 스폰 실행
        if (enemies.Count <= 0)
        {
            SpawnWave();
        }

        // UI 갱신
        UpdateUI();
    }

    // 웨이브 정보를 UI로 표시
    private void UpdateUI() {
        // 현재 웨이브와 남은 적의 수 표시
        UIManager.instance.UpdateWaveText(wave, enemies.Count);
    }

    // 현재 웨이브에 맞춰 적을 생성
    private void SpawnWave() {
        CreateEnemy(0);
    }

    // 적을 생성하고 생성한 적에게 추적할 대상을 할당
    private void CreateEnemy(float intensity) {

        var spawnPointNum = Random.Range(0, spawnPoints.Length - 1);
        var spawnPoint = spawnPoints[spawnPointNum];

        var enemy = Instantiate<Enemy>(
            enemyPrefab,                                        //스폰할 에너미 프리팹
            spawnPoint.position,                                //스폰할 위치
            Quaternion.LookRotation(spawnPoint.forward));       //스폰시 회전값(spawnPoint)

        //10%활률로 강화된 좀비가 소환(빨강 좀비는 3배 빠르고 3배 단단하며 3배 데미지가 높다)
        if (Random.Range(0, 100f) <= 10f)
        {
            enemy.Setup(
            Random.Range(healthMin, healthMax) * 3,
            Random.Range(damageMin, damageMax) * 3,
            Random.Range(speedMin, speedMax) * 3,
            strongEnemyColor);

        }
        //그 외에는 일반 좀비가 소환
        else
        {
            enemy.Setup(
            Random.Range(healthMin, healthMax),
            Random.Range(damageMin, damageMax),
            Random.Range(speedMin, speedMax),
            Color.white);
        }

        enemies.Add(enemy);
    }
}