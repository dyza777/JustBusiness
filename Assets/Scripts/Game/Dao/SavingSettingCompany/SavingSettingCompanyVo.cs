using System;
using System.Collections.Generic;

namespace Game.Dao.SavingSettingCompany
{
    [Serializable]
    public class SavingSettingCompanyVo
    {
        public double balance = 0;
        public List<SettingCompany> settingCompany;
    }

    [Serializable]
    public class SettingCompany
    {
        public string nameCompany;        //Имя
        public int revenueDelay;          //Задержка дохода
        public double basicCost;           //Базовая стоимость
        public double basicIncome;         //Базовый доход
        public int currentLevel;          //Текущий уровень
        
        //TODO: Можно выделить в отдельный ScriptableObject. Очень удобно если контролировать через гугл таблицу
        public List<BusinessImprovement> businessImprovement;

        public SettingCompany(
            string nameCompany, 
            int revenueDelay, 
            double basicCost, 
            double basicIncome, 
            int currentLevel,
            List<BusinessImprovement> businessImprovement
        )
        {
            this.nameCompany = nameCompany;
            this.revenueDelay = revenueDelay;
            this.basicCost = basicCost;
            this.basicIncome = basicIncome;
            this.currentLevel = currentLevel;
            this.businessImprovement = businessImprovement;
        }
    }

    [Serializable]
    public class BusinessImprovement
    {
        public string titleName;   
        public double price;   
        public double income; 
        public bool isPurchased; 
    }
}