// using UnityEngine;
// using VContainer;
// using VContainer.Unity;
//
// namespace ShootEmUp
// {
//     public class GameLifetimeScope : LifetimeScope
//     {
//         [SerializeField]
//         KeyboardInputConfig keyboardInputConfig;
//         [SerializeField]
//         BulletConfig bulletConfig;
//         
//         protected override void Configure(IContainerBuilder builder)
//         {
//             builder.RegisterComponent(keyboardInputConfig).AsImplementedInterfaces();
//             
//             builder.RegisterComponent(bulletConfig).AsSelf();
//         }
//     }
// }