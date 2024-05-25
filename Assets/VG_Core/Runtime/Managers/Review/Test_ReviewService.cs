using System;
using UnityEngine;
using VG.Internal;


namespace VG
{
    public class Test_ReviewService : ReviewService
    {
        [SerializeField] private bool _useInBuild;


        public override bool supported => VG.Environment.editor || _useInBuild;

        public override void Initialize() => InitCompleted();

        public override void Request(Action onHandled)
        {
            Core.LogEditor("Review requested.");
            onHandled?.Invoke();
        }

        protected override void OnInitialized() { }
    }
}


