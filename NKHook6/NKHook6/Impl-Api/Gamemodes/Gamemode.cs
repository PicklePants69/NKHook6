using Assets.Scripts.Unity.Towers.Mods;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace NKHook6.Api.Gamemodes
{
    public class Gamemode
    {
        private static Image modItemBase;
        public bool isLoaded;
        public string modName;
        public string modDescription;
        public string modThumbnailPath;

        public Image modItem;
        public Text modText;
        public Button selectButton;

        public Gamemode()
        {
            Setup();
        }

        private void Setup()
        {
            Loader.customGameModes.Add(this);

        }

        internal void AddModItem(int slot)
        {
            if (modItemBase == null)
            {
                modItemBase = Loader.gamemodeUI.transform.Find("ModItem Scroll Area/ModItem Container/ModItem").GetComponent<Image>();

                Logger.Log("modItemBase size: " + modItemBase.rectTransform.sizeDelta.x + " | " + modItemBase.rectTransform.sizeDelta.y);
                //modItemBase.gameObject.SetActive(false);
            }

            int windowHeight = Screen.height;
            int windowWidth = Screen.width;

            

            Logger.Log("Window size: " + windowWidth + " | " + windowHeight);

            if (modItem == null)
            {
                modItem = GameObject.Instantiate(modItemBase);
                var pos = modItemBase.transform.position;
                modItem.transform.position = new Vector3(pos.x, pos.y - 45, pos.z);
                modItem.gameObject.SetActive(true);
                modItem.transform.SetParent(Loader.modItemContainer.transform);
            }

            if (modText == null)
                modText = modItem.transform.Find("ModName").GetComponent<Text>();

            if (selectButton == null)
                selectButton = modItem.transform.Find("SelectButton").GetComponent<Button>();

            //SetPos();
            SetScaleSize();

            modText.text = "Half Income!";

            //selectButton.onClick.AddListener(() => TestButtonClick());

            //modText.text = "Hello World";

            /*var pos = modItemBase.transform.position;

            var modItemRt = modItemBase.GetComponent<RectTransform>();
            var scale = modItemRt.sizeDelta;*/
            //GameObject test = GameObject.Instantiate(modItemBase.gameObject);

            //test.transform.parent = modItem.transform;







            //test.transform.localScale = new Vector3(scale.x +50, scale.y, scale.z);
            //var rt = test.GetComponent<RectTransform>();
            //rt.sizeDelta = new Vector2(scale.x, scale.y);
            //test.transform.position = new Vector3(pos.x, pos.y-35, pos.z);


            //modItem.transform.SetParent(Loader.modItemContainer.transform);
            //modItem.gameObject.SetActive(true);


            //test.transform.SetParent(Loader.modItemContainer.transform);


            /*var text = test.transform.Find("ModName").GetComponent<Text>();
            text.text = "new mod item's text";*/


            //test.SetActive(true);



            //test.transform.parent = Loader.modItemContainer.transform;

            //test = modItem;
            /*var pos = modItem.transform.position;
            test.transform.position = new Vector3(pos.x, pos.y - 35, pos.z);*/

            //var a = Loader.modItemContainer.gameObject.AddComponent<Image>() as Image;
        }

        private void SetScaleSize()
        {
            var scale = Loader.modItemContainer.sizeDelta;
            var baseSize = modItemBase.GetComponent<RectTransform>().sizeDelta;
            modItem.GetComponent<RectTransform>().sizeDelta = new Vector2(baseSize.x+1000, baseSize.y);

            /*var text = modItemBase.transform.Find("ModName").GetComponent<Text>();
            var textSize = text.rectTransform.sizeDelta;
            modText.GetComponent<RectTransform>().sizeDelta = new Vector2(textSize.x, textSize.y - 10);

            var buttonSize = selectButton.GetComponent<RectTransform>().sizeDelta;
            selectButton.GetComponent<RectTransform>().sizeDelta = buttonSize;*/
        }

        private void SetPos()
        {
            //var baseSize = modItemBase.GetComponent<RectTransform>().sizeDelta;
            var pos = Loader.modItemContainer.position;
            //modItem.transform.position = new Vector3(pos.x + 5, modItem.transform.position.y, modItem.transform.position.z);

            /*var text = modItemBase.transform.Find("ModName").GetComponent<Text>();
            var textSize = text.rectTransform.sizeDelta;
            modText.GetComponent<RectTransform>().sizeDelta = textSize;

            var buttonSize = selectButton.GetComponent<RectTransform>().sizeDelta;
            selectButton.GetComponent<RectTransform>().sizeDelta = buttonSize;*/
        }


        void TestButtonClick()
        {

        }
    }
}
