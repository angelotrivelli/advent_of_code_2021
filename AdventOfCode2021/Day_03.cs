using AoCHelper;

namespace AdventOfCode2021
{
    public class Day_03 : BaseDay
    {
        private readonly string input;
        private IEnumerable<UInt16> diags;

        public Day_03()
        {
            input = File.ReadAllText(InputFilePath);
            diags = input.Split("\r\n").Select(x => Convert.ToUInt16(x,2));
            // 12 bit binary numbers represented as unsigned int16's
        }

        public override ValueTask<string> Solve_1()
        {
            int[] tally = {0,0,0,0, 0,0,0,0, 0,0,0,0};
            //int[] tally = {0,0,0,0,0};

            foreach(var d in diags)
                for(UInt16 i=0 ; i<12 ; i++)
                    tally[i] += (d & (1 << i)) >> i;

            // Console.WriteLine("------------");
            // foreach(var t in tally){
            //     Console.Write(t + " ");
            // }
            // Console.WriteLine();
            // Console.WriteLine("------------");

            var numLines = diags.Count();
            var g = tally.Select(x => x>numLines/2 ? 1 : 0).Reverse();
            var e = tally.Select(x => x>numLines/2 ? 0 : 1).Reverse();

            Console.WriteLine($"gamma = {string.Join("",g)}");
            Console.WriteLine($"epsilon = {string.Join("",e)}");

            var gamma = Convert.ToUInt16(string.Join("",g), 2);
            var epsilon = Convert.ToUInt16(string.Join("",e), 2);

            Console.WriteLine($"gamma = {gamma}");
            Console.WriteLine($"epsilon = {epsilon}");

            UInt32 product = ((UInt32)gamma)*((UInt32)epsilon);

            return new ValueTask<string>($"gamma * epsilon = {product}");
        }

        public override ValueTask<string> Solve_2()
        {
            IEnumerable<UInt16> oxy = diags.ToList();

            for(int i=11 ; i>=0 ; i--)
            {

                if (oxy.Where(x => ((x & ((UInt16)1 << i)) >> i == (UInt16)1)).Count() >=
                    oxy.Where(x => ((x & ((UInt16)1 << i)) >> i == (UInt16)0)).Count())
                {
                    oxy = oxy.Where(x => ((x & ((UInt16)1 << i)) >> i) == (UInt16)1).ToList();
                }
                else
                {
                    oxy = oxy.Where(x => ((x & ((UInt16)1 << i)) >> i) == (UInt16)0).ToList();
                }

                // Console.WriteLine($"{i}--------- oxy count = {oxy.Count()} ");
                // foreach(var o in oxy)
                // {
                //     Console.WriteLine(Convert.ToString(o,2).PadLeft(12,'0'));
                // }
                // Console.WriteLine("-----------");
                if (oxy.Count() == 1) break;
            }

            Console.WriteLine($"oxy = {Convert.ToString(oxy.First(), 2).PadLeft(12, '0')}");

            IEnumerable<UInt16> co2 = diags.ToList();

            for (int i = 11; i >= 0; i--)
            {
                if (co2.Where(x => ((x & ((UInt16)1 << i)) >> i == (UInt16)0)).Count() <=
                    co2.Where(x => ((x & ((UInt16)1 << i)) >> i == (UInt16)1)).Count())
                {
                    co2 = co2.Where(x => ((x & ((UInt16)1 << i)) >> i) == (UInt16)0).ToList();
                }
                else
                {
                    co2 = co2.Where(x => ((x & ((UInt16)1 << i)) >> i) == (UInt16)1).ToList();
                }

                // Console.WriteLine($"{i}--------- co2 count = {co2.Count()} ");
                // foreach (var c in co2)
                // {
                //     Console.WriteLine(Convert.ToString(c, 2).PadLeft(12, '0'));
                // }
                // Console.WriteLine("-----------");
                if (co2.Count() == 1) break;
            }

            Console.WriteLine($"co2 = {Convert.ToString(co2.First(), 2).PadLeft(12, '0')}");

            return new ValueTask<string>($"oxy * co2 = {oxy.First()*co2.First()}");
        }
    }

}
