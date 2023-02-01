using System;
using System.Collections.Generic;
using Data.SettingCompanyData;
using Game.Event.EventChange;
using UniRx;
using Zenject;

namespace Game.Dao.SavingSettingCompany.SavingSettingCompanyService.Impls
{
    public class SettingCompanyService : ISettingCompanyService, IInitializable
    {
        private readonly ISavingSettingCompanyState _savingSettingCompanyState;
        private readonly ISettingCompanyDataBase _settingCompanyDataBase;
        private readonly IEventChangeSystem _eventChangeSystem;

        public SettingCompanyService(
            ISavingSettingCompanyState savingSettingCompanyState,
            ISettingCompanyDataBase settingCompanyDataBase,
            IEventChangeSystem eventChangeSystem
        )
        {
            _savingSettingCompanyState = savingSettingCompanyState;
            _settingCompanyDataBase = settingCompanyDataBase;
            _eventChangeSystem = eventChangeSystem;
        }

        public void Initialize()
        {
            _eventChangeSystem.ChangeMoney.Subscribe(UpdateBalance);
        }

        private void UpdateBalance(double value)
        {
            var balance = Math.Round(_savingSettingCompanyState.GetBalance() + value, 2);
            _savingSettingCompanyState.UpdateBalance(balance);
            _eventChangeSystem.BalanceTracking.Execute();
        }

        public List<SettingCompany> GetSettingCompany()
        {
            CreatingCompanies();
            return _savingSettingCompanyState.GetSettingCompany();
        }

        public double GetBalance() 
            => _savingSettingCompanyState.GetBalance();

        public void UpdateSettingCompany(int id, SettingCompany settingCompany) 
            => _savingSettingCompanyState.UpdateSettingCompany(id, settingCompany);

        private void CreatingCompanies() 
            => _savingSettingCompanyState.SetSettingCompany(_settingCompanyDataBase.SettingCompany);
    }
}