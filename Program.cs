using System;

namespace LabWork
{
    // Даний проект є шаблоном для виконання лабораторних робіт
    // з курсу "Об'єктно-орієнтоване програмування та патерни проектування"
    // Необхідно змінювати і дописувати код лише в цьому проекті
    // Відео-інструкції щодо роботи з github можна переглянути 
    // за посиланням https://www.youtube.com/@ViktorZhukovskyy/videos 
    class Program
    {
        static void Main(string[] args)
        {
            // Демонстрація побудови комп'ютерної гри за допомогою патерну Builder
            var builder = new GameBuilder();

            // 1) Побудова інді-гри крок за кроком
            var indieGame = builder
                .SetGraphics("Pixel Art")
                .SetSound("Chiptune")
                .SetStoryline("Невелика пригода в містечку")
                .Build();

            Console.WriteLine("Indie Game:");
            Console.WriteLine(indieGame);
            Console.WriteLine();

            // 2) Використаємо Director для побудови готових пресетів
            var director = new GameDirector();
            var aaaGame = director.BuildAAAStudioGame(new GameBuilder());
            var mobileGame = director.BuildMobileCasualGame(new GameBuilder());

            Console.WriteLine("AAA Studio Game:");
            Console.WriteLine(aaaGame);
            Console.WriteLine();

            Console.WriteLine("Mobile Casual Game:");
            Console.WriteLine(mobileGame);
            Console.WriteLine();

            // Перевірка інкапсуляції: властивості гри доступні для читання,
            // але не можуть бути змінені ззовні (тільки через Builder).
            // Наступний рядок зкоментовано, бо він не компілюється (публічний геттер, приватний сеттер):
            // indieGame.Graphics = "New Graphics"; // помилка компіляції
        }
    }

    // Проста модель гри з інкапсульованими (тільки для читання) властивостями
    public class Game
    {
        public string Graphics { get; }
        public string Sound { get; }
        public string Storyline { get; }

        internal Game(string graphics, string sound, string storyline)
        {
            Graphics = graphics;
            Sound = sound;
            Storyline = storyline;
        }

        public override string ToString()
        {
            return $"Graphics: {Graphics}\nSound: {Sound}\nStoryline: {Storyline}";
        }
    }

    // Builder для крокового створення об'єкта Game
    public class GameBuilder
    {
        private string _graphics = "Default Graphics";
        private string _sound = "Default Sound";
        private string _storyline = "Default Storyline";

        // Відкриті ланцюжкові методи для конфігурації
        public GameBuilder SetGraphics(string graphics)
        {
            _graphics = graphics ?? throw new ArgumentNullException(nameof(graphics));
            return this;
        }

        public GameBuilder SetSound(string sound)
        {
            _sound = sound ?? throw new ArgumentNullException(nameof(sound));
            return this;
        }

        public GameBuilder SetStoryline(string storyline)
        {
            _storyline = storyline ?? throw new ArgumentNullException(nameof(storyline));
            return this;
        }

        // Побудова готового об'єкта Game
        public Game Build()
        {
            return new Game(_graphics, _sound, _storyline);
        }
    }

    // Director, що інкапсулює логіку побудови часто використовуваних пресетів
    public class GameDirector
    {
        public Game BuildAAAStudioGame(GameBuilder builder)
        {
            return builder
                .SetGraphics("High-Fidelity 3D")
                .SetSound("Dolby Surround")
                .SetStoryline("Епічна сюжетна лінія з відкритим світом")
                .Build();
        }

        public Game BuildMobileCasualGame(GameBuilder builder)
        {
            return builder
                .SetGraphics("Simple 2D")
                .SetSound("Light Ambient")
                .SetStoryline("Короткі місії та рівні для швидкої гри")
                .Build();
        }
    }
}
