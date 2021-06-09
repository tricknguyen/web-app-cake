using System;
using System.Collections.Generic;
using System.Text;
using Braintree;
namespace Rocky_Utility.BrainTree
{
    public interface IBrainTreeGate
    {
        IBraintreeGateway CreateGateway();
        IBraintreeGateway GetGateway();
    }
}
