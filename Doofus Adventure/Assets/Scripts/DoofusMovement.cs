using UnityEngine;

public class DoofusMovement : MonoBehaviour
{
    private float speed;
    private Rigidbody rb;
    private Pulpit currentPulpit;

    void Start()
    {
        var diary = ConfigLoader.Load();
        speed = diary.player_data.speed;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal"); // A/D or arrows
        float v = Input.GetAxis("Vertical");   // W/S or arrows

        Vector3 move = new Vector3(h, 0, v) * speed * Time.deltaTime;
        transform.position += move;

        // Fall check — if Doofus drops below a threshold, game over
        if (transform.position.y < -5f)
        {
            GameManager.Instance.TriggerGameOver();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Pulpit pulpit = other.GetComponent<Pulpit>();
        if (pulpit != null)
        {
            if (currentPulpit != null)
                currentPulpit.SetOccupied(false);

            currentPulpit = pulpit;
            pulpit.SetOccupied(true);

            ScoreManager.Instance.RegisterLanding(pulpit);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Pulpit pulpit = other.GetComponent<Pulpit>();
        if (pulpit != null && pulpit == currentPulpit)
        {
            pulpit.SetOccupied(false);
            currentPulpit = null;
        }
    }
}