/*---------Lg_Lg.cs------------
/*********************************************************************
* C# code for the floor of lg lg n, where n is an integer.
*
* Robert Akinie, 9/19/2020
*
*********************************************************************/
using System;

namespace Floor
{
    class Program
    {
        static void Main(string[] args)
        {
			Console.WriteLine("Welcome to the Floor finder!");
			while (true)
			{
				/* The floor is calculated by taking the log of 
				 * the log of the input number
				 */
				Console.Write("\nEnter n: ");
			   /* input assigned to variable n. 
			    */
			   long n = long.Parse(Console.ReadLine());
			   long floor = Log(Log(n));
			   Console.WriteLine("The floor of {0} = {1}.", n, floor);
		   }
	   }
	   /* This fuction calculates the log of the input number
		* The algorithm is a recursive one where the number is 
		* successfully divided by 2 and called again until the
		* base case is reached
		*/
				static long Log (long number)
		{
			if(number > 1)
            {
				return 1 + Log(number / 2);
            }
			return 0;
		}
    }
}