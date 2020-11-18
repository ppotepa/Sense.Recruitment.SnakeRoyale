namespace Sense.Recruitment.SnakeRoyale.Engine.Services
{
    //CommandConstans
    public partial class InputStringCommandResolver : ICommandResolver<string>
    {
        private const string CommandString = "command";
        private const string Parameters = "Parameters";

        private const char Separator = ':';        
        private const char WhiteSpace = ' ';
        private const string DoubleSpace = "  ";

        private const string NotFoundExceptionMessage = 
                        "Invalid parameter Type \n" +
                        "Parameter Name:{0}\n" +
                        "Value:{1}\n" +
                        "Exptected:{2}";
    }
}
