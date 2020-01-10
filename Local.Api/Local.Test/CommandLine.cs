using McMaster.Extensions.CommandLineUtils;

namespace Local.Test
{
    public class CommandLine
     {
         public void Run(string[] args)
         {
             CommandLineApplication app = new CommandLineApplication(false);
             app.HelpOption("-?|-h|--help");
             app.OnExecute(() =>
             {
                 app.ShowHelp();
                 return 0;
             });
             app.Command("trans", command =>
             {
                 //var args1 = new[] { "Arg1", "arg with space", "args ' with \" quotes" };
                 //Process.Start("echo", ArgumentEscaper.EscapeAndConcatenate(args1));
                 string password = Prompt.GetPassword("please input your password: ");
                 //Process.Start(DotNetExe.FullPathOrDefault(), "run");
                 CommandArgument argument = command.Argument("[name]", "", multipleValues: true);
                 CommandOption option = command.Option("-t", "this is a template", CommandOptionType.NoValue);
                 command.OnExecute(() =>
                 {
                     if (option.Value() == "-t")
                     {
                         bool isRun = Prompt.GetYesNo("confirm your transaction, do your want to continue:", false);
                         if (!isRun)
                         {
                             return;
                         }
                         command.Out.WriteLine($"密码是{password}, 参数是：{argument}");
                         return;
                     }
                 });
             });
             app.Execute(args);
         }
     }
}
