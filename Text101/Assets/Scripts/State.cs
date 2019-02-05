using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(menuName = "State")] creates a 'State' choice under asset menu -> create -> 'State'
[CreateAssetMenu(menuName = "State")]
public class State : ScriptableObject
{
    // [SerializeField] adds the field to the inspector within unity
    // [TextArea(14, 10)] changes the size of the text field within unity so that we can 
    // actually see what we are typing
    [TextArea(14, 10)] [SerializeField] string storyText;
    [SerializeField] State[] nextStates;

    public string GetStateStory()
    {
        return storyText;
    }

    public State[] GetNextStates()
    {
        return nextStates;
    }
}
