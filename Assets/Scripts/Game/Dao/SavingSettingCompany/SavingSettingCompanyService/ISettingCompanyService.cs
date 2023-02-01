using System.Collections.Generic;

namespace Game.Dao.SavingSettingCompany.SavingSettingCompanyService
{
    public interface ISettingCompanyService
    {
        List<SettingCompany> GetSettingCompany();
        double GetBalance();
        void UpdateSettingCompany(int id, SettingCompany settingCompany);
    }
}