using UnityEngine;

[RequireComponent(typeof(Cube))]
public class ExplodingCube : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private float explosionForce = 300f;
    [SerializeField] private float explosionRadius = 2f;

    private Cube _cube;

    private const float INITIAL_SPLIT_CHANCE = 1f;
    private float _splitChance = INITIAL_SPLIT_CHANCE;

    private void Awake()
    {
        _cube = GetComponent<Cube>();
    }

    private void OnMouseDown()
    {
        TryExplode();
    }

    private void TryExplode()
    {
        if (Random.value <= _splitChance)
        {
            Split();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Split()
    {
        int count = Random.Range(2, 7);
        Vector3 position = transform.position;

        for (int i = 0; i < count; i++)
        {
            GameObject newCube = Instantiate(cubePrefab, position, Random.rotation);
            Cube cubeComponent = newCube.GetComponent<Cube>();
            
            newCube.transform.localScale = transform.localScale * 0.5f;
            
            cubeComponent.SetColor(new Color(Random.value, Random.value, Random.value));
            
            var exploding = newCube.GetComponent<ExplodingCube>();
            exploding._splitChance = _splitChance / 2f;
            
            cubeComponent.AddExplosionForce(explosionForce, position, explosionRadius);
        }

        Destroy(gameObject);
    }
}