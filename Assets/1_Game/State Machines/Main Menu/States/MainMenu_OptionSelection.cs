using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu_OptionSelection : MainMenuStateBase {
    public override void Enter () {
        context.buttonsCanvasGroup.interactable = false;
        context.buttonsCanvasGroup.blocksRaycasts = false;

        context.screenFader.Play();
        context.screenFader.OnPlayEnd.AddListener( ScreenFadeInHandler );
    }

    private void ScreenFadeInHandler () {
        context.buttonsCanvasGroup.interactable = true;
        context.buttonsCanvasGroup.blocksRaycasts = true;
    }

    private void ScreenFadeOutHandler () {

    }

    public override void Exit () {
        context.screenFader.OnPlayEnd.RemoveListener( ScreenFadeInHandler );
    }
}