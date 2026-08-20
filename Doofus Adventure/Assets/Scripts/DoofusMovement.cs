using UnityEngine;

public class DoofusMovement : MonoBehaviour
{
    private float speed;
    private Pulpit currentPulpit;

    public float raycastDistance = 2f;
    public LayerMask pulpitLayer;

    public float fallGracePeriod = 0.3f; // seconds allowed with no pulpit below before Game Over
    private float timeSinceLastGrounded = 0f;

    void Start()
    {
        var diary = ConfigLoader.Load();
        speed = diary.player_data.speed;
    }

    void Update()
    {
        if (GameManager.Instance.IsGameActive)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            Vector3 move = new Vector3(h, 0, v) * speed * Time.deltaTime;
            transform.position += move;
        }

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
                timeSinceLastGrounded = 0f; // reset grace timer, we're on solid ground

                if (pulpit != currentPulpit)
                {
                    if (currentPulpit != null)
                        currentPulpit.SetOccupied(false);

                    currentPulpit = pulpit;
                    pulpit.SetOccupied(true);

                    ScoreManager.Instance.RegisterLanding(pulpit);
                }

                TimerUI.Instance.UpdateTimer(pulpit.lifeTime);
                return;
            }
        }

        // no pulpit detected below this frame
        timeSinceLastGrounded += Time.deltaTime;

        if (currentPulpit != null)
        {
            currentPulpit.SetOccupied(false);
            currentPulpit = null;
        }

        if (timeSinceLastGrounded >= fallGracePeriod)
        {
            GameManager.Instance.TriggerGameOver();
        }
    }
}