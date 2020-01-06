using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

 
public class NewPlayableAsset : BasicPlayableBehaviour
{
    [Header("對話框")]
    public ExposedReference<Text> dialog;
    private Text _dialog;
    [Multiline(3)]
    public string dialogStr; 
         
    public override void OnGraphStart(Playable playable)
    {
        _dialog = dialog.Resolve(playable.GetGraph().GetResolver());
        
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {

        _dialog.gameObject.SetActive(true);
        _dialog.text = dialogStr;
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        if (_dialog)
        {
            _dialog.gameObject.SetActive(false);
        }
    }
}
