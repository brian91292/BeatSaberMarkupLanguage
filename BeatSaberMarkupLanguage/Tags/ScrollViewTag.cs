﻿using BeatSaberMarkupLanguage.Components;
using HMUI;
using IPA.Utilities;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BeatSaberMarkupLanguage.Tags
{
    public class ScrollViewTag : BSMLTag
    {
        public override string[] Aliases => new[] { "scroll-view" };

        public override GameObject CreateObject(Transform parent)
        {
            TextPageScrollView textScrollView = MonoBehaviour.Instantiate(Resources.FindObjectsOfTypeAll<ReleaseInfoViewController>().First().GetField<TextPageScrollView, ReleaseInfoViewController>("_textPageScrollView"), parent);
            textScrollView.name = "BSMLScrollView";
            Button pageUpButton = textScrollView.GetField<Button, TextPageScrollView>("_pageUpButton");
            Button pageDownButton = textScrollView.GetField<Button, TextPageScrollView>("_pageDownButton");
            VerticalScrollIndicator verticalScrollIndicator = textScrollView.GetField<VerticalScrollIndicator, TextPageScrollView>("_verticalScrollIndicator");
            RectTransform viewport = textScrollView.GetField<RectTransform, TextPageScrollView>("_viewport");
            GameObject.Destroy(textScrollView.GetField<TextMeshProUGUI, TextPageScrollView>("_text").gameObject);
            GameObject gameObject = textScrollView.gameObject;
            MonoBehaviour.Destroy(textScrollView);
            gameObject.SetActive(false);

            BSMLScrollView scrollView = gameObject.AddComponent<BSMLScrollView>();
            scrollView.SetField<ScrollView, Button>("_pageUpButton", pageUpButton);
            scrollView.SetField<ScrollView, Button>("_pageDownButton", pageDownButton);
            scrollView.SetField<ScrollView, VerticalScrollIndicator>("_verticalScrollIndicator", verticalScrollIndicator);
            scrollView.SetField<ScrollView, RectTransform>("_viewport", viewport);

            viewport.anchorMin = new Vector2(0, 0);
            viewport.anchorMax = new Vector2(1, 1);
            scrollView.ReserveButtonSpace = false;

            GameObject parentObj = new GameObject();
            parentObj.name = "BSMLScrollViewContent";
            parentObj.transform.SetParent(viewport, false);

            VerticalLayoutGroup verticalLayout = parentObj.AddComponent<VerticalLayoutGroup>();
            verticalLayout.childForceExpandHeight = false;
            verticalLayout.childForceExpandWidth = false;

            RectTransform rectTransform = parentObj.transform as RectTransform;
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.sizeDelta = new Vector2(0, 0);
            //parentObj.AddComponent<LayoutElement>();
            parentObj.AddComponent<ScrollViewContent>().scrollView = scrollView;

            GameObject child = new GameObject();
            child.name = "BSMLScrollViewContentContainer";
            child.transform.SetParent(rectTransform, false);

            VerticalLayoutGroup layoutGroup = child.AddComponent<VerticalLayoutGroup>();
            layoutGroup.childControlHeight = false;
            layoutGroup.childForceExpandHeight = false;
            layoutGroup.childAlignment = TextAnchor.MiddleCenter;
            layoutGroup.spacing = 0.5f;

            child.AddComponent<ContentSizeFitter>();
            child.AddComponent<LayoutElement>();
            ExternalComponents externalComponents = child.AddComponent<ExternalComponents>();
            externalComponents.components.Add(scrollView);
            externalComponents.components.Add(scrollView.transform);
            externalComponents.components.Add(gameObject.AddComponent<LayoutElement>());

            (child.transform as RectTransform).sizeDelta = new Vector2(0, -1);

            scrollView.SetField<ScrollView, RectTransform>("_contentRectTransform", parentObj.transform as RectTransform);
            gameObject.SetActive(true);
            return child;
        }
    }
}
