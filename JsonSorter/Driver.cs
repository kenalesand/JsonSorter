using CommandLine;

namespace JsonSorter
{
    public class Arguments
    {
        [Option('i', "InputFile", Required = true, HelpText = "JSON file containing an array of objects to sort, based on timestamp field.")]
        public string InputFile { get; set; }
        [Option('o', "OutputFile", Required = false, HelpText = "JSON file to write sorted contents to and may be the same as the input.")]
        public string OutputFile { get; set; }
        [Option('f', "Field", Default = "timestamp", HelpText = "Field to sort on")]
        public string Field { get; set; }
    }

    class Driver
    {
        static void Main(string[] args) => Parser.Default.ParseArguments<Arguments>(args).WithParsed(args => Run(args));

        public static void Run(Arguments args)
        {
            new Sorter().Sort(args.InputFile, args.OutputFile, args.Field);
        }
    }
}
