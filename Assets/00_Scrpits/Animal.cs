using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Animal : MonoBehaviour, IAnimal
{
    [Header("Attribute Animal")]
    private float currentEnergy = 0.5f;
    private float currentHealth = 0.5f;
    private float currentGrowth = 0;
    bool isRest;
    bool isGrowth;
    [SerializeField] private LayerMask layer;
    Dictionary<Vector3, bool> dir = new Dictionary<Vector3, bool>();

    [SerializeField] private Button ShowInfo;
    [SerializeField] private TextMeshProUGUI notify;
    private State state;

    [Header("Animal Move")]
    private SpriteRenderer _spriteRenderer;
    private Animator _anim;


    private void Awake()
    {
        state = State.Idle;
        _anim = GetComponent<Animator>();
        isRest = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        notify.gameObject.SetActive(false);
        ShowInfo = transform.GetChild(0).GetComponentInChildren<Button>();
        ShowInfo.onClick.AddListener(LoadInfo);
        dir.Add(Vector3.left, false);
        dir.Add(Vector3.right, false);
        dir.Add(Vector3.up, false);
        dir.Add(Vector3.down, false);
    }

    private void Update()
    {
        Path();
        currentGrowth += (isGrowth ? 0.005f : 0.01f) * Time.deltaTime;
        currentEnergy -=  0.01f * Time.deltaTime;
        currentHealth -=  0.01f * Time.deltaTime;
        if (currentGrowth >= 1 && !isGrowth)
        {
            currentGrowth = 1f;
            isGrowth = true;
            _anim.SetLayerWeight(1, 1);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if(currentHealth <= 0.2f || isRest)
        {
            if(!isRest)
            {
                notify.gameObject.SetActive(true);  
            }
            state = State.Idle;
            _anim.SetInteger(Contant.State, (int)state);
            return;
        }
    }

    enum State
    {
        Idle,
        Travel
    }

    public void Feed()
    {
        currentEnergy += 0.5f;
        currentEnergy = currentEnergy >= 1f ? 1f : currentEnergy;
        if(currentEnergy >= 0.8f)
        {
            notify.gameObject.SetActive(false); 
        }
        UIManager.intant.EnergySilder.value = currentEnergy;
    }

    public void Cure()
    {
        currentHealth += 0.5f;
        currentHealth = currentHealth >= 1f ? 1f : currentHealth;
        if(currentHealth >= 0.8f)
        {
            notify.gameObject.SetActive(false); 
        }
        UIManager.intant.HealthSlider.value =  currentHealth;
    }

    public void LoadInfo()
    {
        isRest = true;
        UIManager.intant.AnimalInfo.SetActive(true);
        UIManager.intant.LoadData(curEnergy: currentEnergy, curHealth: currentHealth, curGrowth: currentGrowth , icon: _spriteRenderer.sprite);
        UIManager.intant.Feed.onClick.AddListener(Feed);
        UIManager.intant.Cure.onClick.AddListener(Cure);
        UIManager.intant.Exit.onClick.AddListener(Exit);
    }

    public void Exit()
    {
        isRest = false;
        UIManager.intant.RemoveData();
        UIManager.intant.AnimalInfo.SetActive(false);
    }

    private void Path()
    {
        
        
    }
}
