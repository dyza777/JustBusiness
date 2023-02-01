using Core.Utils.Dao.Impls;

namespace Game.Dao.SavingSettingCompany.Impls
{
    public class SavingSettingCompanyDao: ALocalStorageJsonDao<SavingSettingCompanyVo>, ISavingSettingCompanyDao
    {
        public SavingSettingCompanyDao(string fileName) : base(fileName)
        {
            
        }
    }
}