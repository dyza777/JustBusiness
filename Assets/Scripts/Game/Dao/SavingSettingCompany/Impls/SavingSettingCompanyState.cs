using System.Collections.Generic;
using Core.Utils.Dao;
using Zenject;

namespace Game.Dao.SavingSettingCompany.Impls
{
    public class SavingSettingCompanyState: APersistenceState<ISavingSettingCompanyDao, SavingSettingCompanyVo>, ISavingSettingCompanyState, IInitializable
    {
        private SavingSettingCompanyVo _vo;
        private List<SettingCompany> _settingCompany;

        public SavingSettingCompanyState(ISavingSettingCompanyDao dao) : base(dao)
        {
        }

        public void Initialize()
        {
            _settingCompany = new List<SettingCompany>();

            if (!Dao.Exist())
            {
                _vo = new SavingSettingCompanyVo();
                SetDirty();
                return;
            }
            
            _vo = Dao.Load();
            
            foreach (var company in _vo.settingCompany)
            {
                _settingCompany.Add(company);
            }
        }

        public double GetBalance() 
            => _vo.balance;

        public void UpdateBalance(double value)
        {
            _vo.balance = value;
            SetDirty();
        }

        public List<SettingCompany> GetSettingCompany() 
            => _settingCompany;

        public void SetSettingCompany(List<SettingCompany> settingCompany)
        {
            SettingCompany(settingCompany);
            
            _vo.settingCompany = _settingCompany;
            SetDirty();
        }

        private void SettingCompany(List<SettingCompany> settingCompany)
        {
            for (int i = 0; i < settingCompany.Count; i++)
            {
                if (_settingCompany.Count-1 >= i)
                    continue;
                
                _settingCompany.Add(settingCompany[i]);
            }
        }

        public void UpdateSettingCompany(int id, SettingCompany settingCompany)
        {
            _settingCompany[id] = settingCompany;
            _vo.settingCompany[id] = _settingCompany[id];
            SetDirty();
        }
        
        protected override SavingSettingCompanyVo ConvertToState() => _vo;
    }
}