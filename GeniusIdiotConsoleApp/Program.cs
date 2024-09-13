using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GeniusIdiotConsoleApp;

internal class Program
{
    private static void Main(string[] args)
    {
        var userName = GetUserName();
        do
        {
            ExecuteQuiz(userName);
        } while (ShouldPlayAgain());
    }

    private static void ExecuteQuiz(string userName)
    {
        var quizData = GetQuizData();
        Random.Shared.Shuffle(CollectionsMarshal.AsSpan(quizData));
        var questionCount = quizData.Count;

        var correctAnswerCount = 0;
        for (int i = 0; i < questionCount; i++)
        {
            var (question, answer) = quizData[i];

            var userAnswer = GetUserAnswer(i + 1, question);
            if (userAnswer == answer)
            {
                correctAnswerCount++;
            }
        }

        var diagnosis = GetDiagnosis(correctAnswerCount, questionCount);
        DisplayResults(correctAnswerCount, diagnosis, userName);
    }

    private static void DisplayResults(int correctAnswerCount, string diagnosis, string userName)
    {
        Console.Clear();
        Console.WriteLine($"Количество правильных ответов: {correctAnswerCount}");
        Console.WriteLine($"{userName}, ваш диагноз: {diagnosis}");
    }

    private static string GetDiagnosis(int correctAnswerCount, int questionCount)
    {
        var percentage = (double)correctAnswerCount / questionCount * 100;

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

    private static int GetUserAnswer(int questionNumber, string question)
    {
        int result;

        var input = GetInputPrompt();
        while (!int.TryParse(input, out result))
        {
            input = GetInputErrorPrompt();
        }

        return result;

        string GetInputPrompt()
        {
            Console.Clear();
            Console.WriteLine($"Вопрос №{questionNumber}");
            Console.WriteLine(question);
            return Console.ReadLine()?.Trim();
        }

        string GetInputErrorPrompt()
        {
            Console.Clear();
            Console.WriteLine($"Вопрос №{questionNumber}");
            Console.WriteLine($"{question} (Пожалуйста, вводите только цифры)");
            return Console.ReadLine()?.Trim();
        }
    }

    private static bool ShouldPlayAgain()
    {
        Console.WriteLine("Сыграем еще? (y/n)");
        while (true)
        {
            var input = char.ToLower(Console.ReadKey(true).KeyChar);
            if (input == 'y' || input == 'н') return true;
            if (input == 'n' || input == 'т') return false;
        }
    }

    private static string GetUserName()
    {
        var input = GetUserNamePrompt();
        while (string.IsNullOrWhiteSpace(input))
        {
            input = GetUserNameErrorPrompt();
        }

        return input;

        string GetUserNamePrompt()
        {
            Console.Clear();
            Console.WriteLine("Как вас зовут?");
            return Console.ReadLine()?.Trim();
        }

        string GetUserNameErrorPrompt()
        {
            Console.Clear();
            Console.WriteLine("Как вас зовут? (Пожалуйста, введите имя)");
            return Console.ReadLine()?.Trim();
        }
    }

    private static List<(string question, int answer)> GetQuizData()
    {
        return new List<(string, int)>
        {
            ("Сколько будет два плюс два умноженное на два?", 6),
            ("Бревно нужно распилить на 10 частей. Сколько распилов нужно сделать?", 9),
            ("На двух руках 10 пальцев. Сколько пальцев на 5 руках?", 25),
            ("Укол делают каждые полчаса. Сколько нужно минут, чтобы сделать три укола?", 60),
            ("Пять свечей горело, две потухли. Сколько свечей осталось?", 2)
        };
    }
}