using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liquid : MonoBehaviour
{
    public float spreadDelay = 1.0f;     public int maxFlowDistance = 3;
    public int currentFlowDistance = 0;
    [SerializeField] GameObject prefab;
    public LayerMask blockLayer;
    public float scaleDecreaseY = 0.25f;
    public float minYScale = 0.1f;
    public Vector3 initialScale;
    [SerializeField] private GameObject cobbleStone;
    private static HashSet<Vector3> visitedPositions = new HashSet<Vector3>();
    public bool isGenerator = true;
    public bool isSource = true;
    private bool isFlowing = false;
    public bool isFalling;

        public class GeneratedLiquidData
    {
        public GameObject liquidObject;
        public float creationTime;

        public GeneratedLiquidData(GameObject obj, float time)
        {
            liquidObject = obj;
            creationTime = time;
        }
    }

        public List<GeneratedLiquidData> generatedLiquids = new List<GeneratedLiquidData>();

        private Coroutine damageCoroutine;
    public LayerMask checkFallLayer;

    private void OnEnable()
    {
                initialScale = transform.localScale;

                if (isGenerator && !isFlowing)
        {
            StartCoroutine(SpreadWithDelay());         }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Lava") && other.CompareTag("Water"))
        {
            Debug.Log("Cobblestone created");
            Instantiate(cobbleStone, transform.position, Quaternion.identity);

            isFlowing = false;
        }
        
        if (other.CompareTag("Player") && gameObject.CompareTag("Lava"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(DamagePlayer(playerHealth));
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == checkFallLayer)
        {
            Destroy(gameObject);
            print("Destr");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && gameObject.CompareTag("Lava"))
        {
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    IEnumerator DamagePlayer(PlayerHealth playerHealth)
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            playerHealth.TakeDamage(2);
        }
    }

    IEnumerator SpreadWithDelay()
    {
        isFlowing = true;
        while (true)         {
            yield return new WaitForSeconds(spreadDelay);
            SpreadToAdjacentBlocks();
        }
    }

    void SpreadToAdjacentBlocks()
    {
        SpreadToAdjacent(Vector3.down, true); 
        if (IsBlockBelow())         {
            SpreadToAdjacent(Vector3.right, false);
            SpreadToAdjacent(Vector3.left, false);
            SpreadToAdjacent(Vector3.forward, false);
            SpreadToAdjacent(Vector3.back, false);
        }
    }

    void SpreadToAdjacent(Vector3 direction, bool keepOriginalScale)
    {
        RaycastHit hit;
        Vector3 newPosition = transform.position + direction;

        if (!visitedPositions.Contains(newPosition))
        {
            if (Physics.Raycast(transform.position, direction, out hit, 1f))
            {
                                if (hit.collider.CompareTag("Lava"))
                {
                    Debug.Log("Cobblestone created due to interaction with lava.");
                    Instantiate(cobbleStone, hit.point, Quaternion.identity);                  }
                                else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Block"))
                {
                    return;
                }
            }

            if (currentFlowDistance < maxFlowDistance)
            {
                GameObject newLavaBlock = Instantiate(prefab, newPosition, Quaternion.identity);

                if (keepOriginalScale)
                {
                    newLavaBlock.transform.GetChild(0).localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    Transform child = newLavaBlock.transform.GetChild(0);
                    float currentYScale = child.localScale.y;
                    float newYScale = Mathf.Max(currentYScale - scaleDecreaseY, minYScale);
                    child.localScale = new Vector3(child.localScale.x, newYScale, child.localScale.z);
                }

                generatedLiquids.Add(new GeneratedLiquidData(newLavaBlock, Time.time)); 
                Liquid liquid = newLavaBlock.GetComponent<Liquid>();
                liquid.currentFlowDistance = currentFlowDistance + 1;
                if (liquid.currentFlowDistance == maxFlowDistance)
                {
                    liquid.isGenerator = false;
                }
                liquid.isSource = false;

                liquid.generatedLiquids = generatedLiquids;
                visitedPositions.Add(newPosition);
            }
        }
    }

    public void RemoveAllGeneratedLiquids()
    {
        foreach (var liquidData in generatedLiquids)
        {
            if (liquidData.liquidObject != null)
            {
                Destroy(liquidData.liquidObject);
            }
        }

                generatedLiquids.Clear();
    }

    bool IsBlockBelow()
    {
        RaycastHit hit;
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = Vector3.down;
        float rayDistance = 1f;

        Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);

        return Physics.Raycast(rayOrigin, rayDirection, out hit, rayDistance, checkFallLayer);
    }
}
