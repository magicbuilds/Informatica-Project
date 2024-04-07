using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Tower : MonoBehaviour
{
    [Header("Attribute")] public TowerSO currentTower;
    [SerializeField] private float rotateSpeed = 200f;
  

    [Header("References")] 
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button upgradeButton;

    private float bpsBase;
    private int targetingRangeBase;


    private Transform target;
    private float timeUntilFire;
    private int level = 1;
    
    private void Start()
    {
        bpsBase = currentTower.baseBulletSpeed;
        targetingRangeBase = currentTower.baseRange;
        
        upgradeButton.onClick.AddListener(UpgradeTower);
    }
    private void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }

        RotateTowardsTarget();

        if (!CheckTargetIsInRange())
        {
            target = null;
        }
        else
        {
            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / currentTower.baseBulletSpeed)
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        BulletScript bulletScript = bullet.GetComponent<BulletScript>();
        bulletScript.SetTarget(target);

        bulletScript.tower = this;
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, currentTower.baseRange, (Vector2)
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
        return Vector2.Distance(target.position, transform.position) <= currentTower.baseRange;
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

        currentTower.baseBulletSpeed = CalcBPS();
        currentTower.baseRange = Mathf.RoundToInt(CalcRange());
        
        CloseUpgradeUI();
    }

    private float CalcBPS()
    {
        return bpsBase * Mathf.Pow(level, 0.7f);
    }

    private float CalcRange()
    {
        return  Mathf.RoundToInt(targetingRangeBase * Mathf.Pow(level, 0.4f));
    }

    private void OnDrawGizmosSelected()
    {
        //Handles.color = Color.cyan;
        //Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}
