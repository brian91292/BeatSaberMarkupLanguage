﻿using HMUI;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace BeatSaberMarkupLanguage.Components
{
    public class ScrollViewContent : MonoBehaviour
    {
        public ScrollView scrollView;
        
        void Start()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
            StopAllCoroutines();
            StartCoroutine(SetupScrollView());
        }
        void OnEnable()
        {
            UpdateScrollView();
        }
        void OnRectTransformDimensionsChange()
        {
            UpdateScrollView();
        }
        private IEnumerator SetupScrollView()
        {
            RectTransform rectTransform = (transform as RectTransform);
            yield return new WaitWhile(() => rectTransform.sizeDelta.y == -1); //This is a reliable way to wait for the vertical layout group to be set up which it must be before the scrollview can be setup
            UpdateScrollView();
        }
        private void UpdateScrollView()
        {
            scrollView?.Setup();
            scrollView?.RefreshButtonsInteractibility();
        }
    }
}
