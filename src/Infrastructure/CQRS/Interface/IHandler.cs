using System;
using System.Collections.Generic;
using System.Text;

namespace Cqrs.Interface
{
    public interface IListenerHandler
    {
        void Handle(string message);
    }
}
