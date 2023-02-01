using UniRx;

namespace Game.Event.EventChange.Impls
{
    public class EventChangeSystem : IEventChangeSystem
    {
        public ReactiveCommand<double> ChangeMoney { get; } = new ReactiveCommand<double>();
        public ReactiveCommand BalanceTracking { get; } = new ReactiveCommand();
    }
}