﻿using System;
using System.Diagnostics;
using System.Media;
using System.Net;
using System.Runtime.InteropServices;
using System.Speech.Recognition;
using System.Speech.Synthesis;

namespace VoiceRecTest
{
    class Program
    {
        [DllImport("User32", CharSet = CharSet.Auto)]
        public static extern int SystemParametersInfo(int uiAction, int uiParam,
            string pvParam, uint fWinIni);

        public static string Mine { get; set; }
        public static int Emil { get; set; }
        public static string Path = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}";

        private static readonly SpeechSynthesizer synthesizer = new SpeechSynthesizer();

        static void Main(string[] args)
        {
            SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
            Choices commands = new Choices();
            commands.Add(new string[] { "hello computer", "say my name", "open chrome", "what day is it today", "download a hot wallpaper", "play me a cool song", "qvamma", "payday payday", "i told him", "mine mine", "nein nein", "email", "name count", "put on some christmas music", "69", "yes", "eskil", "nice" });
            GrammarBuilder gBuilder = new GrammarBuilder();
            gBuilder.Append(commands);
            Grammar grammar = new Grammar(gBuilder);

            recEngine.LoadGrammarAsync(grammar);
            recEngine.SetInputToDefaultAudioDevice();
            recEngine.SpeechRecognized += recEngine_SpeechRecognized;

            recEngine.RecognizeAsync(RecognizeMode.Multiple);

            Console.WriteLine("Waiting for voice command.");
            Console.ReadKey();
        }

        private static void recEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text)
            {
                case "hello computer":
                    SoundPlayer player7 = new SoundPlayer($@"{ Path }\desktop\sounds\hello.wav");
                    player7.Play();
                    break;
                case "say my name":
                    Console.WriteLine("Chris?! Is that you?");
                    break;
                case "open chrome":
                    Process.Start(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe");
                    Console.WriteLine("Here yah go!");
                    break;
                case "what day is it today":
                    synthesizer.SpeakAsync("It's " + DateTime.Now.DayOfWeek);
                    Console.WriteLine("It's " + DateTime.Now.DayOfWeek);
                    break;
                case "download a hot wallpaper":
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(new Uri("https://github.com/MrChickenBacon/HotPink/raw/master/Hotpink/Back.PNG"), $@"{Path}\desktop\Hot Wallpaper.PNG");
                    }
                    SystemParametersInfo(0x0014, 0, $@"{Path}\desktop\Hot Wallpaper.PNG", 0x0001);
                    Console.WriteLine("Enjoy :)");
                    break;
                case "play me a cool song":
                    try
                    {
                        Console.WriteLine("Oh! It's Party time!");
                        SoundPlayer player1 = new SoundPlayer("https://github.com/MrChickenBacon/Surge/raw/master/town.wav");
                        player1.Play();
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                    }
                    break;
                case "qvamma":
                    SoundPlayer player2 = new SoundPlayer(@"C:\Windows\media\tada.wav");
                    player2.Play();
                    break;
                case "payday payday":
                    Console.WriteLine("Payday!");
                    SoundPlayer player3 = new SoundPlayer($@"{ Path }\desktop\sounds\ka-ching.wav");
                    player3.Play();
                    break;
                case "i told him":
                    Console.WriteLine("Yeah!");
                    SoundPlayer player4 = new SoundPlayer($@"{ Path }\desktop\sounds\Mmm.wav");
                    player4.Play();
                    break;
                case "mine mine":
                    Console.WriteLine("Mine?");
                    SoundPlayer player5 = new SoundPlayer($@"{ Path }\desktop\sounds\mine.wav");
                    player5.Play();
                    break;
                case "email":
                    Console.WriteLine("Emil?");
                    synthesizer.SpeakAsync("pling");
                    Emil++;
                    break;
                case "name count":
                    Console.WriteLine(Emil);
                    synthesizer.SpeakAsync("Emil has been said " + Emil + " times");
                    break;
                case "nein nein":
                    Console.WriteLine("Nine?");
                    SoundPlayer player6 = new SoundPlayer($@"{ Path }\desktop\sounds\nein.wav");
                    player6.Play();
                    break;
                case "put on some christmas music":
                    Console.WriteLine("Jingle");
                    Process.Start("https://www.youtube.com/watch?v=QOAkVCigk5Y");
                    break;
                case "69":
                    synthesizer.SpeakAsync("6 9 six to the nine. YOYOYO, hey to the ho!");
                    break;
                case "yes":
                    synthesizer.SpeakAsync("no!");
                    break;
                case "eskil":
                    synthesizer.SpeakAsync("He's that guitar man right?");
                    break;
                case "nice":
                    Console.WriteLine("Nice?");
                    SoundPlayer player8 = new SoundPlayer($@"{ Path }\desktop\sounds\nice.wav");
                    player8.Play();
                    break;
            }
        }
    }
}
