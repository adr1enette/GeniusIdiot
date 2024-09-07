using System;

namespace GeniusIdiotConsoleApp;

internal class Program
{
    private static void Main(string[] args)
    {
        const int questionCount = 5;

        string[] questions = GetQuestions(questionCount);
        int[] answers = GetAnswers(questionCount);
        string[] diagnoses = GetDiagnoses();

        int validAnswersCount = 0;
        for (int i = 0; i < questionCount; i++)
        {
            Console.Clear();
            Console.WriteLine($"Вопрос №{i + 1}");
            Console.WriteLine(questions[i]);

            int userAnswer;
            while (!int.TryParse(Console.ReadLine().Trim(), out userAnswer))
            {
                Console.Clear();
                Console.WriteLine($"Вопрос №{i + 1}");
                Console.WriteLine($"{questions[i]} (Пожалуйста, вводите только числа)");
            }

            int validAnswer = answers[i];
            if (userAnswer == validAnswer)
            {
                validAnswersCount++;
            }
        }

        Console.Clear();
        Console.WriteLine("Количество правильных ответов: " + validAnswersCount);
        Console.WriteLine("Ваш диагноз: " + diagnoses[validAnswersCount]);
    }

    private static string[] GetDiagnoses()
    {
        string[] diagnoses = new string[6];
        diagnoses[0] = "кретин";
        diagnoses[1] = "идиот";
        diagnoses[2] = "дурак";
        diagnoses[3] = "нормальный";
        diagnoses[4] = "талант";
        diagnoses[5] = "гений";

        return diagnoses;
    }

    private static int[] GetAnswers(int questionCount)
    {
        int[] answers = new int[questionCount];
        answers[0] = 6;
        answers[1] = 9;
        answers[2] = 25;
        answers[3] = 60;
        answers[4] = 2;

        return answers;
    }

    private static string[] GetQuestions(int questionCount)
    {
        string[] questions = new string[questionCount];
        questions[0] = "Сколько будет два плюс два умноженное на два?";
        questions[1] = "Бревно нужно распилить на 10 частей. Сколько распилов нужно сделать?";
        questions[2] = "На двух руках 10 пальцев. Сколько пальцев на 5 руках?";
        questions[3] = "Укол делают каждые полчаса. Сколько нужно минут, чтобы сделать три укола?";
        questions[4] = "Пять свечей горело, две потухли. Сколько свечей осталось?";

        return questions;
    }
}