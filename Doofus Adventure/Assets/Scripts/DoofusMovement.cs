using UnityEngine;

public class DoofusMovement : MonoBehaviour
{
    private float speed;
    private Pulpit currentPulpit;

    public float raycastDistance = 2f;
    public LayerMask pulpitLayer; // optional, can leave as Everything

    void Start()
    {
        var diary = ConfigLoader.Load();
        speed = diary.player_data.speed;
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(h, 0, v) * speed * Time.deltaTime;
        transform.position += move;

        CheckPulpitBelow();

        if (transform.position.y < -5f)
        {
            GameManager.Instance.TriggerGameOver();
        }
    }

    void CheckPulpitBelow()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance))
        {
            Pulpit pulpit = hit.collider.GetComponent<Pulpit>();

            if (pulpit != null)
            {
                if (pulpit != currentPulpit)
                {
                    // landed on a NEW pulpit
                    if (currentPulpit != null)
                        currentPulpit.SetOccupied(false);

                    currentPulpit = pulpit;
                    pulpit.SetOccupied(true);

                    ScoreManager.Instance.RegisterLanding(pulpit);
                }

                // always update timer UI while standing on any pulpit
                TimerUI.Instance.UpdateTimer(pulpit.lifeTime);
                return;
            }
        }

        // no pulpit found below — Doofus is in the air / off the edge
        if (currentPulpit != null)
        {
            currentPulpit.SetOccupied(false);
            currentPulpit = null;
        }
    }
}