namespace TestPong.Game
{
    using TestPong.World;
    using UnityEngine;
    using Zenject;

    public sealed class PongGameContextInstaller : MonoInstaller
    {
        [Header("World")]
        [SerializeField] private WorldArea _gameWorldArea;

        public override void InstallBindings()
        {
            BindSceneObjects();
        }

        private void BindSceneObjects()
        {
            Container.Bind<WorldArea>().FromInstance(_gameWorldArea).AsSingle();
        }
    }
}
