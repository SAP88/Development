using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public static class Helper
    {
        /// <summary>
        /// Подсвечиваем выделенную башню
        /// </summary>
        /// <param name="tower"></param>
        public static void ApplySelected(MonoBehaviour tower)
        {
            //foreach(VisibilityScript obj in tower.GetComponentsInChildren<VisibilityScript>())
            //{
            //    obj.Visible();
            //}

            foreach (Transform obj in tower.transform)
            {
                obj.gameObject.SetActive(true);
            }
        }

        /// <summary>
        /// Снять выделение
        /// </summary>
        /// <param name="tower"></param>
        public static void ApplyDeselected(MonoBehaviour tower)
        {
            //foreach (VisibilityScript obj in tower.GetComponentsInChildren<VisibilityScript>())
            //{
            //    obj.Hide();
            //}

            foreach (Transform obj in tower.transform)
            {
                obj.gameObject.SetActive(false);
            }
        }


        public const int MOVEABLESPACE = 0;
        public const int PLACEABLESPACE = 1;
        public const int RESPAWN = 100;
        public const int ESCAPE = 200;
    }
}
