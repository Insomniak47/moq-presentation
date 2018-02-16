using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NantauCommon
{
    public static class InputUtils
    {
        /// <summary>
        /// Gets an integer from the user via the console. Returns between lower (exclusive) and upper (inclusive)
        /// </summary>
        /// <param name="lower"> the lower bound, values greater than this value will be accepted</param>
        /// <param name="upper"> the upper bound, values lower or equal to this will be accepted </param>
        public static int GetIntInRange(int lower, int upper) //Get rid of this and replace with getSelection method
        {
            bool success = false;
            int returnValue = int.MinValue;

            while (!success)
            {
                string response = Console.ReadLine();

                success = int.TryParse(response, out returnValue);
                success &= returnValue >= lower && returnValue < upper;
            }

            return returnValue;
        }

        public static int GetInt(Range range)
        {
            switch (range)
            {
                case Range.Positive:
                    return GetIntInRange(0, int.MaxValue);
                case Range.PositiveNonZero:
                    return GetIntInRange(1, int.MaxValue);
                case Range.Any:
                    return GetIntInRange(int.MinValue, int.MaxValue);
                case Range.Negative:
                    throw new NotImplementedException();
                case Range.NegativeNonZero:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentException("Bad range value");
            }
        }



        public static int GetListItemChoice(int listCount) 
        {
            bool success = false;
            int returnValue = int.MinValue;

            while (!success)
            {
                string response = Console.ReadLine();

                success = int.TryParse(response, out returnValue);
                success &= returnValue > 0 && returnValue <= listCount;
            }

            return returnValue;
        }


        public static bool GetYesNoValue()
        {
            string processedResp = string.Empty;

            while (processedResp != "y" && processedResp != "n")
            {
                string rawResp = Console.ReadLine();
                processedResp = rawResp.Trim().ToLowerInvariant();
            }

            return processedResp == "y";
        }

        public static int GetSelection(IEnumerable<string> menuOptions)
        {
            int i = 0;

            foreach (var menuItem in menuOptions)
            {
                Console.WriteLine($"{++i}: {menuItem}");
            }

            return InputUtils.GetListItemChoice(menuOptions.Count());
        }


        public enum Range
        {
            Positive,
            PositiveNonZero,
            Negative,
            NegativeNonZero,
            Any
        }

    }
}
