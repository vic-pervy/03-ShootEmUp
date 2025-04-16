using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace ShootEmUp
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private GameObject character;
        [SerializeField] private UIPresenter uiPresenter;
        [FormerlySerializedAs("worldContainer")] [SerializeField] private WorldData worldData;

        //[SerializeField] private Transform worldTransform;

        public override void InstallBindings()
        {
            foreach (var component in Resources.FindObjectsOfTypeAll<MonoBehaviour>().Where(c => c is IGameEvent)
                         .Cast<IGameEvent>())
            {
                var type = component.GetType();
                Container.Bind<IGameEvent>().To(type).FromInstance(component).AsCached();
            }
            
            Container.BindInstance(character).AsCached();
            Container.BindInstance(uiPresenter).AsCached();
            Container.BindInstance(worldData).AsSingle();

            Container.BindInterfacesAndSelfTo<InputManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<CharacterController>().AsSingle();
            Container.BindInterfacesAndSelfTo<UpdateSystem>().AsSingle();
            
            
            //
            
            // Debug.Log(Resources.FindObjectsOfTypeAll<MonoBehaviour>().Where(c => c is IGameEvent)
            //     .Cast<IGameEvent>().Count());
            // //Debug.Log(Container.Resolve<CharacterController>());
            // Debug.Log(Container.ResolveAll<IGameEvent>().Count);
        }
    }
}