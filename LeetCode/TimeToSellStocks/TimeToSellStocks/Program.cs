using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*

Your are given an array of integers prices, for which the i-th element is the price of a given stock on day i;
and a non-negative integer fee representing a transaction fee.

You may complete as many transactions as you like, but you need to pay the transaction fee for each transaction.
You may not buy more than 1 share of a stock at a time (ie.you must sell the stock share before you buy again.)

https://leetcode.com/problems/best-time-to-buy-and-sell-stock-with-transaction-fee/description/

*/

namespace TimeToSellStocks {
    class Program {
        static void Main(string[] args) {
            int fee = 2;
            int[] prices = new int[] { 1, 3, 2, 8, 4, 9 };
            int highest = MaxProfit(prices, fee);
            Console.WriteLine(highest);
            Console.ReadKey();
        }

        static public int MaxProfit(int[] prices, int fee) {
            int priceAmount = 0;
            int holdAmount = prices[0];
            holdAmount = holdAmount - 2 * holdAmount;
            for (int i =0; i < prices.Length; i++) {
                priceAmount = Math.Max(priceAmount, holdAmount + prices[i] - fee);
                holdAmount = Math.Max(holdAmount, priceAmount - prices[i]);
            }
            return priceAmount;
        }
    }
}
