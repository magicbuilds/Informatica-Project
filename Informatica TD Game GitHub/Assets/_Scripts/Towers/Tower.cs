using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    [Header("Attribute")] public TowerSO currentTower;
    [SerializeField] private float rotateSpeed = 200f;

    [Header("References")]
    [SerializeField] public Transform firingPoint;
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button upgradeButton;

    public TileScript tile;

    [Header("DifferentTowers")]
    [SerializeField] private bool hasRandomEnemy;
    [SerializeField] private GameObject knifeBulletPrefab;
    [SerializeField] private GameObject customerPrefab;
    [SerializeField] private GameObject discountTicketPrefab;

    [Header("Other")]
    private Transform target;
    private float timeUntilFire;
    private int level = 0;

    public EnemySO currentEnemy;
    
    public float currentRange;
    public float currentFireRate;
    public float currentBulletSpeed;
    public float currentDamage;

    private bool hasSpawnedCustomer = false;
    private CustomerAI spawnedCustomerAI;
    
    private void Start()
    {
        UpgradeTower();

        upgradeButton.onClick.AddListener(UpgradeTower);
    }

    private void Update()
    {
        timeUntilFire += Time.deltaTime;

        currentDamage = currentTower.baseDamage;

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

        switch (currentTower.towerType)
        {
            case TowerSO.towers.KnifeThrower:
                KnifeThrower();
                break;
            case TowerSO.towers.DiscountGun:
                DiscountGun();
                break;
            case TowerSO.towers.Blade:
                break;
            case TowerSO.towers.DustShooter:
                break;
            case TowerSO.towers.Railgun:
                break;
            case TowerSO.towers.Speaker:
                break;
            case TowerSO.towers.Shelf:
                break;
            case TowerSO.towers.AirConditioner:
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

        }

        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, currentRange, (Vector2)
            transform.position, 0f, enemyMask);
        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= currentRange;
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

        tile.UpdateRange();

        CloseUpgradeUI();
    }

    private float CalcFireRate()
    {
        return currentTower.baseFireRate * Mathf.Pow(level, 0.7f);
    }

    private float CalcRange()
    {
        return  Mathf.RoundToInt(currentTower.baseRange * Mathf.Pow(level, 0.4f));
    }

    private void KnifeThrower()
    {
        RotateTowardsTarget();
        currentDamage = currentTower.baseDamage;

        if (timeUntilFire >= 1f / currentFireRate)
        {
            GameObject bullet = Instantiate(knifeBulletPrefab, firingPoint.position, Quaternion.identity);
            BulletScript bulletScript = bullet.GetComponent<BulletScript>();
            bulletScript.SetTarget(target);

            bulletScript.tower = this;
            

            timeUntilFire = 0f;
        }
    }

    private void DiscountGun()
    {
        RotateTowardsTarget();
        currentDamage = currentTower.baseDamage * currentEnemy.baseHealth;

        if (timeUntilFire >= 1f / currentFireRate)
        {
            
            GameObject bullet = Instantiate(discountTicketPrefab, firingPoint.position, Quaternion.identity);
            BulletScript bulletScript = bullet.GetComponent<BulletScript>();
            bulletScript.SetTarget(target);

            bulletScript.tower = this;
            

            timeUntilFire = 0f;
        }
    }

    private void Checkout()
    {
        if (!hasSpawnedCustomer)
        {
            GameObject spawnedCustomer = Instantiate(customerPrefab, firingPoint.position, Quaternion.identity);
            spawnedCustomerAI = spawnedCustomer.GetComponent<CustomerAI>();
            spawnedCustomerAI.tower = this;

            EnemyStats targetEnemyStats = target.GetComponent<EnemyStats>();
            if (!targetEnemyStats.isTarget)
            {
                spawnedCustomerAI.SetNewTarget(target);
            }

            hasSpawnedCustomer = true;
        }

        if (Vector2.Distance(firingPoint.position, spawnedCustomerAI.transform.position) < 0.25f)
        {
            if (spawnedCustomerAI.target != target)
            {
                EnemyStats targetEnemyStats = target.GetComponent<EnemyStats>();
                if (!targetEnemyStats.isTarget)
                {
                    spawnedCustomerAI.SetNewTarget(target);
                }
            }
        }
    }
}
