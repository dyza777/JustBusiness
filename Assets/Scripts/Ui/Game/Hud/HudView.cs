using System.Collections.Generic;
using Game.Dao.SavingSettingCompany;
using Game.Dao.SavingSettingCompany.SavingSettingCompanyService;
using Game.Event.EventChange;
using SimpleUi.Abstracts;
using Ui.Game.Item;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Game.Hud
{
    public class HudView : UiView
    {
        [SerializeField] private Text money;
        //TODO: При создании использовать Стратегию для подмены префабов, Выделить сервис
        [SerializeField] private ItemView[] itemViews;

        public Text Money => money;
        
        //TODO: Создание нужно отделить
        public void Spawn(ISettingCompanyService settingCompanyService, IEventChangeSystem eventChangeSystem)
        {
            for (int i = 0; i < itemViews.Length; i++)
            {
                itemViews[i].SetSettings(i, settingCompanyService, eventChangeSystem);
            }
        }
    }
}