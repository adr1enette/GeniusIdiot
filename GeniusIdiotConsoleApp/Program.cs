using System;

namespace GeniusIdiotConsoleApp;

internal class Program
{
    private static void Main(string[] args)
    {
        const int questionCount = 5;
        int[] indexArray = GenerateShuffledIndexArray(questionCount);

        string[] questions = GetQuestions(indexArray);
        int[] answers = GetAnswers(indexArray);
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

    private static T[] GetShuffledArray<T>(T[] array, int[] indexArray)
    {
        var shuffledArray = new T[indexArray.Length];
        for (int i = 0; i < indexArray.Length; i++)
        {
            shuffledArray[i] = array[indexArray[i]];
        }

        return shuffledArray;
    }

    private static int[] GetAnswers(int[] indexArray)
    {
        int[] answers = new int[indexArray.Length];
        answers[0] = 6;
        answers[1] = 9;
        answers[2] = 25;
        answers[3] = 60;
        answers[4] = 2;

        return GetShuffledArray(answers, indexArray);
    }

    private static string[] GetQuestions(int[] indexArray)
    {
        string[] questions = new string[indexArray.Length];
        questions[0] = "Сколько будет два плюс два умноженное на два?";
        questions[1] = "Бревно нужно распилить на 10 частей. Сколько распилов нужно сделать?";
        questions[2] = "На двух руках 10 пальцев. Сколько пальцев на 5 руках?";
        questions[3] = "Укол делают каждые полчаса. Сколько нужно минут, чтобы сделать три укола?";
        questions[4] = "Пять свечей горело, две потухли. Сколько свечей осталось?";

        return GetShuffledArray(questions, indexArray);
    }

    private static int[] GenerateShuffledIndexArray(int questionCount)
    {
        int[] array = new int[questionCount];
        for (int i = 0; i < questionCount; i++)
        {
            array[i] = i;
        }

        var random = new Random();
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            (array[i], array[j]) = (array[j], array[i]);
        }

        return array;
    }
}