using System;
using Game.Dao.SavingSettingCompany;
using Game.Dao.SavingSettingCompany.SavingSettingCompanyService;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Game.Item
{
    public class ImprovementForCompanyHandler : MonoBehaviour
    {
        [SerializeField] private Button improvementForCompany;
        [SerializeField] private Text titleCompany;
        [SerializeField] private Text incomeCompany;
        [SerializeField] private Text priceCompany;
        [SerializeField] private Text priceName;
        
        private Action<int> _trackingImprovements;
        private int _id;
        private bool _isPurchased;
        private SettingCompany _settingCompany;
        private ISettingCompanyService _settingCompanyService;

        public Text TitleCompany => titleCompany;
        public Text IncomeCompany => incomeCompany;
        public Text PriceCompany => priceCompany;

        private void Start()
        {
            improvementForCompany.OnClickAsObservable().Subscribe(ActivateImprovement).AddTo(this);
        }

        private void ActivateImprovement(Unit value)
        {
            if (_isPurchased)
                return;
            
            if (_settingCompany.businessImprovement[_id].price > _settingCompanyService.GetBalance())
                return;
            
            _trackingImprovements.Invoke(_id);
            priceCompany.gameObject.SetActive(false);
            priceName.text = "Куплено";
            _isPurchased = true;
        }

        public double IsPurchased(bool isPurchased)
        {
            var price = isPurchased ? Convert.ToDouble(incomeCompany.text.Substring(0, incomeCompany.text.Length - 1)) : 0;
            return price;
        }

        public void TrackingActivationImprovements(int id, Action<int> trackingImprovements, bool isPurchased, 
            SettingCompany settingCompany, ISettingCompanyService settingCompanyService)
        {
            _id = id;
            _trackingImprovements = trackingImprovements;
            _isPurchased = isPurchased;
            
            _settingCompany = settingCompany;
            _settingCompanyService = settingCompanyService;

            if (isPurchased)
            {
                priceCompany.gameObject.SetActive(false);
                priceName.text = "Куплено";
            }
        }
    }
}