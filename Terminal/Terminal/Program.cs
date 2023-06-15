using Terminal.Handlers;
using Terminal.Models.TerminalRequests;

CommandHandler commandHandler = new CommandHandler();
Init();

while (true)
{
    Console.Write($"_{CommandHandler.CurrentDirectoryPath}>");
    string commandString = Console.ReadLine();
    if (commandString != null && commandString != "")
        commandHandler.ExecuteCommand(commandString);
}

void Init()
{
    commandHandler.AddCommandObject(new CD_TerminalRequest());
    // commandHandler.AddCommandObject(new Mkdir_TerminalRequest());
    // commandHandler.AddCommandObject(new Zip_TerminalRequest());
    commandHandler.AddCommandObject(new Extract_TerminalRequest());
    commandHandler.AddCommandObject(new Copy_TerminalRequest());
}

