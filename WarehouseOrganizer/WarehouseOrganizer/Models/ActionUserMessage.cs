using System;
using System.Collections.Generic;
using System.Text;

namespace WarehouseOrganizer.Models
{
    public class ActionUserMessage : InfoUserMessage
    {

        public String ButtonOkText { get; set; }

        public Action<bool> CallBack { get; set; }

    }
    
}
