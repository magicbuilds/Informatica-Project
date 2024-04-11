using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    [Header("AllTowers")]
    [SerializeField] private GameObject ammoPrefab;
    [SerializeField] private float rotateSpeed = 200f;
    [SerializeField] public List<Transform> firingPoints;

    [Header("DifferentTowers")]
    [SerializeField] private Transform rotationPoint;

    [SerializeField] private bool hasRandomEnemy;
    [SerializeField] private bool hitsAllEnemiesInRange;

    [Header("Checkout")]
    public float waitTimeAtCheckout;
    public float peopleAtCheckout = 1;

    [Header("Shelf")]
    public float bombTargetingRange;

    [Header("UpgradeUI")]
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button upgradeButton;

    [Header("Other")]
    public TowerSO currentTower;
    public TileScript tile;
    [SerializeField] private LayerMask enemyMask;

    public List<EnemyStats> targets;
    public EnemyStats target;

    private float timeUntilFire;
    private int level = 0;
    
    public float currentRange;
    public float currentFireRate;
    public float currentBulletSpeed;
    public float currentDamage;
    
    private void Start()
    {
        UpgradeTower();

        upgradeButton.onClick.AddListener(UpgradeTower);

        switch (currentTower.towerType)
        {
            case TowerSO.towers.Shelf:
                bombTargetingRange = UpgradeManager.Instance.baseShelfBombTargetingRange;
                break;
        }
    }

    private void Update()
    {
        timeUntilFire += Time.deltaTime;

        if (target == null)
        {
            FindTarget();
            return;
        }

        if (!CheckTargetIsInRange())
        {
            target = null;
            return;
        }

        if (rotationPoint != null)
        {
            RotateTowardsTarget();
        }

        switch (currentTower.towerType)
        {
            case TowerSO.towers.KnifeThrower:
                ShootingTower();
                break;
            case TowerSO.towers.DiscountGun:
                currentDamage = currentTower.baseDamage * target.health;
                ShootingTower();
                break;
            case TowerSO.towers.Blade:
                break;
            case TowerSO.towers.DustShooter:
                ShootingTower();
                break;
            case TowerSO.towers.Railgun:
                ShootingTower();
                break;
            case TowerSO.towers.Speaker:
                SpamTower();
                break;
            case TowerSO.towers.Shelf:
                BombingTower();
                break;
            case TowerSO.towers.AirConditioner:
                SlowTower();
                break;
            case TowerSO.towers.Checkout:
                Checkout();
                break;
            default:
                Debug.Log("Tower has not been defined");
                break;
        }
    }

    private void FindTarget()
    {
        if (hasRandomEnemy)
        {
            if (EnemyManager.Instance.enemiesLeft.Count <= 0) return;

            if (target == null)
            {
                int randomIndex = Random.Range(0, EnemyManager.Instance.enemiesLeft.Count);
                target = EnemyManager.Instance.enemiesLeft[randomIndex];
                if (Vector2.Distance(target.transform.position, transform.position) <= currentRange)
                {
                    if (currentTower.towerType == TowerSO.towers.Checkout && target.currentEnemy.isBoss == true)
                    {
                        target = null;
                    }

                    if (target.isTarget || target.isDead)
                    {
                        target = null;
                    }
                }
            }
            return;
        }

        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, currentRange, (Vector2)
            transform.position, 0f, enemyMask);
        if (hits.Length > 0)
        {
            target = hits[0].transform.GetComponent<EnemyStats>();

            if (hitsAllEnemiesInRange)
            {
                targets.Clear();

                foreach (RaycastHit2D hit in hits)
                {
                    targets.Add(hit.transform.GetComponent<EnemyStats>());
                }
            }
        }
    }

    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        rotationPoint.rotation = Quaternion.RotateTowards(rotationPoint.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.transform.position, transform.position) <= currentRange;
    }
    
    public void OpenUpgradeUI()
    {
        upgradeUI.SetActive(true);
    }

    public void CloseUpgradeUI()
    {
        upgradeUI.SetActive(false);
        UIManager.Instance.SetHoveringState(false);
    }

    private void ShootingTower()
    {
        if (timeUntilFire >= 1f / currentFireRate)
        {
            ShootBullet();

            timeUntilFire = 0f;
        }
    }

    private void BombingTower()
    {
        if (timeUntilFire >= 1f / currentFireRate)
        {
            ShootBomb();

            timeUntilFire = 0f;
        }
    }

    private void SpamTower()
    {
        if (timeUntilFire >= 1f / currentFireRate)
        {
            ShootMultipleBullets();

            timeUntilFire = 0f;
        }
    }

    private void SlowTower()
    {
        foreach (EnemyStats enemy in targets)
        {
            enemy.SlowEnemy(currentDamage);
        }
    }

    private void ShootBullet()
    {
        GameObject bullet = Instantiate(ammoPrefab, firingPoints[0].position, Quaternion.identity);
        BulletScript bulletScript = bullet.GetComponent<BulletScript>();

        bulletScript.tower = this;
    }

    private void ShootBomb()
    {
        GameObject bomb = Instantiate(ammoPrefab, firingPoints[0].position, Quaternion.identity);
        BombScript bombScript = bomb.GetComponent<BombScript>();

        bombScript.tower = this;
    }

    private void ShootMultipleBullets()
    {
        foreach (Transform firingPoint in firingPoints)
        {
            GameObject bullet = Instantiate(ammoPrefab, firingPoint.position, firingPoint.rotation);
            SpamBulletScript bulletScript = bullet.GetComponent<SpamBulletScript>();

            bulletScript.tower = this;
        }
    }

    public void UpgradeTower()
    {
        level++;
        if (level >= 10)
        {
            level = 10;
        }

        currentFireRate = CalcFireRate();
        currentRange = Mathf.RoundToInt(CalcRange());
        currentBulletSpeed = currentTower.baseBulletSpeed;
        currentDamage = currentTower.baseDamage;

        tile.UpdateRange();

        CloseUpgradeUI();
    }

    private float CalcFireRate()
    {
        return currentTower.baseFireRate * Mathf.Pow(level, 0.7f);
    }

    private float CalcRange()
    {
        return Mathf.RoundToInt(currentTower.baseRange * Mathf.Pow(level, 0.4f));
    }

    private void Checkout()
    {
        if (peopleAtCheckout <= UpgradeManager.Instance.maxPeopleAtCheckout && target != null)
        {
            GameObject spawnedCustomer = Instantiate(ammoPrefab, firingPoints[0].position, Quaternion.identity);
            CustomerAI spawnedCustomerAI = spawnedCustomer.GetComponent<CustomerAI>();
            spawnedCustomerAI.tower = this;

            peopleAtCheckout++;
        }
    }
}
