namespace Sense.Recruitment.SnakeRoyale.Engine
{
    public partial class SimpleGameEngine
    {
        public SimpleGameEngine LoadAssets(string configFileName = "game.assets.json")
        {
            //EMPTY 
            return this;
        }
        public SimpleGameEngine LoadStages(string configFileName = "game.stages.json")
        {
            //EMPTY 
            return this;
        }
        public SimpleGameEngine LoadConfiguration(string configFileName = "engine.config.json")
        {
            //EMPTY 
            return this;
        }
        public SimpleGameEngine LoadDefaultObjects(string configFileName = "game.objects.json")
        {
            //EMPTY 
            return this;
        }

        public void UseDefaultLogic() => Server.UseDefaultLogic = true;

        public SimpleGameEngine Initialize() { return this; }
        
    }
}
