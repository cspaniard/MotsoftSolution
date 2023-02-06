namespace Services.Help.HelpText

open System
open System.ComponentModel.DataAnnotations
open CommandLine
open Model
open Model.Constants

type IHelpTextBroker = Infrastructure.DI.Brokers.HelpDI.IIHelpTextBroker

type Service () =

    //------------------------------------------------------------------------------------------------------------------
    static let getArgLinesInfo () =

        let leftSpaces = String (' ', 5)

        [|
            ArgLineInfo ("-h,  --host", "help text.")
            ArgLineInfo ("-p,  --port", "help text.")
            ArgLineInfo ("network (required)", "help text.")

            ArgLineInfo ($"{leftSpaces}--help", "Muestra esta ayuda y sale.")
            ArgLineInfo ($"{leftSpaces}--version", "Devuelve información de la versión y sale.")
        |]
    //------------------------------------------------------------------------------------------------------------------

    //------------------------------------------------------------------------------------------------------------------
    static member showHelp (appErrors : AppErrors) =

        //--------------------------------------------------------------------------------------------------------------
        let showHelpText (errorMessages : seq<string>) =

            //----------------------------------------------------------------------------------------------------------
            let showHelpText (errorMessages : seq<string>) =

                IHelpTextBroker.printHeader ()
                IHelpTextBroker.printUsage ()
                IHelpTextBroker.printErrorSection errorMessages
                IHelpTextBroker.printArgsInfo <| getArgLinesInfo ()
            //----------------------------------------------------------------------------------------------------------

            match Seq.head errorMessages with
            | "VERSION" -> IHelpTextBroker.printHeader ()
            | "HELP" -> showHelpText Seq.empty
            | _ -> showHelpText errorMessages
        //--------------------------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------------------------
        let processArgErrors (argErrors : ArgErrors) =

            argErrors
            |> Seq.map (fun error ->
                match error with
                | :? HelpRequestedError -> "HELP"
                | :? VersionRequestedError -> "VERSION"
                | :? MissingRequiredOptionError as e -> if e.NameInfo.LongName <> "" then
                                                            $"--{e.NameInfo.LongName}: Parámetro requerido."
                                                        else
                                                            "Valor requerido no especificado."
                | :? UnknownOptionError as e -> $"Opción desconocida: {e.Token}"
                | :? BadFormatConversionError as e -> $"{e.NameInfo.LongName}: Error de conversión de valores."
                | _ -> $"Error desconocido. %A{error}")
            |> showHelpText

            match Seq.head argErrors with
            | :? HelpRequestedError | :? VersionRequestedError -> EXIT_CODE_OK
            | _ -> EXIT_CODE_ARG_ERROR
        //--------------------------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------------------------
        let processExceptionErrors (exceptionErrors : ExceptionErrors) =

            exceptionErrors
            |> Seq.map (fun e -> e.Message)
            |> showHelpText

            EXIT_CODE_EXCEPTION
        //--------------------------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------------------------
        let processValidationError (validationError : ValidationException) =

            [ validationError.Message ]
            |> showHelpText

            EXIT_CODE_ARG_ERROR
        //--------------------------------------------------------------------------------------------------------------

        match appErrors with
        | ArgErrors argErrors -> processArgErrors argErrors
        | ExceptionErrors exceptionErrors -> processExceptionErrors exceptionErrors
        | ValidationError validationError -> processValidationError validationError
    //------------------------------------------------------------------------------------------------------------------
