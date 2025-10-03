using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Renderer _renderer;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
    }

    public void SetColor(Color color)
    {
        _renderer.material.color = color;
    }

    public void AddExplosionForce(float force, Vector3 position, float radius)
    {
        _rigidbody.AddExplosionForce(force, position, radius);
    }
}