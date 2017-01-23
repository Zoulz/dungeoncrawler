using strange.extensions.context.impl;
using DG.Tweening;

namespace trailmarch
{
    public class TrailmarchDungeonRoot : ContextView
    {
        void Awake()
        {
            DOTween.Init(false, true, LogBehaviour.ErrorsOnly);

            context = new TrailmarchDungeonContext(this);
        }
    }
}
