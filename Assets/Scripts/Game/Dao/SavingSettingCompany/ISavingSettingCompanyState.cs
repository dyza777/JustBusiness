using System.Collections.Generic;

namespace Game.Dao.SavingSettingCompany
{
    public interface ISavingSettingCompanyState
    {
        double GetBalance();
        void UpdateBalance(double value);
        
        List<SettingCompany> GetSettingCompany();
        void SetSettingCompany(List<SettingCompany> settingCompany);
        void UpdateSettingCompany(int id, SettingCompany settingCompany);
    }
}