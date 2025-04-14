using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public TextMeshProUGUI notify;
    
    [Header("Products")]
    public GameObject products;
    public GameObject product;

    [Header("Animal Infomation")]
    public GameObject AnimalInfo;
    public Image Icon;
    public Slider EnergySilder;
    public Slider HealthSlider;
    public Slider GrowthSlider;
    public Button Feed;
    public Button Cure;
    public Button Exit;


    protected override void Start()
    {
        base.Start();
        AnimalInfo.SetActive(false);    
    }

    public void ShowMessage(int value)
    {
        if (notify == null)
        {
            return;
        }
        notify.text = "+" + value.ToString();
        Debug.Log(notify.text); 
        notify.transform.localScale = Vector3.one;
        notify.GetComponent<Animator>().Play(0);
    }
    
    public void LoadData(Sprite icon , float curEnergy , float curHealth , float curGrowth)
    {
        Icon.sprite = icon;
        EnergySilder.value = curEnergy;
        HealthSlider.value = curHealth;
        GrowthSlider.value = curGrowth; 
    }

    public void RemoveData()
    {
        Feed.onClick.RemoveAllListeners();
        Cure.onClick.RemoveAllListeners();
        Exit.onClick.RemoveAllListeners();
    }

    public void ShowProduct(Sprite icon)
    {
        GameObject g = ObjectPooling.intant.CreateGameObject(product);
        g.GetComponent<Image>().sprite = icon;
        g.transform.SetParent(products.transform);
    }
}
