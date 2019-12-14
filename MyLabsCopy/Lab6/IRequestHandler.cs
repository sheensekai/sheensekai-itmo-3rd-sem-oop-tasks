using System;
using System.Collections.Generic;
using System.Text;
using MyLabsCopy.Lab6.Requests;

namespace MyLabsCopy.Lab6
{
    interface IRequestHandler
    {
        void HandleRequest(IRequest request);
    }
}
