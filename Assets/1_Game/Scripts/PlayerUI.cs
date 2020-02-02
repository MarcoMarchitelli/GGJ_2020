using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour {
    [Header("References")]
    public Image image;
    public TextMeshProUGUI nameText;

    PlayerEntity player;

    public void Setup ( PlayerEntity player ) {
        this.player = player;
        image.sprite = this.player.data.icon;
        nameText.text = this.player.data.name;
    }
}