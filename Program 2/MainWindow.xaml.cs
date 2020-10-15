using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Collections;

/*
This program would have us read a text file and compute word-occurrence statistics.
 */

namespace BabbleSample
{
    /// Babble framework
    /// Starter code for CS212 Babble assignment
    public partial class MainWindow : Window
    {
        private string input;               // input file
        private string[] words;             // input file broken into array of words
        private int wordCount = 200;        // number of words to babble
        private Dictionary<string, ArrayList>
        hash = new Dictionary<string, ArrayList>(); // dictionary for hash table

        public MainWindow()
        {
            InitializeComponent();
        }

        private void loadButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.FileName = "Sample"; // Default file name
            ofd.DefaultExt = ".txt"; // Default file extension
            ofd.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show open file dialog box
            if ((bool)ofd.ShowDialog())
            {
                input = System.IO.File.ReadAllText(ofd.FileName);  // read file
                words = Regex.Split(input, @"\s+");       // split into array of words
                analyzeInput(orderComboBox.SelectedIndex);
            }
        }

        private void babbleButton_Click(object sender, RoutedEventArgs e)
        {
            // clear the text box
            textBlock1.Text = " ";

            // string[] is an array that has been assigned the hash keys
            string[] keys = hash.Keys.ToArray();

            Random rand = new Random();
            int count = 0;

            for (int i = 0; i < wordCount; i++)
            {
                ArrayList values = hash[keys[count]];     // ArrayList has the key based array assigned to it
                int random_num = rand.Next(values.Count); // random number generator from 0 to array list length

                // first key
                if (count == 0)
                    textBlock1.Text += keys[count] + " ";

                else if (count == keys.Length - 1)
                    count = 0;

                else
                {
                    textBlock1.Text += values[random_num] + " ";
                }
                count += 1;
            }

        }

