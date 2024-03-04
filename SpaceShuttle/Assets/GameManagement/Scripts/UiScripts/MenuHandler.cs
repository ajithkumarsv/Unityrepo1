using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GM
{
    public class MenuHandler : Handler
    {

        MenuController menuController { get { return MenuController.Instance; } }

        private void Start()
        {

        }

        public override void Init()
        {
            base.Init();
        }

        public override void DeInit()
        {
            base.DeInit();
        }


        public void ActivateOptionshandler()
        {
            menuController.ActivateOptionsHandler();
        }
        public void ActivateCreditHandler()
        {

            menuController.ActivateCredithandler();
        }

        public void PlayGame()
        {
            menuController.PlayGame();
        }




    }
}