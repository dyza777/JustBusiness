using UniRx;

namespace Game.Event.EventChange
{
    public interface IEventChangeSystem
    {
        ReactiveCommand<double> ChangeMoney { get; }
        ReactiveCommand BalanceTracking { get; }
    }
}