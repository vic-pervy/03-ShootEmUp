using System.Linq;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private GameObject character;
        [SerializeField] private UIPresenter uiPresenter;

        [SerializeField] private Transform worldTransform;

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

            Container.BindInstance(worldTransform).WithId("worldTransform");

            Container.BindInterfacesAndSelfTo<InputManager>().AsCached();
            Container.BindInterfacesAndSelfTo<GameManager>().AsCached();
            Container.BindInterfacesAndSelfTo<CharacterController>().AsCached();
            Container.BindInterfacesAndSelfTo<UpdateSystem>().AsCached();
            
            
            //
            
            // Debug.Log(Resources.FindObjectsOfTypeAll<MonoBehaviour>().Where(c => c is IGameEvent)
            //     .Cast<IGameEvent>().Count());
            // //Debug.Log(Container.Resolve<CharacterController>());
            // Debug.Log(Container.ResolveAll<IGameEvent>().Count);
        }
    }
}