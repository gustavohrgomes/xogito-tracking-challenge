using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Tracking.Domain.Exceptions;

[Serializable]
internal class InvalidProductQuantityException : Exception
{
    public InvalidProductQuantityException(string message) : base(message)
    { }
}
