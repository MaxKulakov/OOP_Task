namespace OOP
{
    // Целевой класс объявляет интерфейс, с которым может работать
    // клиентский код
    public interface ITarget
    {
        string GetRequest();
    }
    
    // Адаптируемый класс содержит некоторое полезное поведение, но его
    // интерфейс несовместим  с существующим клиентским кодом. Адаптируемый
    // класс нуждается в некоторой доработке,  прежде чем клиентский код сможет
    // его использовать
    class Adaptee
    {
        public string GetSpecificRequest()
        {
            return "Specific request.";
        }
    }
    
    // Адаптер делает интерфейс Адаптируемого класса совместимым с целевым
    // интерфейсом
    class Adapter : ITarget
    {
        private readonly Adaptee _adaptee;

        public Adapter(Adaptee adaptee)
        {
            this._adaptee = adaptee;
        }

        public string GetRequest()
        {
            return $"This is '{this._adaptee.GetSpecificRequest()}'";
        }
    }
    
}