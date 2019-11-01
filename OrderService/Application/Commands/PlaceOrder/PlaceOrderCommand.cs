using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.PlaceOrder
{
    public class PlaceOrderCommand : Command
    {
        public PlaceOrderDTO Order { get; set; }

        public PlaceOrderCommand() {

        }

        public override void Execute()
        {
            
        }
    }
}
