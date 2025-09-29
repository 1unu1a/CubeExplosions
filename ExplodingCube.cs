using UnityEngine;

public class ExplodingCube : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private int minChildren = 2;
    [SerializeField] private int maxChildren = 6;
    [SerializeField] private float explosionForce = 300f;
    [SerializeField] private float explosionRadius = 2f;

    private static float _splitChance = 1f;

    private void OnMouseDown()
    {
        if (Random.value <= _splitChance)
        {
            SplitCube();
            _splitChance *= 0.5f;
        }
        else
        {
            Destroy(gameObject);
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
            
            var renderer = newCube.GetComponent<Renderer>();
            renderer.material.color = Random.ColorHSV();
            
            var rb = newCube.AddComponent<Rigidbody>();
            
            rb.AddExplosionForce(explosionForce, position, explosionRadius);
        }

        Destroy(gameObject);
    }
}