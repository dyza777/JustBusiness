using Core.Services.Scenes.Impls;
using Game.Dao.SavingSettingCompany.Impls;
using UnityEngine;
using Zenject;

namespace Installers.Project
{
    public class ProjectInstallers : MonoInstaller
    {
        public override void InstallBindings()
        {
            SetSettings();
            BindServices();
            BindDao();
        }

        private void SetSettings()
        {
            Application.targetFrameRate = 60;
            SignalBusInstaller.Install(Container); 
        }

        private void BindServices()
        {
            Container.BindInterfacesTo<SceneService>().AsSingle();
        }
        
        private void BindDao()
        {
            Container.BindInterfacesTo<SavingSettingCompanyDao>().AsSingle()
                .WithArguments("SavingSettingCompany.json");
            
            Container.BindInitializableExecutionOrder<SavingSettingCompanyState>(-10000);
            Container.BindInterfacesTo<SavingSettingCompanyState>().AsSingle();
        }
    }
}