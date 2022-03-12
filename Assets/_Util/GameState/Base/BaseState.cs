using Audio;
using UnityEngine;
using _Util;
using _Util.GameState;
using UnityEngine.SceneManagement;

namespace _Application
{
    public abstract class BaseState : GameState<StateMachine>
    {
        protected AudioManager AudioManager => AudioManager.Current;
        protected StageLoader StageLoader => StageLoader.Current;
        protected AssetsFacade AssetsFacade => AssetsFacade.Current;

        public static StageId LoadStageId { get; private set; } = StageId.None;
        public static bool IsLoad => (LoadStageId != StageId.None);

        public BaseState(StateMachine stateMachine) : base(stateMachine) { }

        public override void OnStateEnter()
        {
            Debug.Log("LoadState : " + GetType().Name);

            InitializeAllObjects();

            AssetsFacade.InvokeSetupAssets();
        }

        protected void InitializeAllObjects()
        {
            AudioManager.Voice.Stop();
            AudioManager.BGM.Stop();
            AudioManager.Ambient.Stop();
            AudioManager.SE.Stop();

            LoadStageId = StageId.None;
        }

        public override void OnStateUpdate()
        {
            if (AssetsFacade.Current)
            {
                AssetsFacade.InvokeSetupAssets();
            }
        }

        public override void OnStateFixedUpdate()
        {
            if (AssetsFacade.Current)
            {
                AssetsFacade.InvokeFixedUpdateAssets();
            }
        }

        public override void OnStateLateUpdate()
        {
            base.OnStateLateUpdate();

            if (AssetsFacade.Current)
            {
                AssetsFacade.InvokeLateUpdateAssets();
            }
        }

        public override void OnStateExit()
        {
            if (AssetsFacade.Current)
            {
                AssetsFacade.InvokExitAssets();
            }
        }

        protected void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public static void LoadStage(StageId stageId)
        {
            LoadStageId = stageId;
        }

        public static BaseState LoadState(StageId stageId, StateMachine stateMachine)
        {
            return stageId switch
            {
                StageId.None => new TemplateState(stateMachine),
                _ => throw new System.ComponentModel.InvalidEnumArgumentException(),
            };
        }
    }
}