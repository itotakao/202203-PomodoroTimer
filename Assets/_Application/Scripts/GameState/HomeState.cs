using System.Collections;
using UnityEngine;
using _Util.GameState;

namespace _Application
{
    public class HomeState : BaseState
    {
        public static HomeState Current { get; private set; }

        public enum Status
        {
            None = -1,
            Idle = 0,
            About = 1,
            Setting = 2,
            Start = 3,
            Stop = 4,
            Pause = 5,
            Restart = 6,
            Finish = 7
        }
        public Status CurrenStatus { get; private set; }

        public HomeState(StateMachine stateMachine) : base(stateMachine)
        {
            TransitionFunctions.Add(new TransitionFunction(Transition));
        }

        private BaseState Transition()
        {
            return (IsLoad ? LoadState(LoadStageId, StateMachine) : null);
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            Current = this;
            StageLoader.LoadStage(StageId.Home);

            StartCoroutine(CoStateEnter());
        }

        private IEnumerator CoStateEnter()
        {
            yield return new WaitUntil(() => StageLoader.IsLoadComplete);
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
        }

        public override void OnStateFixedUpdate()
        {
            base.OnStateFixedUpdate();
        }

        public override void OnStateLateUpdate()
        {
            base.OnStateLateUpdate();
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }

        public void SetStatus(Status status)
        {
            CurrenStatus = status;
            string eventName = "On" + status.ToString();
            AssetsFacade.InvokeUtilityEvnet(eventName);
        }
    }
}