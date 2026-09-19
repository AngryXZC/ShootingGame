using UnityEngine;
using System.Collections;

[AddComponentMenu("MyGame/Player")]
public class Player : MonoBehaviour
{
    public float m_speed = 1;
    public float m_life = 3;
    public Transform m_rocket;

    protected Transform m_transform;

    float m_rocketRate = 0;

    public AudioClip m_shootClip;
    protected AudioSource m_audio;
    public Transform m_explosionFX;
    protected Vector3 m_targetPos;
    public LayerMask m_inputMask;

    void Start()
    {
        m_transform = this.transform;
        m_audio = GetComponent<AudioSource>();
        m_targetPos = this.m_transform.position;
    }

    void Update()
    {
        float movev = 0;
        float moveh = 0;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            movev -= m_speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            movev += m_speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveh += m_speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveh -= m_speed * Time.deltaTime;
        }

        bool keyboardMove = moveh != 0f || movev != 0f;
        if (keyboardMove)
        {
            this.m_transform.Translate(new Vector3(moveh, 0, movev));
            m_targetPos = this.m_transform.position;
        }
        else
        {
            MoveTo();
        }

        m_rocketRate -= Time.deltaTime;
        if (m_rocketRate <= 0)
        {
            m_rocketRate = 0.1f;

            if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
            {
                Instantiate(m_rocket, m_transform.position, m_transform.rotation);

                if (m_audio != null && m_shootClip != null)
                {
                    m_audio.PlayOneShot(m_shootClip);
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("PlayerRocket"))
        {
            m_life -= 1;

            if (m_life <= 0)
            {
                Instantiate(m_explosionFX, m_transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }

    void MoveTo()
    {
        if (!Input.GetMouseButton(0) || Camera.main == null)
        {
            return;
        }

        Vector3 ms = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(ms);
        RaycastHit hitinfo;
        if (Physics.Raycast(ray, out hitinfo, 1000, m_inputMask))
        {
            m_targetPos = hitinfo.point;
        }

        this.m_transform.position = Vector3.MoveTowards(
            this.m_transform.position,
            m_targetPos,
            m_speed * Time.deltaTime);
    }
}
