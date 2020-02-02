using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour {
    [Header("References")]
    public Image image;
    public TextMeshProUGUI nameText;
    public GameObject repairCommand;
    public GameObject repairProgress;
    public Image fillImage;

    private PlayerEntity player;

    public void Setup ( PlayerEntity player ) {
        this.player = player;
        image.sprite = this.player.data.icon;
        nameText.text = this.player.data.name;
    }

    public void CanRepair ( bool value ) {
        repairCommand.SetActive( value );
        nameText.gameObject.SetActive( !value );
        repairProgress.SetActive( false );
    }

    public void StartRepair ( bool value ) {
        repairProgress.SetActive( value );
        nameText.gameObject.SetActive( !value );
        repairCommand.SetActive( false );
    }

    public void Repairing ( float percent ) {
        fillImage.fillAmount = percent;
    }
}