using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace VG
{
    public class TokenMediator : Initializable
    {
        [SerializeField] private List<Initializable> _waitInitialization;

        public override void Initialize()
        {
            StartCoroutine(InitializeTokens());
        }


        IEnumerator InitializeTokens()
        {
            bool allInitialized = false;

            while (!allInitialized)
            {
                allInitialized = true;

                foreach (var waitInitialization in _waitInitialization)
                    if (!waitInitialization.initialized) allInitialized = false;

                yield return null;
            }

            TokenHandler.LoadTokens();

            InitCompleted();

        }


    }
}



