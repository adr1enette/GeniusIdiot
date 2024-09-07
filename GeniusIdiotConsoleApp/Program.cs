using System;
using System.Collections.Generic;

namespace GeniusIdiotConsoleApp;

internal class Program
{
    private static void Main(string[] args)
    {
        string userName = AskUserName();
        do
        {
            var questions = GetQuestionList();
            var answers = GetAnswerList();
            int questionCount = questions.Count;

            int correctAnswerCount = RunQuiz(questions, answers, questionCount);
            string diagnosis = DetermineDiagnosis(correctAnswerCount, questionCount);
            DisplayResults(correctAnswerCount, userName, diagnosis);
        } while (AskPlayAgain());
    }

    private static void DisplayResults(int correctAnswerCount, string userName, string diagnosis)
    {
        Console.Clear();
        Console.WriteLine($"Количество правильных ответов: {correctAnswerCount}");
        Console.WriteLine($"{userName}, ваш диагноз: {diagnosis}");
    }

    private static int RunQuiz(List<string> questions, List<int> answers, int questionCount)
    {
        var random = new Random();
        int correctAnswerCount = 0;
        for (int i = 0; i < questionCount; i++)
        {
            int questionNumber = i + 1;
            int index = random.Next(questions.Count);
            string question = GetAndRemoveElement(questions, index);
            int validAnswer = GetAndRemoveElement(answers, index);

            AskQuestion(questionNumber, question);
            int userAnswer = GetUserAnswer(questionNumber, question);
            if (userAnswer == validAnswer)
            {
                correctAnswerCount++;
            }
        }

        return correctAnswerCount;
    }

    private static int GetUserAnswer(int questionNumber, string question)
    {
        int userAnswer;
        while (!int.TryParse(Console.ReadLine().Trim(), out userAnswer))
        {
            AskQuestion(questionNumber, $"{question} (Пожалуйста, вводите только числа)");
        }

        return userAnswer;
    }

    private static void AskQuestion(int questionNumber, string question)
    {
        Console.Clear();
        Console.WriteLine($"Вопрос №{questionNumber}");
        Console.WriteLine(question);
    }

    private static T GetAndRemoveElement<T>(List<T> list, int index)
    {
        var randomElement = list[index];
        list.RemoveAt(index);

        return randomElement;
    }

    private static string DetermineDiagnosis(int correcAnswerCount, int questionCount)
    {
        double percentage = (double)correcAnswerCount / questionCount * 100;

        return percentage switch
        {
            100 => "гений",
            >= 76 => "талант",
            >= 51 => "нормальный",
            >= 26 => "дурак",
            >= 1 => "идиот",
            _ => "кретин"
        };
    }

    private static bool AskPlayAgain()
    {
        while (true)
        {
            Console.WriteLine("Сыграем еще? (y/n)");
            char userInput = char.ToLower(Console.ReadKey(true).KeyChar);

            if (userInput == 'y')
            {
                return true;
            }
            else if (userInput == 'n')
            {
                Console.Write("Спасибо за участие!");
                return false;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Нажмите 'y' чтобы играть, 'n' чтобы выйти");
            }
        }
    }

    private static string AskUserName()
    {
        Console.Clear();
        Console.WriteLine("Как вас зовут?");

        return Console.ReadLine().Trim();
    }

    private static List<int> GetAnswerList()
    {
        return new List<int> { 6, 9, 25, 60, 2 };
    }

    private static List<string> GetQuestionList()
    {
        return new List<string>
        {
            "Сколько будет два плюс два умноженное на два?",
            "Бревно нужно распилить на 10 частей. Сколько распилов нужно сделать?",
            "На двух руках 10 пальцев. Сколько пальцев на 5 руках?",
            "Укол делают каждые полчаса. Сколько нужно минут, чтобы сделать три укола?",
            "Пять свечей горело, две потухли. Сколько свечей осталось?"
        };
    }
}