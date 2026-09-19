using UnityEngine;
using System.Collections;

[AddComponentMenu("MyGame/Rocket")]
public class Rocket : MonoBehaviour
{
    public float m_speed = 10;
    public float m_liveTime = 1;
    public float m_power = 1.0f;

    protected Transform m_trasform;

    void Start()
    {
        m_trasform = this.transform;
        Destroy(this.gameObject, m_liveTime);
    }

    void Update()
    {
        m_trasform.Translate(new Vector3(0, 0, -m_speed * Time.deltaTime));
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy"))
            return;

        Destroy(this.gameObject);
    }
}
