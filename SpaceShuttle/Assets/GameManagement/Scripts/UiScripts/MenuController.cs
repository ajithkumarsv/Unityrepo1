using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GM
{
    public class MenuController : Singleton<MenuController>
    {
        //Queue<Handler> handler = new Queue<Handler>();

        Handler currentActiveHandler = null;

        [SerializeField] MenuHandler menuhandler;
        [SerializeField] OptionsHandler optionsHandler;
        [SerializeField] CreditHandler creditHandler;


        private void Awake()
        {
            menuhandler.DeInit();
            optionsHandler.DeInit();
            creditHandler.DeInit();
        }
        public void OnLoaded()
        {
            ActivateMenuHandler();
        }


        public void ActivateHandler(Handler handler)
        {
            if (currentActiveHandler != null) currentActiveHandler.DeInit();
            handler.Init();
            currentActiveHandler = handler;
        }

        public void ActivateMenuHandler()
        {
            ActivateHandler(menuhandler);
        }

        public void ActivateOptionsHandler()
        {
            ActivateHandler(optionsHandler);
        }

        public void ActivateCredithandler()
        {
            ActivateHandler(creditHandler);
        }
        public void PlayGame()
        {

            GameManager.Instance.PlayGame();

        }


    }
}