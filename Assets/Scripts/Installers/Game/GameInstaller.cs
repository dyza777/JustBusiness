using Game.Dao.SavingSettingCompany.SavingSettingCompanyService.Impls;
using Game.Event.EventChange.Impls;
using Ui.Game;
using Ui.Game.Windows;
using Zenject;

namespace Installers.Game
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Bind();
            BindManagers();
            BindWindows();
        }

        private void Bind()
        {
            Container.BindInterfacesTo<EventChangeSystem>().AsSingle();
            
            Container.BindInterfacesTo<SettingCompanyService>().AsSingle();
        }

        private void BindManagers()
        { 
            Container.BindInterfacesTo<GameWindowManager>().AsSingle();
        }

        private void BindWindows()
        {
            Container.BindInterfacesAndSelfTo<HudWindow>().AsSingle();
        }
    }
}