using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class HoleReacting : MonoBehaviour
{
    public Collider redHoleCollider;
    public Collider blueHoleCollider;
    public Collider purpleHoleCollider;

    public Transform bar;
    public TMP_Text currentPoint;
    public AudioSource audioSource;

    
    public GameObject heart_1;
    public GameObject heart_2;
    public GameObject heart_3;
    public GameObject useHeartObject;




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

        bool Rgray = redHoleCollider.gameObject.GetComponent<SpriteRenderer>().color == Color.gray;
        bool Bgray = blueHoleCollider.gameObject.GetComponent<SpriteRenderer>().color == Color.gray;
        bool Pgray = purpleHoleCollider.gameObject.GetComponent<SpriteRenderer>().color == Color.gray;

        state = CheckState(Rgray, Bgray, Pgray);

        if ((Rgray || Bgray || Pgray) && temp_state == state && transform.localScale.x == 1.2f)
        {
            //Debug.Log(gameObject);
            timer += Time.deltaTime;
            float progress = timer / 1.5f;
            SetProgressBar(bar, progress);
            if (timer >= 1.5f)
            {
                
                if (transform.parent.gameObject.name == "Red Balls" && Rgray)
                {
                    point += 1f;
                    ResetHoleColors();
                }
                else if(transform.parent.gameObject.name == "Blue Balls" && Bgray)
                {
                    point += 1f;
                    ResetHoleColors();
                }
                else if( transform.parent.gameObject.name == "Purple Balls" && Pgray)
                {
                    point += 1f;
                    ResetHoleColors();
                }

                else
                {
                    if(transform.parent.gameObject.name == "Error Balls")
                    {
                        useHeart.blood -= 1f;
                        if(useHeart.blood == 2)
                        {
                            heart_1.GetComponent<SpriteRenderer>().color = Color.black;
                            Debug.Log(heart_1.GetComponent<SpriteRenderer>().color);
                        }
                        else if(useHeart.blood == 1)
                        {
                            heart_2.GetComponent<SpriteRenderer>().color = Color.black;
                        }
                        else if(useHeart.blood == 0)
                        {
                            heart_3.GetComponent<SpriteRenderer>().color = Color.black;
                            point = 0f;
                        }
                        ResetHoleColors();
                    }
                    else
                    {
                        point += -1f;
                        ResetHoleColors();
                    }

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
        else if((Rgray || Bgray || Pgray) && temp_state == state && transform.localScale.x != 1.2f)
        {
        }
        else
        {
            timer = 0;
            ResetProgressBar(bar);
        }
        temp_state = state;
    }
    public string CheckState (bool red, bool blue, bool purple)
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
        SetHoleColor(redHoleCollider, Color.black);
        SetHoleColor(blueHoleCollider, Color.black);
        SetHoleColor(purpleHoleCollider, Color.black);
    }
    private void SetHoleColor(Collider holeCollider, Color color)
    {
        SpriteRenderer sr = holeCollider.gameObject.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = color;
        }
        //Debug.Log(sr.color);
    }
}
