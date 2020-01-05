using UnityEngine;

namespace Gameplay
{
    public interface IIntentionRequester
    {
        void IntentionAccepted();
        void IntentionDeclined();
    }
}
