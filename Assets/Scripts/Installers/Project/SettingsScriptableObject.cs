using Data.SettingCompanyData;
using UnityEngine;
using Zenject;

namespace Installers.Project
{
    public class SettingsScriptableObject : MonoInstaller<SettingsScriptableObject>
    {
        [SerializeField] private SettingCompanyDataBase settingCompany;
        
        public override void InstallBindings()
        {
            Container.Bind<ISettingCompanyDataBase>().FromInstance(settingCompany).AsSingle();
        }
    }
}