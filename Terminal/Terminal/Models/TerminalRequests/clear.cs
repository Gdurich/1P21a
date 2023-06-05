using System;
using Terminal.Handlers;
using Terminal.Models.TerminalRequests.Base;

namespace Terminal.Models.TerminalRequests
{
    internal class CLEAR_TerminalRequest : TerminalRequest
    {
        public CLEAR_TerminalRequest()
        {
            CommandName = "clear";
        }

        public override void Execute(CommandHandler handler, string commandBody = "")
        {
            Console.Clear();
            base.Execute(handler, commandBody);
        }
    }
}
