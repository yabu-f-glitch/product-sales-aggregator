using System;
using System.Collections.Generic;
using System.Linq;

class ProductSalesAggregator
{
    static void Main(string[] args)
    {
        List<(string name, decimal price)> products = new List<(string, decimal)>();
        bool continueInput = true;

        Console.WriteLine("======================================");
        Console.WriteLine("  商品売上入力・集計プログラム");
        Console.WriteLine("======================================\n");

        while (continueInput)
        {
            Console.Write("商品名を入力してください: ");
            string productName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(productName))
            {
                Console.WriteLine("エラー: 商品名を入力してください。\n");
                continue;
            }

            Console.Write("金額を入力してください: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price) || price < 0)
            {
                Console.WriteLine("エラー: 正の数値を入力してください。\n");
                continue;
            }

            products.Add((productName, price));
            Console.WriteLine($"✓ {productName}: ¥{price:F2} を追加しました。\n");

            Console.Write("続けて入力しますか？ (y/n): ");
            string input = Console.ReadLine()?.ToLower();
            continueInput = (input == "y" || input == "yes");
            Console.WriteLine();
        }

        if (products.Count == 0)
        {
            Console.WriteLine("入力されたデータがありません。");
            return;
        }

        DisplayResults(products);
    }

    static void DisplayResults(List<(string name, decimal price)> products)
    {
        Console.WriteLine("\n======================================");
        Console.WriteLine("  集計結果");
        Console.WriteLine("======================================\n");

        // 商品一覧
        Console.WriteLine("【商品一覧】");
        for (int i = 0; i < products.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {products[i].name}: ¥{products[i].price:F2}");
        }

        // 合計
        decimal total = products.Sum(p => p.price);
        Console.WriteLine($"\n【合計金額】¥{total:F2}");

        // 平均
        decimal average = products.Average(p => p.price);
        Console.WriteLine($"【平均金額】¥{average:F2}");

        // 最高額・最低額
        decimal max = products.Max(p => p.price);
        decimal min = products.Min(p => p.price);
        Console.WriteLine($"【最高金額】¥{max:F2}");
        Console.WriteLine($"【最低金額】¥{min:F2}");

        // 件数
        Console.WriteLine($"【入力件数】{products.Count} 件");

        Console.WriteLine("\n======================================");
    }
}
