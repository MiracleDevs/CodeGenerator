using System;
using System.Collections.Generic;
using System.Linq;

namespace MiracleDevs.CodeGen.Logic.Translations
{
    public class ObjectDefinitions : IObjectDefinitions
    {
        #region Properties

        public Dictionary<Type, ObjectDefinition> Definitions { get; set; }

        #endregion

        #region Constructor

        public ObjectDefinitions()
        {
            this.Definitions = new Dictionary<Type, ObjectDefinition>();
        }

        #endregion

        #region Public Methods

        public void ProcessEntryPointType(Type type)
        {
            this.Definitions.Clear();
            this.GetDefinition(type);
        }

        public ObjectDefinition GetDefinition(Type type)
        {
            if (this.Definitions.ContainsKey(type))
                return this.Definitions[type];
          
            var newObjectDefinition = ObjectDefinitionFactory.Create(type);
            this.Definitions.Add(type, newObjectDefinition);
            newObjectDefinition.Process();

            return newObjectDefinition;
        }

        public IEnumerable<ObjectDefinition> Find(Func<ObjectDefinition, bool> predicate)
        {
            return this.Definitions.Values.Where(predicate);
        }

        #endregion
    }
}