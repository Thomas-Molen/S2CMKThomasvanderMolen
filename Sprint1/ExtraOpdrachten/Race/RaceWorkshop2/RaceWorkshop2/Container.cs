using System;
using System.Collections.Generic;
using System.Text;

namespace RaceWorkshop2
{
    public class Container<T>
    {
        private List<T> entities;

        public Container()
        {
            entities = new List<T>();
        }

        public void Add(T NewEntity)
        {
            entities.Add(NewEntity);
        }

        public void Remove(T EntityToRemove)
        {
            if (!entities.Contains(EntityToRemove))
            {
                throw new ArgumentOutOfRangeException(string.Format("{0} not found in container<{0}: {1} ", EntityToRemove.GetType(), EntityToRemove.ToString()));
            }

            entities.Remove(EntityToRemove);
        }

        public IEnumerable<T> GetAllTeams()
        {
            return entities.AsReadOnly();
        }
    }
}
