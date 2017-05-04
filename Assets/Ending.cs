using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Ending : MonoBehaviour {

    public TextAsset Instructions;
    private List<string> dialogComponents;
    public GameObject credits;

    void Awake()
    {
        dialogComponents = new List<string>(Instructions.text.Split('\n'));
        dialogComponents = dialogComponents.Select(x => x.Trim()).ToList();
        dialogComponents = dialogComponents.Where(x => x != "").ToList();
    }

    // Use this for initialization
    void Start () {
        Player.instance.GetComponent<SpriteRenderer>().enabled = false;
        UIController.instance.screenfader.FadeIn(1.0f);
        StartCoroutine(End());
    }
	
    IEnumerator End()
    {
        GameManager.instance.SuspendGame();
        for (int i = 0; i < dialogComponents.Count; i++)
        {
            string[] dialogPieces = dialogComponents[i].Split(new string[] { " : " }, System.StringSplitOptions.None);
            string speaker = "";
            string dialog = "";
            if (dialogPieces.Count() > 1)
            {
                speaker = dialogPieces[0];
                dialog = dialogPieces[1];
            }
            else
                dialog = dialogPieces[0];
            UIController.instance.dialog.displayDialog(dialog, speaker);


            yield return new WaitForSeconds(0.1f);
            while (!UIController.instance.dialog.dialogCompleted)
            {
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(0.2f);
            //Replace this with things in the control set
            while (!Controls.confirmInputHeld())
            {
                yield return new WaitForSeconds(0.1f);
            }
        }

        UIController.instance.dialog.closeDialog();

        credits.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        while (true)
        {
            if (Controls.confirmInputHeld())
                break;
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(GameManager.instance.gameObject);
        Destroy(QuestManager.instance.gameObject);
        Destroy(CameraControls.instance.gameObject);
        Destroy(Player.instance.gameObject);
        Destroy(UIController.instance.gameObject);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
}
