using System;

namespace ExampleNamespace
{
    abstract class AbstractTemplate
    {
        public void ExecuteTemplate()
        {
            StepOne();
            RequiredStepOne();
            StepTwo();
            OptionalHookOne();
            RequiredStepTwo();
            StepThree();
            OptionalHookTwo();
        }

        protected void StepOne()
        {
            Console.WriteLine("AbstractTemplate says: Executing StepOne");
        }

        protected void StepTwo()
        {
            Console.WriteLine("AbstractTemplate says: Executing StepTwo");
        }

        protected void StepThree()
        {
            Console.WriteLine("AbstractTemplate says: Executing StepThree");
        }

        protected abstract void RequiredStepOne();

        protected abstract void RequiredStepTwo();

        protected virtual void OptionalHookOne() { }

        protected virtual void OptionalHookTwo() { }
    }

    class ConcreteTemplateA : AbstractTemplate
    {
        protected override void RequiredStepOne()
        {
            Console.WriteLine("ConcreteTemplateA says: Implemented RequiredStepOne");
        }

        protected override void RequiredStepTwo()
        {
            Console.WriteLine("ConcreteTemplateA says: Implemented RequiredStepTwo");
        }
    }

    class ConcreteTemplateB : AbstractTemplate
    {
        protected override void RequiredStepOne()
        {
            Console.WriteLine("ConcreteTemplateB says: Implemented RequiredStepOne");
        }

        protected override void RequiredStepTwo()
        {
            Console.WriteLine("ConcreteTemplateB says: Implemented RequiredStepTwo");
        }

        protected override void OptionalHookOne()
        {
            Console.WriteLine("ConcreteTemplateB says: Overridden OptionalHookOne");
        }
    }

    class Client
    {
        public static void UseTemplate(AbstractTemplate template)
        {
            template.ExecuteTemplate();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Client code can work with different subclasses:");

            Client.UseTemplate(new ConcreteTemplateA());

            Console.Write("\n");

            Console.WriteLine("Client code can work with different subclasses:");
            Client.UseTemplate(new ConcreteTemplateB());
        }
    }
}