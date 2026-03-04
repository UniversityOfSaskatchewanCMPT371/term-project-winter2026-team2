public interface IExampleModel : IModel
{
    public int GetCount();
};

public class ExampleModel : Model
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

public interface IExampleController : IController { }

// generics are used to declare types, and typecasting (required)
public class ExampleController : Controller<IExampleModel, IExampleView>
{
    // init is called automatically by Start() built in the base class
    public override void Init()
    {
        // necessary to call these
        this.CheckModelRef();
        this.CheckViewRef();
        // you'll have access to:
        // this.modelInstance;              accessor to view component
        // this.modelInstance.GetCount();   accessor to model component
    }
}

public interface IExampleView : IView { }

// generics are used to declare types, and typecasting (required)
public class ExampleView : View<IExampleController>
{
    // init is called automatically by Start() built in the base class
    public override void Init()
    {
        // necessary to call this
        CheckControllerRef();
        // you'll have access to:
        // this.controllerInstance          accessor to controller component
    }
}