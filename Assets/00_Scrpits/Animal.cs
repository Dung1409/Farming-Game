using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Animal : MonoBehaviour, IAnimal
{
    [Header("Attribute Animal")]
    private float currentEnergy = 0.5f;
    [SerializeField] private float MinEnergy;
    private float currentHealth = 0.5f;
    private float currentGrowth = 0;
    bool isRest;
    bool isGrowth;

    [SerializeField]Button ShowInfo;
    [SerializeField] private TextMeshProUGUI notify;
    private State state;

    [Header("Animal Move")]
    private SpriteRenderer _spriteRenderer;
    private Animator _anim;


    private void Awake()
    {
        state = State.Idle;
        MinEnergy = currentEnergy / 5;
        _anim = GetComponent<Animator>();
      
        isRest = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        notify.gameObject.SetActive(false);
        ShowInfo = transform.GetChild(0).GetComponentInChildren<Button>();
        ShowInfo.onClick.AddListener(LoadInfo);
    }
    private void Update()
    {
        currentGrowth += (isGrowth ? 0.005f : 0.01f) * Time.deltaTime;
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

        if(currentEnergy < MinEnergy|| currentHealth <= 0.5f || isRest)
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

    IEnumerator Rest()
    {
        isRest = true;
        yield return new WaitForSeconds(2f);
    }

    public void Feed()
    {
        currentEnergy += 0.5f;
        currentEnergy = currentEnergy >= 1f ? 1f : currentEnergy;
        notify.gameObject.SetActive(false); 
        UIManager.intant.EnergySilder.value = currentEnergy;
    }

    public void Cure()
    {
        currentHealth += 0.5f;
        currentHealth = currentHealth >= 1f ? 1f : currentHealth;
        notify.gameObject.SetActive(false); 
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
}
