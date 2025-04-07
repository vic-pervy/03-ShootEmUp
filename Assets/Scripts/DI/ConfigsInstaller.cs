using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    [CreateAssetMenu(fileName = "ConfigInstaller", menuName = "Installers/ConfigInstaller")]
    public class ConfigsInstaller : ScriptableObjectInstaller<ConfigsInstaller>
    {
        [SerializeField] KeyboardInputConfig keyboardInputConfig;

        public override void InstallBindings()
        {
            Container.Bind<IInputConfig>().FromInstance(keyboardInputConfig).AsSingle();
        }
    }
}