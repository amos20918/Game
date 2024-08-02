using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HoleReactingAdvanced : MonoBehaviour
{
    public Collider ballHoleCollider;
    public Collider carHoleCollider;
    public Collider animalHoleCollider;

    public GameObject ballHoleOutside;
    public GameObject carHoleOutside;
    public GameObject animalHoleOutside;

    public Transform bar;
    public TMP_Text currentPoint;
    public AudioSource audioSource;


    public GameObject heart_1;
    public GameObject heart_2;
    public GameObject heart_3;
    public GameObject useHeartObject;

    public Color[] colors;




    private ReceiveHandMessage mqttMessage;
    private GenerateNewBall generate;

    private float timer;
    private float point;
    private string r;
    private string p;
    private string b;
    private string state;
    private string temp_state;
    private Vector3 originalScale;
    private float blood;
    private UseHeart useHeart;
    private bool use_heart;

    // Start is called before the first frame update
    void Start()
    {
        use_heart = false;
        if (useHeartObject != null)
        {
            useHeart = useHeartObject.GetComponent<UseHeart>();

            if (useHeart == null)
            {
                Debug.LogError("OtherScript is not attached to the otherGameObject.");
            }
        }
        else
        {
            Debug.LogError("otherGameObject has not been assigned in the inspector.");
        }
        if (useHeart != null)
        {
            use_heart = useHeart.useHeart;
        }

        if (use_heart)
        {
            heart_1.SetActive(true);
            heart_2.SetActive(true);
            heart_3.SetActive(true);
        }
        else
        {
            heart_1.SetActive(false);
            heart_2.SetActive(false);
            heart_3.SetActive(false);
        }

        generate = GetComponent<GenerateNewBall>();
        mqttMessage = GetComponent<ReceiveHandMessage>();
        timer = 0;
        ResetProgressBar(bar);
        state = "000";
        temp_state = state;
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(heart_1.GetComponent<SpriteRenderer>().color);
        point = float.Parse(currentPoint.text.Replace("Point: ", "").Replace("/30", ""));

        bool Rgray = ballHoleCollider.gameObject.GetComponent<SpriteRenderer>().color == Color.gray;
        bool Bgray = carHoleCollider.gameObject.GetComponent<SpriteRenderer>().color == Color.gray;
        bool Pgray = animalHoleCollider.gameObject.GetComponent<SpriteRenderer>().color == Color.gray;

        state = CheckState(Rgray, Bgray, Pgray);

        if ((Rgray || Bgray || Pgray) && temp_state == state && transform.localScale.x == 1.2f)
        {
            timer += Time.deltaTime;
            float progress = timer / 1.5f;
            SetProgressBar(bar, progress);
            if (timer >= 1.5f)
            {
                
                if (transform.parent.gameObject.name == "Balls" && Rgray && IsRightColor(ballHoleOutside))
                {
                    point += 2f;
                    ResetHoleColors();
                    ballHoleOutside.GetComponent<SpriteRenderer>().color = colors[Random.Range(0, colors.Length)];
                }
                else if (transform.parent.gameObject.name == "Cars" && Bgray && IsRightColor(carHoleOutside))
                {
                    point += 2f;
                    ResetHoleColors();
                    carHoleOutside.GetComponent<SpriteRenderer>().color = colors[Random.Range(0, colors.Length)];
                }
                else if (transform.parent.gameObject.name == "Animals" && Pgray && IsRightColor(animalHoleOutside))
                {
                    point += 2f;
                    ResetHoleColors();
                    animalHoleOutside.GetComponent<SpriteRenderer>().color = colors[Random.Range(0, colors.Length)];
                }

                else
                {
                    point += -1f;
                    ResetHoleColors();
                }
                audioSource.Play();
                gameObject.transform.localScale = originalScale;
                generate.DuplicateRandomObject();
                Destroy(gameObject);
                ResetProgressBar(bar);
                timer = 0;
                currentPoint.text = "Point: " + point.ToString() + "/30";
            }
        }
        else if ((Rgray || Bgray || Pgray) && temp_state == state && transform.localScale.x != 1.2f)
        {
            //Debug.Log("Progress bar used" + gameObject);
        }
        else
        {
            timer = 0;
            ResetProgressBar(bar);
        }
        temp_state = state;
    }
    public string CheckState(bool red, bool blue, bool purple)
    {

        if (red)
            r = "1";
        else
            r = "0";
        if (blue)
            b = "1";
        else
            b = "0";
        if (purple)
            p = "1";
        else
            p = "0";
        string state = r + b + p;
        return state;
    }

    private void SetProgressBar(Transform progressBar, float progress)
    {
        Vector3 scale = progressBar.localScale;
        scale.x = progress;
        progressBar.localScale = scale;
    }
    private void ResetProgressBar(Transform progressBar)
    {
        Vector3 scale = progressBar.localScale;
        scale.x = 0f;
        progressBar.localScale = scale;
    }
    private void ResetHoleColors()
    {
        SetHoleColor(ballHoleCollider, Color.black);
        SetHoleColor(carHoleCollider, Color.black);
        SetHoleColor(animalHoleCollider, Color.black);
    }
    private void SetHoleColor(Collider holeCollider, Color color)
    {
        SpriteRenderer sr = holeCollider.gameObject.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = color;
        }
    }
    bool IsRightColor(GameObject outside)
    {
        Color childColor = transform.Find("Child").GetComponent<SpriteRenderer>().color;
        if (childColor != null)
        {
            if(childColor == outside.GetComponent<SpriteRenderer>().color)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            Debug.Log("no childcolor");
            return false;
        }
    }
}
