
using System.Runtime.CompilerServices;

public interface ITryModel : IModel
{
    public int GetCount();
};

public class TryModel : Model 
{
    // nothing really new here because model class is simple enough
    // it will force you to make Init() method
    private int count = 1;

    public int GetCount()
    {
        return count;
    }

    // init is called automatically by Start() built in the base class
    public override void Init()
    {
        
    }    
}

public interface ITryController : IController
{
    
}

// generics are used to declare types, and typecasting (required)
public class TryController : Controller<ITryModel, ITryView>
{
    // init is called automatically by Start() built in the base class
    public override void Init()
    {
        // you'll have access to:
        // this.modelInstance;              accessor to view component
        // this.modelInstance.GetCount();   accessor to model component
    }    
}

public interface ITryView : IView
{
    
}

// generics are used to declare types, and typecasting (required)
public class TryView : View<ITryController>
{
    // init is called automatically by Start() built in the base class
    public override void Init()
    {
        // you'll have access to:
        // this.controllerInstance          accessor to controller component
    }    
}