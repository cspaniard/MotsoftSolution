namespace Model

open System
open System.ComponentModel.DataAnnotations
open CommandLine

type SequenceInfo = {
    Prefix : string
}

type MoveInfo = {
    Id : int
    Name : string
}

type ArgLineInfo = ArgLineInfo of paramNames : string * helpText : string

type Parsed = Parsed<ArgumentOptions>
type NotParsed = NotParsed<ArgumentOptions>

type ArgErrors = seq<Error>
type ExceptionErrors = seq<Exception>

type AppErrors =
    | ArgErrors of ArgErrors
    | ExceptionErrors of ExceptionErrors
    | ValidationError of ValidationException

module Definitions =

    let (|Parsed|NotParsed|) (parserResult : ParserResult<_>) =

        match parserResult with
        | :? Parsed -> Parsed
        | :? NotParsed -> NotParsed
        | _ -> failwith "No debiéramos llegar aquí."