        // This is to call the analyzeInput function when a selected text input has words.
        private void orderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (words != null)
            {
                analyzeInput(orderComboBox.SelectedIndex);
            }
        }

        /*  analyzeInput uses the user input order to select the N'th order for its statistics computation.
 It would also call keyValue_display() and displays the statistics. 
 */
        private void analyzeInput(int order)
        {
            if (order > 0)
            {
                MessageBox.Show("Analyzing at order: " + order);
                switch (order)
                {
                    case 0:
                        first_order();
                        keyValue_display();
                        break;
                    case 1:
                        second_order();
                        keyValue_display();
                        break;
                    case 2:
                        third_order();
                        keyValue_display();
                        break;
                    case 3:
                        fourth_order();
                        keyValue_display();
                        break;
                    case 4:
                        fifth_order();
                        keyValue_display();
                        break;
                    default:
                        break;
                }
            }
        }

        // This displays the statistics.
        public void keyValue_display()
        {
            //clears the textbox
            textBlock1.Text = " ";
            textBlock1.Text = "For this file, the number of individual words is " + hash.Keys.Count + ", and the total number of words is " + words.Length + "\n";

            //IIndividual words, with their occurrences would be displayed here
            foreach (KeyValuePair<string, ArrayList> entry in hash)
            {
                textBlock1.Text += entry.Key + " -> ";
                foreach (string wordafter in entry.Value)
                    textBlock1.Text += wordafter + " ";

                textBlock1.Text += "\n";
            }
        }

        // the first order function creates a first order hash table 
        public void first_order()
        {
            hash.Clear(); // each time the order is called, clear the hash table
            int index_count = 0;
            foreach (string word in words)
            {
                if (!hash.ContainsKey(word))   // this asserts that the word is not already in the hash table
                    hash.Add(word, new ArrayList());   // if the above is asserted, create a key for that word and put it in the array

                // string index getter
                int index_of_word = Array.IndexOf(words, word, index_count);

                if (index_of_word == words.Length - 1)
                    break;

                else
                    hash[word].Add(words[index_of_word + 1]); // this adds 1 to the index of the current word, and the cycle repeats

                // to keep track of the occurrence
                index_count += 1;
            }
        }

        // the second order function creates a second order hash table 
        public void second_order()
        {
            hash.Clear(); // each time this order is called, clear the hash table
            int index_count = 0;
            foreach (string word in words)
            {

                int index_of_word = Array.IndexOf(words, word, index_count); //get the index of the next word

                /*
				the following strings are assigned the next two words respectively, 
				and the last string is assigned the concatenation of the current word and the next word
				*/
                string second_word = words[index_of_word + 1];
                string two_concatenated_words = word + '_' + second_word;

                if (!hash.ContainsKey(two_concatenated_words))   // this asserts that the word is not already in the hash table
                    hash.Add(two_concatenated_words, new ArrayList());  // if the above is asserted, create a key for that word and put it in the array

                int index_of_second_word = Array.IndexOf(words, second_word, index_count); //second word index getter

                /*
				asserts that the end of the second array has been reached
   			    if not, this adds 1 to the index of the current word, and the cycle repeats 
				*/
                if (index_of_second_word == words.Length - 1)
                    break;
                else
                    hash[two_concatenated_words].Add(words[index_of_second_word + 1]);
                index_count += 1;
            }

        }

        // the third order function creates a third order hash table 
        public void third_order()
        {
            hash.Clear(); // each time this order is called, clear the hash table
            int index_count = 0;
            foreach (string word in words)
            {
                int index_of_word = Array.IndexOf(words, word, index_count); //get the index of the next word

                /*
				the following strings are assigned the next two words respectively, 
				and the last string is assigned the concatenation of the current word and the next two words
				*/
                string second_word = words[index_of_word + 1];
                string third_word = words[index_of_word + 2];
                string three_concatenated_words = word + '_' + second_word + '_' + third_word;

                /*
				the following lines, in order, asserts that the word is not already in the hash table
  			    if the above is asserted, create a key for that word and put it in the array
			    we then retrieve the index of the third word 
				*/
                if (!hash.ContainsKey(three_concatenated_words))
                    hash.Add(three_concatenated_words, new ArrayList());
                int index_of_third_word = Array.IndexOf(words, third_word, index_count);

                /*
				asserts that the end of the second array has been reached
				if not, this adds 1 to the index of the current word, and the cycle repeats 
				*/
                if (index_of_third_word == words.Length - 1)
                    break;
                else
                    hash[three_concatenated_words].Add(words[index_of_third_word + 1]);
                index_count += 1;
            }
        }

        public void fourth_order()
        {
            hash.Clear(); // each time this order is called, clear the hash table
            int index_count = 0;
            foreach (string word in words)
            {
                int index_of_word = Array.IndexOf(words, word, index_count); //get the index of the next word

                /*
				the following strings are assigned the next three words respectively, 
				and the last string is assigned the concatenation of the current word and the next three words
				*/
                string second_word = words[index_of_word + 1];
                string third_word = words[index_of_word + 2];
                string fourth_word = words[index_of_word + 3];
                string four_concatenated_wordsd = word + '_' + second_word + '_' + third_word + '_' + fourth_word;

                /* 
				the following lines, in order, asserts that the word is not already in the hash table
				if the above is asserted, create a key for that word and put it in the array
				we then retrieve the index of the fourth word 
				*/
                if (!hash.ContainsKey(four_concatenated_wordsd))
                    hash.Add(four_concatenated_wordsd, new ArrayList());
                int index_of_fourth_word = Array.IndexOf(words, fourth_word, index_count);

                /* 
				asserts that the end of the second array has been reached
				if not, this adds 1 to the index of the current word, and the cycle repeats 
				*/
                if (index_of_fourth_word == words.Length - 1)
                    break;
                else
                    hash[four_concatenated_wordsd].Add(words[index_of_fourth_word + 1]);
                index_count += 1;
            }

        }

        public void fifth_order()
        {
            hash.Clear(); // each time this order is called, clear the hash table
            int index_count = 0;
            foreach (string word in words)
            {
                int index_of_word = Array.IndexOf(words, word, index_count); //get the index of the next word

                /* 
				the following strings are assigned the next three words respectively, 
				and the last string is assigned the concatenation of the current word and the next three words
				*/
                string second_word = words[index_of_word + 1];
                string third_word = words[index_of_word + 2];
                string fourth_word = words[index_of_word + 3];
                string fifth_word = words[index_of_word + 4];
                string five_concatenated_words = word + '_' + second_word + '_' + third_word + '_' + fourth_word + '_' + fifth_word;

                /* 
				the following lines, in order, asserts that the word is not already in the hash table
				if the above is asserted, create a key for that word and put it in the array
				we then retrieve the index of the fifth word 
				*/
                if (!hash.ContainsKey(five_concatenated_words))
                    hash.Add(five_concatenated_words, new ArrayList());
                int index_of_fifth_word = Array.IndexOf(words, fifth_word, index_count);

                /*
				asserts that the end of the second array has been reached
				if not, this adds 1 to the index of the current word, and the cycle repeats 
				*/
                if (index_of_fifth_word == words.Length - 1)
                    break;
                else
                    hash[five_concatenated_words].Add(words[index_of_fifth_word + 1]);
                index_count += 1;
            }

        }


    }
}


