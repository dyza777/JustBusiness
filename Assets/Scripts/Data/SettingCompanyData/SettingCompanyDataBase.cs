using System.Collections.Generic;
using Game.Dao.SavingSettingCompany;
using UnityEngine;

namespace Data.SettingCompanyData
{
    public interface ISettingCompanyDataBase
    {
        List<SettingCompany> SettingCompany { get; }
    }

    //TODO: В будущем переделать на загрузчик с гугл документов
    [CreateAssetMenu(fileName = "SettingCompanyDataBase", menuName = "Settings/SettingCompanyDataBase", order = 0)]
    public class SettingCompanyDataBase : ScriptableObject, ISettingCompanyDataBase
    {
        [SerializeField] private List<SettingCompany> setting;

        public List<SettingCompany> SettingCompany => setting;
    }
}