using UnityEngine;

[RequireComponent(typeof(Cube))]
public class ExplodingCubeV2 : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private int minChildren = 2;
    [SerializeField] private int maxChildren = 6;
    [SerializeField] private float baseExplosionForce = 300f;
    [SerializeField] private float baseExplosionRadius = 2f;

    private Cube _cube;
    private static float _splitChance = 1f;
    private static readonly Collider[] Buffer = new Collider[50];

    private void Awake()
    {
        _cube = GetComponent<Cube>();
    }

    private void OnMouseDown()
    {
        if (Random.value <= _splitChance)
        {
            SplitCube();
            _splitChance *= 0.5f;
        }
        else
        {
            GlobalExplosion();
        }
    }

    private void SplitCube()
    {
        Vector3 position = transform.position;
        int count = Random.Range(minChildren, maxChildren + 1);

        for (int i = 0; i < count; i++)
        {
            GameObject newCube = Instantiate(cubePrefab, position, Random.rotation);
            
            newCube.transform.localScale = transform.localScale * 0.5f;
            
            Cube cubeComponent = newCube.GetComponent<Cube>();
            cubeComponent.SetColor(Random.ColorHSV());
            cubeComponent.AddExplosionForce(baseExplosionForce, position, baseExplosionRadius);
        }

        Destroy(gameObject);
    }

    private void GlobalExplosion()
    {
        Vector3 position = transform.position;
        
        float scaleFactor = 1f / transform.localScale.x;
        float explosionForce = baseExplosionForce * scaleFactor;
        float explosionRadius = baseExplosionRadius * scaleFactor;
        
        int count = Physics.OverlapSphereNonAlloc(position, explosionRadius, Buffer);

        for (int i = 0; i < count; i++)
        {
            Rigidbody rb = Buffer[i].attachedRigidbody;
            if (rb != null && rb.gameObject != gameObject)
            {
                rb.AddExplosionForce(explosionForce, position, explosionRadius);
            }
        }

        Destroy(gameObject);
    }
}
