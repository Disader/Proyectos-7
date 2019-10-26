using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHeartsVisual : MonoBehaviour
{
    public static HealthHeartsSystem healthHeartsSystemStatic;

    [SerializeField]
    private Sprite heart0_Sprite;
    [SerializeField]
    private Sprite heart0_5_Sprite;
    [SerializeField]
    private Sprite heart1_Sprite;
    [SerializeField]
    private AnimationClip heartFullAnimationClip;

    [Header("Variables de la Representación Visual del Corazones")]
    public int initialHeartNumber;
    public int numberOfHeartsPerRow;
    public float distanceBetweenHeartsXY;
    public float heartsSize;

    private List<HeartImage> heartImageList;
    private HealthHeartsSystem healthHeartsSystem;
    private bool isHealing;

    private void Awake()
    {
        heartImageList = new List<HeartImage>();
    }

    private void Start()
    {
        InvokeRepeating("HealingAnimatedPeriodic", 0, 0.05f);
        HealthHeartsSystem healthHeartsSystem = new HealthHeartsSystem(initialHeartNumber);
        SetHealthHeartsSystem(healthHeartsSystem);
    }

    /// //Prueba///////////////////

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            healthHeartsSystem.Damage(1);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            healthHeartsSystem.Damage(2);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            healthHeartsSystem.Heal(1);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            healthHeartsSystem.Heal(3);
        }
    }

    /// //Prueba///////////////////

    public void SetHealthHeartsSystem(HealthHeartsSystem healthHeartsSystem)
    {
        this.healthHeartsSystem = healthHeartsSystem;
        healthHeartsSystemStatic = healthHeartsSystem;

        List<HealthHeartsSystem.Heart> heartList = healthHeartsSystem.GetHeartList();
        int row = 0;
        int col = 0;
        int colMax = numberOfHeartsPerRow;
        float rowColSize = distanceBetweenHeartsXY;

        for (int i = 0; i < heartList.Count; i++)
        {
            HealthHeartsSystem.Heart heart = heartList[i];
            Vector2 heartAnchoredPosition = new Vector2(col * rowColSize, -row * rowColSize);
            CreateHeartImage(heartAnchoredPosition).SetHeartFragment(heart.GetFragmentAmount());

            col++;
            if(col >= colMax)
            {
                row++;
                col = 0;
            }
        }

        healthHeartsSystem.OnDamaged += HealthHeartsSystem_OnDamaged;
        healthHeartsSystem.OnHealed += HealthHeartsSystem_OnHealed;
        healthHeartsSystem.OnDead += HealthHeartsSystem_OnDead;
    }

    private void HealthHeartsSystem_OnDead(object sender, System.EventArgs e)
    {
        Debug.Log("Dead!");
    }

    private void HealthHeartsSystem_OnHealed(object sender, System.EventArgs e)
    {
        //El sistema de corazones se ha curado
        //RefreshAllHearts();
        isHealing = true;
    }

    private void HealthHeartsSystem_OnDamaged(object sender, System.EventArgs e)
    {
        //El sistema de corazones ha recibido daño
        RefreshAllHearts();
    }

    private void RefreshAllHearts()
    {
        List<HealthHeartsSystem.Heart> heartList = healthHeartsSystem.GetHeartList();
        for (int i = 0; i < heartImageList.Count; i++)
        {
            HeartImage heartImage = heartImageList[i];
            HealthHeartsSystem.Heart heart = heartList[i];
            heartImage.SetHeartFragment(heart.GetFragmentAmount());
        }
    }

    public void HealingAnimatedPeriodic()
    {
        if (isHealing)
        {
            bool fullyHealed = true;

            List<HealthHeartsSystem.Heart> heartList = healthHeartsSystem.GetHeartList();
            for (int i = 0; i < heartList.Count; i++)
            {
                HeartImage heartImage = heartImageList[i];
                HealthHeartsSystem.Heart heart = heartList[i];

                if (heartImage.GetFragmentAmount() != heart.GetFragmentAmount())
                {
                    //Comprobar si el resultado visual es diferente de la lógica
                    heartImage.AddHeartVisualFragment();
                    if(heartImage.GetFragmentAmount() == HealthHeartsSystem.MAX_FRAGMENT_AMOUNT)
                    {
                        //Este corazón estaba completamente curado
                        heartImage.PlayHeartFullAnimation();
                    }
                    fullyHealed = false;
                    break;
                }
            }

            if(fullyHealed)
            {
                isHealing = false;
            }
        }
     }

    public HeartImage CreateHeartImage(Vector2 anchoredPosition)
    {
        //Crear el GameObject
        GameObject heartGameObject = new GameObject("Heart", typeof(Image), typeof (Animation));

        //Ponerlo como hijo de este Transform
        heartGameObject.transform.SetParent(this.transform);
        heartGameObject.transform.localPosition = Vector3.zero;

        //Colocar la posicion del ancla y el tamaño del corazón
        heartGameObject.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
        heartGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(heartsSize, heartsSize);
        heartGameObject.GetComponent<RectTransform>().localScale = new Vector2(1, 1);

        heartGameObject.GetComponent<Animation>().AddClip(heartFullAnimationClip, "HeartFull");

        //Ponerle el sprite 
        Image heartImageUI = heartGameObject.GetComponent<Image>();
        heartImageUI.sprite = heart0_Sprite;

        HeartImage heartImage = new HeartImage(this, heartImageUI, heartGameObject.GetComponent<Animation>());
        heartImageList.Add(heartImage);

        return heartImage;
    }

    //ESTO ES UNA CLASE
    //Representa un único corazón
    public class HeartImage
    {
        private int fragments;
        private Image heartImage;
        private HealthHeartsVisual healthHeartsVisual;
        private Animation animation;

        public HeartImage(HealthHeartsVisual healthHeartsVisual ,Image heartImage, Animation animation)
        {
            this.healthHeartsVisual = healthHeartsVisual;
            this.heartImage = heartImage;
            this.animation = animation;
        }

        public void SetHeartFragment(int fragments)
        {
            this.fragments = fragments;
            switch(fragments)
            {
                case 0: heartImage.sprite = healthHeartsVisual.heart0_Sprite; break;
                case 1: heartImage.sprite = healthHeartsVisual.heart0_5_Sprite; break;
                case 2: heartImage.sprite = healthHeartsVisual.heart1_Sprite; break;
            }
        }

        public int GetFragmentAmount()
        {
            return fragments;
        }

        public void AddHeartVisualFragment()
        {
            SetHeartFragment(fragments + 1);
        }

        public void PlayHeartFullAnimation()
        {
            animation.Play("HeartFull", PlayMode.StopAll);
        }
    }
}
