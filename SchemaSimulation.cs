using SPZCapstoneVar2.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SPZCapstoneVar2
{
    public class SchemaSimulation
    {
        private readonly List<Element> _elements;
        private readonly List<Connection> _connections;

        public SchemaSimulation(List<Element> elements, List<Connection> connections)
        {
            _elements = elements;
            _connections = connections;
        }

        public bool? CalculateValueFor(Element targetElement)
        {
            switch (targetElement.Type)
            {
                case ElementType.OUTPUT_ELEMENT:
                {
                    var relatedInputId = _connections.FirstOrDefault(connection => connection.ToId == targetElement.Id)?.FromId;
                    if (relatedInputId == null)
                    {
                        return null;
                    }
                    var relatedInput = _elements.First(element => element.Id == relatedInputId);
                    return CalculateValueFor(relatedInput);
                }
                case ElementType.INPUT_ELEMENT: return targetElement.InputElementValue;
                default: throw new NotImplementedException();
            }
        }
    }
}
