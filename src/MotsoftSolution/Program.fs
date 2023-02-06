open System
open CommandLine
open CommandLine.Text
open Model
open Model.Definitions

//----------------------------------------------------------------------------------------------------------------------
try
    let args = Environment.GetCommandLineArgs () |> Array.tail
    use parser = new Parser (fun o -> o.HelpWriter <- null)

    match parser.ParseArguments<ArgumentOptions> args with
    | Parsed as opts ->
            // Lanzar la aplicación
            // appStart opts.Value
            // |> exit
            exit 0
    | NotParsed as notParsed ->
            HelpText.AutoBuild<ArgumentOptions> notParsed
            |> Console.Error.WriteLine
            exit 1

with e ->
    Console.WriteLine e.Message
//----------------------------------------------------------------------------------------------------------------------
