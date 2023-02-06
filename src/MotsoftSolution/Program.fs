open System
open System.ComponentModel.DataAnnotations
open CommandLine
open Model
open Model.Definitions
open Model.Constants

type private IHelpService = DI.Services.HelpDI.IHelpService
type private IExceptionService = DI.Services.ExceptionsDI.IExceptionService

//----------------------------------------------------------------------------------------------------------------------
try
    let args = Environment.GetCommandLineArgs () |> Array.tail
    use parser = new Parser (fun o -> o.HelpWriter <- null)

    match parser.ParseArguments<ArgumentOptions> args with
    | Parsed as opts ->
             // appInit opts.Value
             // appStart opts.Value
             // |> exit
             exit 0
    | NotParsed as notParsed ->
             notParsed.Errors
             |> ArgErrors
             |> IHelpService.showHelp
             |> exit

with
| :? AggregateException as ae -> IHelpService.showHelp <| ExceptionErrors ae.InnerExceptions |> exit
| :? ValidationException as ve -> IHelpService.showHelp <| ValidationError ve |> exit
| e -> IExceptionService.outputException e
       exit EXIT_CODE_EXCEPTION
//----------------------------------------------------------------------------------------------------------------------
