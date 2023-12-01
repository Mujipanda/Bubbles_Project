using TMPro;
using UnityEngine;

public class WeightDisplay : MonoBehaviour
{
    private TMP_Text text;
    private Rigidbody rb;
    private float mass;

    [SerializeField]
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }
    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody>();
        mass = rb.mass;
        text = GetComponent<TMP_Text>();
        text.text = mass.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        mass = rb.mass;

        Mathf.RoundToInt(mass);

        int massInt = Mathf.CeilToInt(mass);
        gameObject.transform.parent.LookAt(player.transform);
        text.text = "Mass " + massInt.ToString();
    }
}
