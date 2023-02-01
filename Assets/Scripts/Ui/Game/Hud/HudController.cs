using Game.Dao.SavingSettingCompany.SavingSettingCompanyService;
using Game.Event.EventChange;
using SimpleUi.Abstracts;
using UniRx;
using Zenject;

namespace Ui.Game.Hud
{
    public class HudController : UiController<HudView>, IInitializable
    {
        private readonly ISettingCompanyService _settingCompanyService;
        private readonly IEventChangeSystem _eventChangeSystem;

        public HudController(
            ISettingCompanyService settingCompanyService,
            IEventChangeSystem eventChangeSystem
        )
        {
            _settingCompanyService = settingCompanyService;
            _eventChangeSystem = eventChangeSystem;
        }

        public void Initialize()
        {
            View.Money.text = _settingCompanyService.GetBalance().ToString();
            View.Spawn(_settingCompanyService, _eventChangeSystem);

            _eventChangeSystem.BalanceTracking.Subscribe(OnChangeMoney).AddTo(View);
        }

        private void OnChangeMoney(Unit value)
        {
            View.Money.text = _settingCompanyService.GetBalance().ToString();
        }
    }
}