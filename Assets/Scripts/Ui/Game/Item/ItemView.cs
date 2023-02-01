using System;
using System.Collections.Generic;
using DG.Tweening;
using Game.Dao.SavingSettingCompany;
using Game.Dao.SavingSettingCompany.SavingSettingCompanyService;
using Game.Event.EventChange;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Game.Item
{
    //TODO: Хорошое решение создавать ItemView через контейнер Zenject 
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private Button lvlUp;
        [SerializeField] private Text title;
        [SerializeField] private Scrollbar scrollbar;
        [SerializeField] private Text lvlValue;
        [SerializeField] private Text incomeValue;
        [SerializeField] private Text lvlUpPrice;
        
        //TODO: Необходимо создавать в зависимости от количество улучшений 
        [SerializeField] private List<ImprovementForCompanyHandler> improvementForCompanyHandler;

        private Tween _animation;

        private SettingCompany _settingCompany;
        private ISettingCompanyService _settingCompanyService;
        private IEventChangeSystem _eventChangeSystem;
        private List<BusinessImprovement> _businessImprovement;
        private double _incomeValue;
        private double _lvlUpPrice;
        private int _id;

        private void Start()
        {
            lvlUp.OnClickAsObservable().Subscribe(UpLvl).AddTo(this);
        }

        private void UpLvl(Unit value)
        {
            if (_lvlUpPrice > _settingCompanyService.GetBalance())
                return;

            if (_settingCompany.currentLevel <= 0)
            {
                LogicLvlChange();
                StartAnimation(true, _settingCompany.revenueDelay);
                return;
            }

            LogicLvlChange();
        }

        private void LogicLvlChange()
        {
            _eventChangeSystem.ChangeMoney.Execute(-_lvlUpPrice);
            _settingCompanyService.UpdateSettingCompany(_id, ChangeSettingLvlCompany(++_settingCompany.currentLevel));
            CalculatingBalance(_settingCompany);
        }
        
        private void ImprovementTrackingLogic(int id)
        {
            _eventChangeSystem.ChangeMoney.Execute(-_businessImprovement[id].price);
            _settingCompanyService.UpdateSettingCompany(_id, TrackingImprovementsCompany());
            CalculatingBalance(_settingCompany);
        }

        private SettingCompany ChangeSettingLvlCompany(int currentLevel)
        {
            return new SettingCompany(
                _settingCompany.nameCompany,
                _settingCompany.revenueDelay,
                _settingCompany.basicCost,
                BasicIncome(),
                currentLevel,
                _settingCompany.businessImprovement
                );
        }
        
        private SettingCompany TrackingImprovementsCompany()
        {
            return new SettingCompany(
                _settingCompany.nameCompany,
                _settingCompany.revenueDelay,
                _settingCompany.basicCost,
                _settingCompany.basicIncome,
                _settingCompany.currentLevel,
                _businessImprovement
                );
        }

        public void SetSettings(int id, ISettingCompanyService settingCompanyService, IEventChangeSystem eventChangeSystem)
        {
            _id = id;
            _settingCompanyService = settingCompanyService;
            _settingCompany = settingCompanyService.GetSettingCompany()[id];
            _eventChangeSystem = eventChangeSystem;
            title.text = _settingCompany.nameCompany;

            _businessImprovement = _settingCompany.businessImprovement;
            for (int i = 0; i < improvementForCompanyHandler.Count; i++)
            {
                improvementForCompanyHandler[i].TitleCompany.text = _businessImprovement[i].titleName;
                improvementForCompanyHandler[i].PriceCompany.text = _businessImprovement[i].price.ToString() + "$";
                improvementForCompanyHandler[i].IncomeCompany.text = "+" + _businessImprovement[i].income.ToString() + "%";
                improvementForCompanyHandler[i].TrackingActivationImprovements(i, TrackingImprovements, _businessImprovement[i].isPurchased, _settingCompany, _settingCompanyService);
            }
            
            CalculatingBalance(_settingCompany);

            var active = _settingCompany.currentLevel >= 1;
            StartAnimation(active, _settingCompany.revenueDelay);
        }

        private void TrackingImprovements(int value)
        {
            _businessImprovement[value].isPurchased = true;
            
            ImprovementTrackingLogic(value);
        }

        private void CalculatingBalance(SettingCompany settingCompany)
        {
            lvlValue.text = settingCompany.currentLevel.ToString();
            
            incomeValue.text = BasicIncome().ToString() + "$";
            
            _lvlUpPrice = Math.Round((settingCompany.currentLevel + 1) * settingCompany.basicCost, 2);
            lvlUpPrice.text = _lvlUpPrice.ToString() + "$";
        }

        private double BasicIncome()
        {
            //TODO: По хорошему математику тоже бы вынести в Helper или что-то подобное (а еще эта строчка чет срашная =) )
            _incomeValue = Math.Round(_settingCompany.currentLevel * _settingCompany.basicCost * 
                                      (1 + (improvementForCompanyHandler[0].IsPurchased(_businessImprovement[0].isPurchased) 
                                            +  improvementForCompanyHandler[1].IsPurchased(_businessImprovement[1].isPurchased))/100), 2);
            return _incomeValue;
        }
        
        private void StartAnimation(bool active, int duration)
        {
            if (!active)
                return;

            OnComplete(duration);
        }

        private void OnComplete(int duration)
        {
            _animation = DOTween.To(Setter, scrollbar.size, 1, duration)
                .OnComplete(() =>
                {
                    Complete();
                    OnComplete(duration);
                });
        }

        private void Setter(float pnewvalue) 
            => scrollbar.size = pnewvalue;

        private void Complete()
        {
            scrollbar.size = 0;
            _eventChangeSystem.ChangeMoney.Execute(_incomeValue);
        }
    }
}
