using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace OOP
{
    class Program
    {

        // Паттерн Строитель
        // Позволяет создавать сложные объекты пошагово
        static void StartBuilder()
        {
            var director = new Director();
            var builder = new ConcreteBuilder();
            director.Builder = builder;
            
            Console.WriteLine("Standard basic product:");
            director.BuildMinimalViableProduct();
            Console.WriteLine(builder.GetProduct().ListParts());

            Console.WriteLine("Standard full featured product:");
            director.BuildFullFeaturedProduct();
            Console.WriteLine(builder.GetProduct().ListParts());
            
            Console.WriteLine("Custom product:");
            builder.BuildPartA();
            builder.BuildPartC();
            Console.Write(builder.GetProduct().ListParts());
        }


        // Паттерн Адаптер
        // Позволяет объектам с несовместимыми интерфейсами работать вместе
        static void StartAdapter()
        {
            Adaptee adaptee = new();
            ITarget target = new Adapter(adaptee);

            Console.WriteLine("Adaptee interface is incompatible with the client.");
            Console.WriteLine("But with adapter client can call it's method.");

            Console.WriteLine(target.GetRequest());
        }


        // Паттерн Прототип
        // Позволяет копировать объекты не вдаваясь в подробности их реализации
        [Obsolete]
        static void StartPrototype(){

            Prototype.Person p1 = new();
            p1.Age = 42;
            p1.BirthDate = Convert.ToDateTime("1977-01-01");
            p1.Name = "Jack Daniels";
            p1.IdInfo = new Prototype.IdInfo(666);
            
            Prototype.Person p2 = p1.ShallowCopy();
            Prototype.Person p3 = p1.DeepCopy();
            
            Console.WriteLine("Original values of p1, p2, p3:");
            Console.WriteLine("   p1 instance values: ");
            DisplayValues(p1);
            Console.WriteLine("   p2 instance values:");
            DisplayValues(p2);
            Console.WriteLine("   p3 instance values:");
            DisplayValues(p3);
            
            p1.Age = 32;
            p1.BirthDate = Convert.ToDateTime("1900-01-01");
            p1.Name = "Frank";
            p1.IdInfo.IdNumber = 7878;
            Console.WriteLine("\nValues of p1, p2 and p3 after changes to p1:");
            Console.WriteLine("   p1 instance values: ");
            DisplayValues(p1);
            Console.WriteLine("   p2 instance values (reference values have changed):");
            DisplayValues(p2);
            Console.WriteLine("   p3 instance values (everything was kept the same):");
            DisplayValues(p3);
            
            static void DisplayValues(Prototype.Person p)
            {
                Console.WriteLine("      Name: {0:s}, Age: {1:d}, BirthDate: {2:MM/dd/yy}",
                    p.Name, p.Age, p.BirthDate);
                Console.WriteLine("      ID#: {0:d}", p.IdInfo.IdNumber);
            }
        }

        
        // Паттерн Декоратор
        // Позволяет динамически добавлять объектам новую функциональность
        static void StartDecorator()
        {
            Client client = new();

            var simple = new ConcreteComponent();
            Console.WriteLine("Client: I get a simple component:");
            client.ClientCode(simple);
            Console.WriteLine();

            ConcreteDecoratorA decorator1 = new(simple);
            ConcreteDecoratorB decorator2 = new(decorator1);
            Console.WriteLine("Client: Now I've got a decorated component:");
            client.ClientCode(decorator2);
        }
        
        
        // Паттерн Одиночка
        // Гарантирует что у класса только один экземпляр и он представляет к нему
        // глобальную точку доступа
        static void StartSingleton()
        {
            Singleton s1 = Singleton.GetInstance();
            Singleton s2 = Singleton.GetInstance();

            if (s1 == s2)
            {
                Console.WriteLine("Singleton works, both variables contain the same instance.");
            }
            else
            {
                Console.WriteLine("Singleton failed, variables contain different instances.");
            }
        }

        // Паттерн Фасад
        // Представляет простой интерфейс к сложной системе классов, библиотеке или фреймворку
        static void StartFacade()
        {
            Subsystem1 subsystem1 = new();
            Subsystem2 subsystem2 = new();
            Facade facade = new(subsystem1, subsystem2);
            Client.ClientCode(facade);
        }

        // Паттерн Наблюдатель
        // Позволяет одним объектам следить и реагировать на события,
        // происходящие в других объектах    
        static void StartObserver()
        {
            var subject = new Subject();
            var observerA = new ConcreteObserverA();
            subject.Attach(observerA);

            var observerB = new ConcreteObserverB();
            subject.Attach(observerB);

            subject.SomeBusinessLogic();
            subject.SomeBusinessLogic();

            subject.Detach(observerB);

            subject.SomeBusinessLogic();
        }

        
        // Паттерн Посредник
        // Позволяет уменьшить связанность множества классов между собой
        static void StartMediator()
        {
            Component1 component1 = new Component1();
            Component2 component2 = new Component2();
            new ConcreteMediator(component1, component2);

            Console.WriteLine("Client triggets operation A.");
            component1.DoA();

            Console.WriteLine();

            Console.WriteLine("Client triggers operation D.");
            component2.DoD();
        }

        [Obsolete]
        static void Main(string[] args)
        {
            Console.WriteLine("Select Pattern:\n1-Adapter    2-Builder\n" +
                              "3-Decorator  4-Facade\n5-Mediator   " +
                              "6-Observer\n7-Prototype  8-Singleton\n");
            int i = 0;
            try
            {
                i = Int16.Parse(Console.ReadLine());
            }
            catch(Exception e) { Console.WriteLine(e); }
            Console.WriteLine();
            
            switch (i)
            {
                case 1:
                    StartAdapter(); break;
                case 2:
                    StartBuilder(); break;
                case 3:
                    StartDecorator(); break;
                case 4:
                    StartFacade(); break;
                case 5:
                    StartMediator(); break;
                case 6:
                    StartObserver(); break;
                case 7:
                    StartPrototype(); break;
                case 8:
                    StartSingleton(); break;
                default:
                    Console.WriteLine("This case not found");
                    break;
            }
        }
    }
}
