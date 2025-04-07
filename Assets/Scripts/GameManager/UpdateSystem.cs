using System.Collections.Generic;
using Zenject;

namespace ShootEmUp
{
    public class UpdateSystem : ITickable, IFixedTickable
    {
        private HashSet<IUpdate> updates = new HashSet<IUpdate>();
        private HashSet<IFixedUpdate> fixedUpdates = new HashSet<IFixedUpdate>();
        
        public void Tick()
        {
            foreach (var update in updates) update.Update();
        }

        public void FixedTick()
        {
            foreach (var fixedUpdate in fixedUpdates) fixedUpdate.FixedUpdate();
        }

        public void SubscribeObject(IGameEvent item)
        {
            if (item is IUpdate updateItem) updates.Add(updateItem);
            if (item is IFixedUpdate fixedUpdateItem) fixedUpdates.Add(fixedUpdateItem);
        }

        public void UnsubscribeObject(IGameEvent item)
        {
            if (item is IUpdate updateItem) updates.Remove(updateItem);
            if (item is IFixedUpdate fixedUpdateItem) fixedUpdates.Remove(fixedUpdateItem);
        }
    }
}