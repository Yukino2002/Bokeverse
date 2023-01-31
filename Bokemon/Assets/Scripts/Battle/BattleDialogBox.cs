using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleDialogBox : MonoBehaviour {
    [SerializeField] Text dialogText;
    [SerializeField] GameObject actionSelector;
    [SerializeField] GameObject moveSelector;
    [SerializeField] GameObject moveDetails;

    [SerializeField] int lettersPerSecond;
    [SerializeField] Color highlightedColor;

    [SerializeField] List<Text> actionTexts;
    [SerializeField] List<Text> moveTexts;

    [SerializeField] Text ppText;
    [SerializeField] Text typeText;

    public void SetDialog(string dialog) {
        dialogText.text = dialog;
    }

    // coroutine to type out the dialog, one letter at a time
    public IEnumerator TypeDialog(string dialog) {
        dialogText.text = "";
        foreach (char letter in dialog.ToCharArray()) {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }

        yield return new WaitForSeconds(1f);
    }

    // all Text based components have a enable/disable function
    public void EnableDialogText(bool enabled) {
        dialogText.enabled = enabled;
    }

    // ActionSelector is a GameObject, hence this property
    public void EnableActionSelector(bool enabled) {
        actionSelector.SetActive(enabled);
    }

    public void EnableMoveSelector(bool enabled) {
        moveSelector.SetActive(enabled);
        moveDetails.SetActive(enabled);
    }

    // to select the action, change the color of the text
    public void UpdateActionSelection(int selectedAction) {
        for (int i = 0; i < actionTexts.Count; i++) {
            if (i == selectedAction) {
                actionTexts[i].color = highlightedColor;
            } else {
                actionTexts[i].color = Color.black;
            }
        }
    }

    public void UpdateMoveSelection(int selectedMove, Move move) {
        for (int i = 0; i < moveTexts.Count; i++) {
            if (i == selectedMove) {
                moveTexts[i].color = highlightedColor;
            } else {
                moveTexts[i].color = Color.black;
            }
        }

        ppText.text = $"{move.PP}/{move.Base.PP}";
        typeText.text = move.Base.Type.ToString();
    }

    public void SetMoveNames(List<Move> moves) {
        for (int i = 0; i < moveTexts.Count; i++) {
            if (i < moves.Count) {
                moveTexts[i].text = moves[i].Base.Name;
            } else {
                moveTexts[i].text = "-";
            }
        }
    }
}
